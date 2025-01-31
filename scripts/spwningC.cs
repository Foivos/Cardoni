using Godot;
using System;

public partial class spwningC : Node
{


	[Export] public PackedScene enemyPreffab { get; set; }
	[Export] float spawnX;
	[Export] float enemySpeed;


	// public void NewGame()
	// {
	// _score = 0;

	// var player = GetNode<Player>("Player");
	// var startPosition = GetNode<Marker2D>("StartPosition");
	// player.Start(startPosition.Position);

	//GetNode<Timer>("StartTimer").Start();
	//}
	// GetNode<Timer>("MobTimer").Start();
	// GetNode<Timer>("ScoreTimer").Start();

	private void _on_spawn_timer_timeout()
	{


		spawnEnemy();
		GD.Print("spawned");
	}



	void spawnEnemy()
	{


		enemyC mob = enemyPreffab.Instantiate<enemyC>();

		// Choose a random location on Path2D.
		//var mobSpawnLocation = GetNode<PathFollow2D>("MobPath/MobSpawnLocation");
		//mobSpawnLocation.ProgressRatio = GD.Randf();

		// Set the mob's direction perpendicular to the path direction.
		//float direction = mobSpawnLocation.Rotation + Mathf.Pi / 2;



		// Set the mob's position to a random location.
		//mob.Position = mobSpawnLocation.Position;
		mob.Position = new Vector2(spawnX, 50);

		// Add some randomness to the direction.
		//direction += (float)GD.RandRange(-Mathf.Pi / 4, Mathf.Pi / 4);
		//float direction = GD.RandRange(-45, 45);
		//mob.Rotation = direction;

		// Choose the velocity.
		//var velocity = new Vector2(0, -enemySpeed);//(float)GD.RandRange(2.0, 2.0)
		mob.LinearVelocity = new Vector2(0, -enemySpeed);
		mob.GravityScale = 0;
	
	

		// Spawn the mob by adding it to the Main scene.
		AddChild(mob);

	}



}
