using System;
using Godot;

public partial class enemyC : Node2D
{





	[Export] Sprite2D sprite;
	public override void _Ready() { setUpHealthBar(); }
	//public void _Process(float delta) { processMovement(); }




	public override void _Process(double delta)
	{
		processMovement( delta);
		// 	displayHealthBar();

		//GD.Print("enemyC _Process");
	}
	




	#region  TAKE DAMAGE + HEALTH BAR

	//[Export] RigidBody2D rb;
	[Export] TextureProgressBar hpBar;
	[Export] int hp;
	public void onDamage(int amount)
	{
		hp -= amount;


		if (hp <= 0) QueueFree();
		else
		{
			if (hpBar != null) hpBar.Value = hp;
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
		if (hpBar == null) return;




		hpBar.Value = Math.Clamp(hp, 5, hpBar.Value - 5);

		hpBar.Visible = true;


	}



	#endregion



	#region  MOVEMENT




	[ExportGroup("movement")]
	[Export] public bool stopMoving = true;

	[Export] float speed;

	const float downLimit = 200;// after that die and deal damage for now
	public void processMovement(double delta) // every frame called
	{

		if (stopMoving) return;

		Position += new Vector2(0, speed) * (float)delta;



		if (Position.Y < downLimit) return;

		testPlayer.damage(1);
		QueueFree();


	}



	#endregion





	public void atackTest()//! DONT DELETE DONT DELETE
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
