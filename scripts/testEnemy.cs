namespace Cardoni;

using System;
using Godot;

public partial class testEnemy : Area2D
{
	//[Export] RigidBody2D rb;
	[Export]TextureProgressBar hpBar;
	

	[Export]Sprite2D sprite;
	

	public override void _Ready()
	{
		setUpHealthBar();
		displayShord();
	}

	//public void _Process(float delta) { processMovement(); }




	public override void _Process(double delta)
	{
		processMovement(delta);
		// 	displayHealthBar();

		//GD.Print("enemyC _Process");
	}

	#region  TAKE DAMAGE + HEALTH BAR

	[Export]
	uint hp;

	public void onDamage(uint amount)
	{
		hp -= amount;

		if (hp <= 0)
			QueueFree();
		else
		{
			if (hpBar != null)
				hpBar.Value = hp;
			battleEffectsC.inst.addHitTwo(sprite);
			battleEffectsC.inst.doShake(sprite);
		}

		GD.Print("dmg: " + amount + " HP: " + hp);
	}

	void setUpHealthBar()
	{
		if (hpBar != null)
		{
			hpBar.MaxValue = hp;
			hpBar.Value = hp;
			hpBar.MinValue = 0;
		}
	}

	void displayHealthBar()
	{
		if (hpBar == null)
			return;

		hpBar.Value = Math.Clamp(hp, 5, hpBar.Value - 5);

		hpBar.Visible = true;
	}

	#endregion


	#region  shord that dynamicaly changes with atack damage

	[ExportGroup("shord dmg assumed 1-10")]
	[Export]
	uint _atackDamage;

	uint atackDamage
	{ // ? the fact that im here i mean this >> { << is a gift to you foivos ( if this is not the case sorry my formatter changed it .. )
		get { return _atackDamage; }
		set
		{
			_atackDamage = value;
			displayShord();
		}
	}

	[Export]
	Sprite2D shord;

	[Export]
	Vector2 sizes;

	void displayShord()
	{
		if (shord == null)
			return;
		if (_atackDamage == 0)
		{
			shord.Visible = false;
			return;
		}

		shord.Scale = Vector2.One * Mathf.Lerp(sizes.X, sizes.Y, _atackDamage / 10f);
	}

	#endregion



	#region  MOVEMENT




	[ExportGroup("movement")]
	[Export]
	public bool stopMoving = true;

	[Export]
	float speed;

	const float downLimit = 200; // after that die and deal damage for now

	public void processMovement(double delta) // every frame called
	{
		// if (stopMoving || )
		// 	return;

		// Position += new Vector2(0, speed) * (float)delta;

		// if (Position.Y < downLimit)
		// 	return;

		// testPlayer.damage(1);
		// QueueFree();
	}



	#endregion





	public void atackTest() //! DONT DELETE DONT DELETE
	{
		var spaceState = GetWorld2D().DirectSpaceState;

		// Create a circular shape
		CircleShape2D shape = new CircleShape2D();
		shape.Radius = 20;

		PhysicsShapeQueryParameters2D parameters = new PhysicsShapeQueryParameters2D();
		parameters.SetShape(shape);
		parameters.Transform = new Transform2D(0, GlobalPosition + new Vector2(0, -20));

		var transform = new Transform2D(0, GlobalPosition);

		var result = spaceState.IntersectShape(parameters); //.IntersectShape(shape, transform);

		if (result.Count > 0)
		{
			GD.Print("Found colliders: " + result.Count);
		}
	}
}
