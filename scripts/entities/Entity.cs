namespace Cardoni;

using System;
using System.Collections.Generic;
using Godot;


public partial class Entity
{

	public static uint idCounter;
	public uint id;



	public string Name { get; set; }
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

	public uint BaseMovementSpeed { get; set; }
	public float MovementSpeedModifier { get; set; } = 1;
	public uint MovementSpeed => (uint)Math.Max(0, Math.Floor(BaseMovementSpeed * MovementSpeedModifier));

	public uint BaseAttackSpeed { get; set; }
	public float AttackSpeedModifier { get; set; } = 1;
	public uint AttackSpeed => (uint)Math.Max(0, Math.Floor(BaseAttackSpeed * AttackSpeedModifier));


	public int lookingDirection;//? never zero .. even when not moving
	int direction;

	public int Direction
	{
		get { return direction; }
		set
		{
			direction = value;

			if(value != 0)lookingDirection = value;

			if (Parent != null && Parent.Weapon != null) Parent.setShordPosition(this);


			//if (Parent != null && Parent.EnemyMove != null)
			//Parent.EnemyMove.SetProcess(direction != 0);
		}
	}


	public Effect[] Effects = new Effect[Enum.GetNames(typeof(EffectType)).Length];
	public List<ICondition> Conditions = new List<ICondition>();

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

	protected Entity()
	{
		idCounter += 2;
		id = idCounter;

		Name = Name + "_" + id;

		SpawnManager.Spawn(this);
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



		if (Health <= 0)
		{
			fallingShords.throwItem(Parent.Weapon);
			textEffects.displayDmgText(this, 0, ovveride: "DEAD");

			battleEffectsC.inst.EntityBlood(this);


			GameState.Kill(this);
		}
		else
		{
			fallingShords.testStrikeThere(Parent.Sprite.GlobalPosition);
			//battleEffectsC.doHitParticles(Parent.Sprite.GlobalPosition);

			battleEffectsC.inst.EntityBlood(this); //battleEffectsC.doBloodParticles(this);
			new battleEffectsC.hitDmg(Parent.Sprite);
			textEffects.displayDmgText(this, damage);
		}

	}

	public virtual void Move()
	{


		int dx = (int)MovementSpeed * Direction;
		if (dx == 0)
			return;

		Y += dx;

		Parent.processSideMove();
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
		Parent.QueueFree();
	}

	public virtual void Spawn() { }

	public int VerticalDistance(Entity target)
	{
		return Math.Abs(target.Y - Y) - target.Height;
	}
}
