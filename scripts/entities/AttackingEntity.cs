using System;
using Godot;

namespace Cardoni;

public abstract partial class AttackingEntity : Entity
{
	public const uint StacksPerAttack = 1200;

	uint attackStacks;

	uint startingStacks = 600;

	public uint Range { get; set; }

	public Entity Target { get; set; }

	public EntityMask TargetMask { get; set; }

	public bool Attacking { get; set; }

	void Tick()
	{
		if (Attacking) {
			if (Target != null && IsValidTarget(Target) && InRange(Target)) {
				ContinueAttack();
			} else {
				FinishAttack();
			}
		} else {
			Target = FindClosestTarget();
			if (Target == null)
			{
				Direction = 0;
				return;
			}
			if (InRange(Target))
			{
				StartAttack();
			}
			else
			{
				Direction = Target.Y > Y ? 1 : -1;
			}
		}
	}

	protected virtual void FinishAttack()
	{
		attackStacks += AttackSpeed;
		if (attackStacks >= startingStacks) {
			StopAttack();
			attackStacks = startingStacks;
		}
	}

	protected virtual bool InRange(Entity target)
	{
		return VerticalDistance(target) <= Range;
	}

	protected virtual void StartAttack()
	{
		Attacking = true;
		GD.Print(Name, " starting attack against ", Target.Name, " ", MovementSpeedModifier);
		Direction = 0;
		attackStacks = startingStacks;
	}

	protected virtual void StopAttack()
	{
		Attacking = false;
		GD.Print(Name, " stopping attack");
	}

	protected virtual Entity FindClosestTarget()
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

	protected virtual bool IsValidTarget(Entity entity)
	{
		return entity != this
			&& entity.IsAlive
			&& (entity.OccupyingLanes & OccupyingLanes) != 0
			&& TargetMask.Matches(entity.Mask);
	}

	protected virtual void ContinueAttack()
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
