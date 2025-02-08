using System;
using Godot;

namespace Cardoni;

public abstract partial class AttackingEntity : Entity
{
	public const uint StacksPerAttack = 1200;

	public uint AttackSpeed { get; set; }

	uint attackStacks;

	uint startingStacks = 600;

	public uint Range { get; set; }

	public Entity Target { get; set; }

	public uint TargetMask { get; set; }

	public bool Attacking { get; set; }

	void Tick()
	{
		if (Target == null || !IsValidTarget(Target))
		{
			if (Attacking)
			{
				StopAttack();
			}
			Target = FindClosestTarget();
			if (Target == null)
				return;
			Direction = Target.Y > Y ? 1 : -1;
		}

		if (VerticalDistance(Target) <= Range)
		{
			if (Attacking)
			{
				ContinueAttack();
			}
			else
			{
				StartAttack();
			}
		}
		else if (Attacking)
		{
			StopAttack();
		}
	}

	private void StartAttack()
	{
		Attacking = true;
		GD.Print(Name, " starting attack against ", Target.Name);
		Direction = 0;
		attackStacks = startingStacks;
	}

	private void StopAttack()
	{
		Attacking = false;
		GD.Print(Name, " stopping attack");
	}

	private Entity FindClosestTarget()
	{
		Entity closest = null;
		int minDistance = Constants.TicksPerLane + 1;
		foreach (Entity entity in GameState.Entities)
		{
			if (!IsValidTarget(entity))
				continue;

			int distance = VerticalDistance(entity);
			if (distance < minDistance)
			{
				minDistance = distance;
				closest = entity;
			}
		}
		return closest;
	}

	private bool IsValidTarget(Entity entity)
	{
		return entity != this
			&& entity.IsAlive
			&& (entity.OccupyingLanes & OccupyingLanes) != 0
			&& (entity.Mask & TargetMask) != 0;
	}

	private void ContinueAttack()
	{
		attackStacks += AttackSpeed;
		while (attackStacks >= StacksPerAttack)
		{
			Attack();
			attackStacks -= StacksPerAttack;
		}
	}

	protected abstract void Attack();

	public override void Spawn()
	{
		base.Spawn();
		GameState.AddTicked(Tick);
	}

	public override void Kill()
	{
		base.Kill();
		GameState.RemoveTicked(Tick);
	}
}
