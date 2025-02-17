namespace Cardoni;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using Godot;
using Godot.Collections;

public partial class Entity : Node2D
{
	[Export]
	public TextureProgressBar HealthBar { get; set; }

	[Export]
	public Sprite2D Sprite { get; set; }
	public EntityData Data { get; set; }

	public uint MaxHealth => Data.MaxHealth;
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

	public uint BaseMovementSpeed => Data.BaseMovementSpeed;
	public float MovementSpeedModifier { get; set; } = 1;
	public uint MovementSpeed => (uint)Math.Max(0, Math.Floor(BaseMovementSpeed * MovementSpeedModifier));

	public uint BaseAttackSpeed => Data.BaseAttackSpeed;
	public float AttackSpeedModifier { get; set; } = 1;
	public uint AttackSpeed => (uint)Math.Max(0, Math.Floor(BaseAttackSpeed * AttackSpeedModifier));

	public int Direction { get; set; }

	public Effect[] Effects = new Effect[Enum.GetNames<EffectType>().Length];

	public Shape2D Shape { get; set; }

	public bool IsAlive => Health > 0;

	OccupyingLanes occupyingLanes;
	public OccupyingLanes OccupyingLanes
	{
		get => occupyingLanes;
		set
		{
			occupyingLanes = value;
			Position = Position with { X = ((value.To + value.From) / 2f - 1.5f) * Constants.GridWidth };
		}
	}

	public uint Height => Data.Height;
	public uint Width => Data.Width;

	int y;
	public int Y
	{
		get => y;
		set
		{
			y = value;
			Position = Position with
			{
				Y = ((value - Constants.TicksPerLane / 2f) / Constants.GridTicks + 0.5f) * Constants.GridHeight,
			};
		}
	}

	public EntityMask Mask { get; set; }

	public Characteristic Characteristic { get; set; }

	public Entity()
	{
		for (int i = 0; i < Effect.EffectTypes.Length; i++)
		{
			Type effectType = Effect.EffectTypes[i];
			Effects[i] = (Effect)Activator.CreateInstance(effectType, this);
		}
	}

	public void SetData(EntityData data, int lane, int y)
	{
		Data = data;
		if (data == null)
			return;

		HealthBar.MaxValue = data.MaxHealth;
		Health = (int)data.MaxHealth;
		OccupyingLanes = new OccupyingLanes(lane, (int)(lane + data.Width - 1));
		Y = y;
		Mask = data.Mask;
		if (data.Sprite != null)
		{
			Sprite.Texture = data.Sprite.Texture;
			Sprite.RegionRect = data.Sprite.RegionRect;
		}
	}

	public override void _Ready()
	{
		base._Ready();
		Characteristic = Data.Characteristic?.Create(this);
	}

	public virtual void Damage(int damage)
	{
		Health -= damage;
		GD.Print(Name, " damaged at: ", GameState.Tick, " currecnt health is ", Health);

		if (Health <= 0)
		{
			GameState.Kill(this);
		}
	}

	public virtual void DamageTyped(DamageTypes damageType, int damage)
	{
		DamageType.DealDamage(this, damageType, damage);
	}

	public virtual void Move()
	{
		int dx = (int)MovementSpeed * Direction;
		if (dx == 0)
			return;
		Y += dx;
	}

	public virtual void UpdatePosition(float dt)
	{
		int dx = (int)MovementSpeed * Direction;
		if (dx == 0)
			return;

		float y = Y + dt * dx;
		y = ((y - Constants.TicksPerLane / 2f) / Constants.GridTicks + 0.5f) * Constants.GridHeight;
		Position = Position with { Y = y };
	}

	public virtual void Kill()
	{
		Characteristic?.End();
		foreach (Effect effect in Effects)
		{
			if (effect == null)
			{
				continue;
			}
			effect.End();
		}

		QueueFree();
	}

	public virtual void Spawn()
	{
		Characteristic?.Start();
	}

	public int VerticalDistance(Entity target)
	{
		return Math.Abs(target.Y - Y) - (int)(target.Height + Height) / 2;
	}

	public int DistanceSquared(Entity target)
	{
		int VerticalDistance = target.Y - Y;
		int HorizontalDistance =
			Math.Max(
				0,
				Math.Max(OccupyingLanes.From - target.OccupyingLanes.To, target.OccupyingLanes.From - OccupyingLanes.To)
			) * Constants.GridTicks;
		return VerticalDistance * VerticalDistance + HorizontalDistance * HorizontalDistance;
	}

	public double Distance(Entity target)
	{
		return Math.Sqrt((float)DistanceSquared(target));
	}

	internal bool Affected(EffectType effectType)
	{
		Effect effect = Effects[(int)effectType];
		return effect != null && effect.Active;
	}

	internal T GetEffect<T>()
		where T : Effect
	{
		EffectType effectType = (EffectType)typeof(T).GetField("Type").GetValue(null);

		return (T)Effects[(int)effectType];
	}
}
