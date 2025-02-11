using System;
using Godot;

namespace Cardoni;

public abstract partial class EffectEntity : Entity
{
    public EntityMask EffectMask { get; set; }

	public override void Spawn()
	{
		base.Spawn();
        foreach (Entity entity in GameState.Entities) {
            if (IsValidTarget(entity))
            {  
                ApplyEffect(entity);
            }
        }
        Events.OnSpawn += ApplyEffect;
	}

	public override void Kill()
	{
		base.Kill();
        foreach (Entity entity in GameState.Entities) {
            RemoveEffect(entity);
        }
	}

	public abstract void ApplyEffect(Entity entity);
	public abstract void RemoveEffect(Entity entity);

	public virtual bool IsValidTarget(Entity entity) {
        return entity != this
			&& entity.IsAlive
			&& (entity.OccupyingLanes & OccupyingLanes) != 0
			&& EffectMask.Matches(entity.Mask);
    }
}
