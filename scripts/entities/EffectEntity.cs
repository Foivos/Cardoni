using System;
using System.Collections.Generic;
using Godot;

namespace Cardoni;

public abstract partial class EffectEntity : Entity
{
	public EntityMask EffectMask { get; set; }

	public List<ICondition> ActiveConditions { get; set; } = new();

	public override void Spawn()
	{
		base.Spawn();
		foreach (Entity entity in GameState.Entities)
		{
			OnSpawn(entity);
		}
		Events.OnSpawn += OnSpawn;
	}

	public override void Kill()
	{
		base.Kill();
		foreach (ICondition condition in ActiveConditions)
		{
			condition.End();
		}
	}


	public abstract void ApplyEffect(Entity entity);

	public virtual bool IsValidTarget(Entity entity)
	{
		return entity != this
			&& entity.IsAlive
			&& OccupyingLanes.Intersects(entity.OccupyingLanes)
			&& EffectMask.Matches(entity.Mask);
	}

	public virtual void OnSpawn(Entity entity) {
		if (IsValidTarget(entity))
		{
			ApplyEffect(entity);
		}
	}
}
