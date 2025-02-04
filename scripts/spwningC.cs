using Godot;
using System;

public partial class spwningC : Node
{

	[Export] public Area2D GameView;
	[Export] public PackedScene EnemyPreffab { get; set; }
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

	public override void _Ready()
	{
		new Expiring(20, new System.Action<uint>((uint tick) => {
			SpawnEnemy(0);
			new Expiring(40, new System.Action<uint>((uint tick) => {
				uint[] lanes = {0, 2, 1, 3};
				uint lane = lanes[((tick - 20) / 40) % 4];
				SpawnEnemy(lane);
			}), 3);
		}));
	}



	void SpawnEnemy(uint lane)
	{
		Enemy mob = EnemyPreffab.Instantiate<Enemy>();
		mob.GameView = GameView;

		mob.Position = new Vector2(spawnX * (2 * lane + 1), 50);

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
