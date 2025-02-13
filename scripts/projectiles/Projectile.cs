namespace Cardoni;

using System;
using System.Collections.Generic;
using Godot;

public partial class Projectile {
    public Sprite2D Sprite { get; } = new Sprite2D();

    public uint Piercing { get; set; } = 1;

    public EntityActive[] Actives { get; set; }

    public EntityMask Mask { get; set; } = new EntityMask();

    int y;
    public int Y { get => y; set {
        if (value < 0) {
            Destroy();
            return;
        }
        y = value;
        Sprite.Position = Sprite.Position with { Y = (value - Constants.TicksPerLane / 2) * Constants.GridHeight / Constants.GridTicks };
    } }

    uint lane;
    public uint Lane { get => lane; set {
        lane = value;
        Sprite.Position = Sprite.Position with { X = (value - 1.5f) * Constants.GridWidth };
    } }

    public int MovementSpeed { get; set; }

    List<Entity> AlreadyHit { get; } = new();

    public Projectile() {
        GameView.Instance.AddChild(Sprite);
        GameState.SpawningProjectiles.Add(this);
    }

    public void Move() {
        Y += MovementSpeed;
    }

    public Entity Colliding() {
        foreach (Entity entity in GameState.Entities) {
            if (!IsValidTarget(entity)) {
                continue;
            }
            if (VerticalDistance(entity) <= 0) {
                
                return entity;
            }
        }
        return null;
    }

    public virtual void Hit(Entity entity) {
        AlreadyHit.Add(entity);
        foreach (EntityActive active in Actives) {
            active.Activate(entity);
        }
        if (Piercing == 0) {
            return;
        } else if (Piercing == 1) {
            Destroy();
        } else {
            Piercing -= 1;
        }
    }

	private void Destroy()
	{
		GameState.DyingProjectiles.Add(this);
        Sprite.QueueFree();
	}

	protected virtual bool IsValidTarget(Entity entity)
	{
		return Mask.Matches(entity.Mask)
            && entity.OccupyingLanes.IsIn(Lane)
            && AlreadyHit.IndexOf(entity) == -1;
	}

	protected virtual int VerticalDistance(Entity target)
	{
		return (int) Math.Abs(target.Y - Y) - target.Height;
	}
}