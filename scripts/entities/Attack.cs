using System;
using Godot;

namespace Cardoni;

public abstract class Attack
{
	public const uint StacksPerAttack = 1200;

	uint attackStacks;

	uint startingStacks = 600;

	public uint Range { get; set; }

	public Entity Target { get; set; }
	public Entity Entity { get; set; }

	public bool Attacking { get; set; }

	public Attack(Entity entity, uint range)
	{
		Entity = entity;
		Range = range;
	}

	void Tick()
	{
		if (Attacking)
		{
			if (Target != null && IsValidTarget(Target) && InRange(Target))
			{
				ContinueAttack();
			}
			else
			{
				FinishAttack();
			}
		}
		else
		{
			Target = FindClosestTarget();
			if (Target == null)
			{
				Entity.Direction = 0;
				return;
			}
			if (InRange(Target))
			{
				StartAttack();
			}
			else
			{
				Entity.Direction = Target.Y > Entity.Y ? 1 : -1;
			}
		}
	}

	protected virtual void FinishAttack()
	{
		attackStacks += Entity.AttackSpeed;
		if (attackStacks >= startingStacks)
		{
			StopAttack();
			attackStacks = startingStacks;
		}
	}

	protected virtual bool InRange(Entity target)
	{
		return Entity.VerticalDistance(target) <= Range;
	}

	protected virtual void StartAttack()
	{
		GD.Print(Entity.Name, " ", Entity.AttackSpeedModifier);
		Attacking = true;
		Entity.Direction = 0;
		attackStacks = startingStacks;
	}

	protected virtual void StopAttack()
	{
		Attacking = false;
	}

	protected virtual Entity FindClosestTarget()
	{
		Entity closest = null;
		int minDistance = Constants.TicksPerLane + 1;
		foreach (Entity entity in GameState.Entities)
		{
			if (!IsValidTarget(entity))
				continue;

			int distance = Entity.VerticalDistance(entity);
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
		return entity != Entity
			&& entity.IsAlive
			&& Entity.OccupyingLanes.Intersects(entity.OccupyingLanes)
			&& Entity.TargetMask.Matches(entity.Mask);
	}

	protected virtual void ContinueAttack()
	{
		attackStacks += Entity.AttackSpeed;
		while (attackStacks >= StacksPerAttack)
		{
			Activate();
			attackStacks -= StacksPerAttack;
		}
	}

	protected abstract void Activate();

	public void Start()
	{
		GameState.AddTicked(Tick);
	}

	public void End()
	{
		GameState.RemoveTicked(Tick);
	}
}
