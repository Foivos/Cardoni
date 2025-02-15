using System;
using System.Collections.Generic;
using Godot;

namespace Cardoni;

public abstract partial class Passive
{
	public List<Condition> ActiveConditions { get; set; } = new();

	public Entity Entity { get; set; }

	public EntityMask TargetMask { get; set; }

	public Passive(Entity entity, EntityMask targetMask)
	{
		Entity = entity;
		TargetMask = targetMask;
	}

	public virtual void Start()
	{
		foreach (Entity entity in GameState.Entities)
		{
			OnSpawn(entity);
		}
		Events.OnSpawn += OnSpawn;
	}

	public virtual void End()
	{
		foreach (Condition condition in ActiveConditions)
		{
			condition.End();
		}
		Events.OnSpawn -= OnSpawn;
	}

	public abstract void ApplyEffect(Entity entity);

	public virtual bool IsValidTarget(Entity entity)
	{
		return entity != Entity
			&& entity.IsAlive
			&& Entity.OccupyingLanes.Intersects(entity.OccupyingLanes)
			&& TargetMask.Matches(entity.Mask);
	}

	public virtual void OnSpawn(Entity entity)
	{
		if (IsValidTarget(entity))
		{
			ApplyEffect(entity);
		}
	}
}
