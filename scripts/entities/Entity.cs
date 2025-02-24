namespace Cardoni;

using System;
using Godot;

public partial class Entity : Node2D
{
	[Export]
	public TextureProgressBar HealthBar { get; set; }

	[Export]
	public Sprite2D Sprite { get; set; }

	[Export]
	public Sprite2D Weapon { get; set; }

	[Export]
	public Label HealthLabel { get; set; }

	public EntityData Data { get; set; }

	public uint MaxHealth => Data.MaxHealth;
	int health;
	public virtual int Health
	{
		get { return health; }
		set
		{
			health = value;
			HealthLabel.Text = value.ToString();
			//HealthBar.Value = value;
		}
	}

	public uint BaseMovementSpeed => Data.BaseMovementSpeed;
	public float MovementSpeedModifier { get; set; } = 1;
	public uint MovementSpeed => (uint)Math.Max(0, Math.Floor(BaseMovementSpeed * MovementSpeedModifier));

	public uint BaseAttackSpeed => Data.BaseAttackSpeed;
	public float AttackSpeedModifier { get; set; } = 1;
	public uint AttackSpeed => (uint)Math.Max(0, Math.Floor(BaseAttackSpeed * AttackSpeedModifier));

	public int FacingDirection { get; set; } //? never zero .. even when not moving
	int direction;

	public int Direction
	{
		get { return direction; }
		set
		{
			direction = value;

			if (value != 0)
				FacingDirection = value;

			if (Weapon != null)
				SetWeaponPosition(this);
		}
	}

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
		if (Health <= 0 || IsAlive == false)
		{
			GD.PushError("DAMAGE Entity ALREADY DEAD");
			return;
		}

		Health -= damage;
		GD.Print(Name, " damaged at: ", GameState.Tick, " currecnt health is ", Health);

		SpecialState.EntityBlood(this);
		if (Health <= 0)
		{
			fallingShords.throwItem(Weapon);
			textEffects.displayDmgText(this, 0, ovveride: "DEAD");

			GameState.Kill(this);
		}
		else
		{
			fallingShords.testStrikeThere(this);

			SpecialState.HitDamage(Sprite);
			textEffects.displayDmgText(this, damage);
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

		ProcessSideMove();
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

	public void SetWeaponPosition(Entity entity)
	{
		if (Weapon == null)
			return;

		bool lookingDown = entity.FacingDirection == 1;

		// if (enytity.Direction != 0) lookingDown = enytity.Direction == 1;
		// else if (enytity is AttackingEntity atacker && atacker.Target != null)
		// {
		// 	lookingDown = atacker.Target.Parent.Sprite.GlobalPosition.Y
		// > enytity.Parent.Sprite.GlobalPosition.Y;
		// }
		// else GD.PushError("NEED BETTER WAY OF KNOWING DIRECTION");

		if (lookingDown)
			Weapon.RotationDegrees = -135;
		else
			Weapon.RotationDegrees = -45;

		Vector2 shordPosition = new Vector2(23, 23);
		if (!lookingDown)
			shordPosition.Y *= -1;
		Weapon.Position = shordPosition;
	}

	public void ProcessSideMove() { }
}
