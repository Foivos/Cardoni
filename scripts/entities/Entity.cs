namespace Cardoni;

using System;
using System.Collections.Generic;
using Godot;
using Godot.Collections;

public partial class Entity : Node2D
{
	public EntityParent Parent { set; get; }
	public TextureProgressBar HealthBar => Parent.HealthBar;

	public Sprite2D Sprite => Parent.Sprite;

	EntityData data;
	public EntityData Data
	{
		get => data;
		set
		{
			data = value;
			if (value == null)
				return;

			HealthBar.MaxValue = value.MaxHealth;
		}
	}

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
			Parent.Position = Parent.Position with { X = ((value.To + value.From) / 2f - 1.5f) * Constants.GridWidth };
		}
	}

	public int Height { get; set; }
	public int Width { get; set; }

	int y;
	public int Y
	{
		get => y;
		set
		{
			y = value;
			Parent.Position = Parent.Position with
			{
				Y = (value - Constants.TicksPerLane / 2) * Constants.GridHeight / Constants.GridTicks,
			};
		}
	}

	public EntityMask Mask { get; set; }

	public EntityMask TargetMask { get; set; }

	public Characteristic Characteristic { get; set; }

	public Entity()
	{
		SpawnManager.Spawn(this);
		for (int i = 0; i < Effect.EffectTypes.Length; i++)
		{
			Effects[i] = (Effect)
				Effect.EffectTypes[i].GetConstructor(new Type[] { typeof(Entity) }).Invoke(new object[] { this });
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
		y = (y - Constants.TicksPerLane / 2) * Constants.GridHeight / Constants.GridTicks;
		Parent.Position = Parent.Position with { Y = y };
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

		Parent.QueueFree();
	}

	public virtual void Spawn()
	{
		Characteristic.Start();
	}

	public int VerticalDistance(Entity target)
	{
		return Math.Abs(target.Y - Y) - target.Height;
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
