namespace Cardoni;

using System;
using Godot;

public partial class testEnemy : DamagingEntity
{
	[Export]
	Sprite2D Sword;

	public override void _Ready()
	{
		base._Ready();
		Health = 5;
		setUpHealthBar();
		displayShord();
		GameState.Entities.Add(this);
		foreach (Node child in GetChildren())
		{
			if (child is CollisionShape2D collision)
			{
				Shape = collision.Shape;
			}
		}
	}

	//public void _Process(float delta) { processMovement(); }

	public override void Damage(int amount)
	{
		base.Damage(amount);
		if (HealthBar != null)
			HealthBar.Value = Health;
		if (battleEffectsC.inst != null)
		{
			battleEffectsC.inst.addHitTwo(Sprite);
			battleEffectsC.inst.doShake(Sprite);
		}
	}

	void setUpHealthBar()
	{
		if (HealthBar != null)
		{
			HealthBar.MaxValue = Health;
			HealthBar.Value = Health;
			HealthBar.MinValue = 0;
		}
	}

	void displayHealthBar()
	{
		if (HealthBar == null)
			return;

		HealthBar.Value = Math.Clamp(Health, 5, HealthBar.Value - 5);

		HealthBar.Visible = true;
	}

	void displayShord()
	{
		if (Sword == null)
			return;
		if (AttackDamage == 0)
		{
			Sword.Visible = false;
			return;
		}

		Sword.Scale = Vector2.One;
	}
}
