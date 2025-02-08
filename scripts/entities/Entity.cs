namespace Cardoni;

using System;
using System.Collections.Generic;
using Godot;

public partial class Entity : Node2D
{
	public EntityParent Parent { set; get; }
	public TextureProgressBar HealthBar => Parent.HealthBar;

	public Sprite2D Sprite => Parent.Sprite;

	uint maxHealth;
	public virtual uint MaxHealth
	{
		get { return maxHealth; }
		set
		{
			maxHealth = value;
			HealthBar.MaxValue = value;
		}
	}
	int health;
	public virtual int Health
	{
		get { return health; }
		set
		{
			health = value;
			HealthBar.Value = value;
		}
	}

	public int MovementSpeed { get; set; }

	public int Direction { get; set; }

	public Effect[] Effects = new Effect[Enum.GetNames(typeof(EffectType)).Length];
	public List<ICondition> Conditions = new List<ICondition>();

	public Shape2D Shape { get; set; }

	public bool IsAlive => Health > 0;

	public uint OccupyingLanes { get; set; }

	public int Height { get; set; }
	public int Width { get; set; }

	public int Y { get; set; }

	public uint Mask { get; set; }

	public virtual void Damage(int damage)
	{
		Health -= damage;
		GD.Print(Name, " damaged at: ", GameState.Tick, " currecnt health is ", Health);

		if (Health <= 0)
		{
			Kill();
		}
	}

	public virtual void Move()
	{
		int dx = MovementSpeed * Direction;
		if (dx == 0)
			return;
		Y += dx;
	}

	public virtual void UpdatePosition(float dt)
	{
		int dx = MovementSpeed * Direction;
		if (dx == 0)
			return;

		float y = Y + dt * dx;
		y = (y - Constants.TicksPerLane / 2) * Constants.GridHeight / Constants.GridTicks;
		Parent.Position = Parent.Position with { Y = y };
	}

	public virtual void Kill()
	{
		foreach (Effect effect in Effects)
		{
			if (effect == null)
			{
				continue;
			}
			effect.End();
		}
		foreach (ICondition condiiton in Conditions)
		{
			condiiton.End();
		}
		GameState.Entities.Remove(this);
		Parent.QueueFree();
	}

	public virtual void Spawn() { }

	public int VerticalDistance(Entity target)
	{
		return Math.Abs(target.Y - Y) - target.Height;
	}
}
