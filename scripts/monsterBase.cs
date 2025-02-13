using System;
using Cardoni;
using Godot;

public partial class monsterBase : Node2D
{
	[Export]
	Sprite2D sprite;
	public uint lane;

	public override void _Ready()
	{
		if (healthBar != null)
		{
			healthBar.MaxValue = hp;
			healthBar.MinValue = 0;
		}

		hp = _hp;
		attackDamage = _attackDamage;
	}

	public override void _Process(double delta)
	{
		if (isAtacking)
			atack(delta);
	}

	#region  TAKE DAMAGE + HEALTH BAR + DEATH

	[Export]
	public TextureProgressBar healthBar;

	[Export]
	uint _hp;
	uint hp
	{
		get { return _hp; }
		set
		{
			_hp = value;
			if (healthBar == null)
			{
				GD.Print("no health bar " + Name);
				return;
			}
			if (_hp == 0)
				return;

			healthBar.Value = _hp;
		}
	}

	public virtual void onDamage(uint amount)
	{
		hp = hp - amount;

		if (hp <= 0)
			onDeath();
		else
		{
			new battleEffectsC.hitDmg(sprite);
			battleEffectsC.inst.doShake(sprite);

			GD.Print("dmg: " + amount + " HP: " + hp);
		}
	}

	public void onDeath()
	{


		fallingShords.throwItem(shord);
		// spawning.monsterDied(this);
		QueueFree();
	}

	#endregion


	#region  shord that dynamicaly changes with atack damage

	[ExportGroup("shord dmg assumed 1-10")]
	[Export]
	uint _attackDamage;

	uint attackDamage
	{ // ? the fact that im here i mean this >> { << is a gift to you foivos ( if this is not the case sorry my formatter changed it .. )
		get { return _attackDamage; }
		set
		{
			_attackDamage = value;
			if (shord == null)
			{
				GD.Print("no shord " + Name);
				return;
			}

			if (_attackDamage == 0)
			{
				shord.Visible = false;
				return;
			}

			shord.Scale = Vector2.One * Mathf.Lerp(sizes.X, sizes.Y, _attackDamage / 10f);
		}
	}

	[Export]
	Sprite2D shord;

	[Export]
	Vector2 sizes;

	#endregion


	#region  ATACKING  test version  controlled by spawner for now ..


	public bool isAtacking;

	[Export]
	bool isRanged;

	[Export]
	float _atackRange;
	const int defaultMeeleRange = 100;
	public float atackRange
	{
		get
		{
			if (isRanged == false)
				return defaultMeeleRange;
			return _atackRange;
		}
		private set { }
	}

	[Export]
	float atackCooldown;
	double atackTimer;
	public monsterBase target;

	public virtual void atack(double time)
	{
		if (isAtacking == false)
			return;

		atackTimer += time;
		if (atackTimer < atackCooldown)
			return;
		atackTimer = 0;

		if (isRanged)
		{
			if (this is Enemy)
				testShooter.shootMinions(Position, (int)attackDamage);
			else
				testShooter.shootEnemies(Position, (int)attackDamage);
		}
		else if (target != null && target.hp > 0)
			target.onDamage(attackDamage);
	}

	#endregion
}
