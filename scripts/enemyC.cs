namespace Cardoni;

using System;
using Godot;

public partial class enemyC : Entity
{
	//[Export] RigidBody2D rb;
	[Export]
	TextureProgressBar HealthBar;

	[Export]
	Sprite2D Sprite;

	public override void _Ready()
	{
		Health = 5;
		setUpHealthBar();
		displayShord();
		GameState.Instance.Entities.Add(this);
		foreach (Node child in GetChildren())
		{
			if (child is CollisionShape2D)
			{
				Shape = ((CollisionShape2D)child).Shape;
			}
		}
	}

	//public void _Process(float delta) { processMovement(); }




	public override void _Process(double delta)
	{
		processMovement(delta);
		// 	displayHealthBar();

		//GD.Print("enemyC _Process");
	}

	#region  TAKE DAMAGE + HEALTH BAR

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

	const float downLimit = 300; // after that die and deal damage for now

	public void processMovement(double delta) // every frame called
	{
		if (stopMoving)
			return;

		Position += new Vector2(0, speed) * (float)delta;

		if (Position.Y < downLimit)
			return;

		testPlayer.damage(1);
		GameState.Instance.Entities.Remove(this);
		QueueFree();
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
