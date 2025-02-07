namespace Cardoni;

using System;
using System.Collections.Generic;
using Godot;

public partial class spawning : Node
{
	//[Export]public Area2D GameView;


	static spawning inst;


	[Export] public PackedScene EnemyPreffab { get; set; }
	[Export] public PackedScene minionPreffab { get; set; }


	[Export]
	float spawnX;
	[Export] float minionY;


	[Export] bool overrideEnemySpeed;
	[Export]
	float enemySpeed;

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



		new Expiring(10, calculateAtacksAndTraffic, 0);


		inst = this;
		new Expiring(
			20,
			new Action(() =>
			{
				SpawnEnemy(0);
				new Expiring(
					40,
					new Action(() =>
					{
						uint[] lanes = { 0, 2, 1, 3 };
						uint lane = lanes[(GameState.Instance.Tick - 20 / 40) % 4];
						SpawnEnemy(lane);
					}),
					0
				);
			})
		);

		for (uint i = 0; i < 4; i++)
		{
			enemies[i] = new List<Enemy>();
			minions[i] = new List<monsterBase>();
		}


		spawnMinion(0);
		spawnMinion(3);
	}


	List<Enemy>[] enemies = new List<Enemy>[4];
	List<monsterBase>[] minions = new List<monsterBase>[4];
	public static void monsterDied(monsterBase monster)
	{
		if (inst == null) return;

		if (monster is Enemy) inst.enemies[monster.lane].Remove((Enemy)monster);
		else inst.minions[monster.lane].Remove(monster);



	}


	void SpawnEnemy(uint lane)
	{

		Enemy enm = EnemyPreffab.Instantiate<Enemy>();
		//mob.GameView = GameView;

		enm.Position = new Vector2(spawnX * (2 * lane + 1), 50);
		enm.lane = lane;

		if (overrideEnemySpeed) enm.speed = enemySpeed;
		AddChild(enm);
		if (enemies[lane] == null) enemies[lane] = new List<Enemy>();
		enemies[enm.lane].Add(enm);

		// Add some randomness to the direction.
		//direction += (float)GD.RandRange(-Mathf.Pi / 4, Mathf.Pi / 4);
		//float direction = GD.RandRange(-45, 45);
		//mob.Rotation = direction;

		// Choose the velocity.
		//var velocity = new Vector2(0, -enemySpeed);//(float)GD.RandRange(2.0, 2.0)

		// mob.LinearVelocity = new Vector2(0, -enemySpeed);
		// mob.GravityScale = 0;

		// Spawn the mob by adding it to the Main scene.


	}


	void spawnMinion(uint lane)
	{
		monsterBase minion = minionPreffab.Instantiate<monsterBase>();


		minion.Position = new Vector2(spawnX * (2 * lane + 1), minionY);
		minion.lane = lane;


		AddChild(minion);
		//
		if (minions[lane] == null) minions[lane] = new List<monsterBase>();
		//GD.Print("is null ==" +(minions[lane] == null));
		//GD.Print("minion null ==" +minion == null);
		minions[lane].Add(minion);

	}



	[Export] int atackRange;
	[Export] int atackPlayerRange;
	

	void calculateAtacksAndTraffic()
	{


		void calculateEnemyTraffic(uint lane)
		{
			if (enemies[lane] == null) return;
			if (enemies[lane].Count < 2) return;

			for (int i = enemies[lane].Count - 1; i >= 1; i--)
			{
				enemies[lane][i].stopMoving = enemies[lane][i].Position.Y < enemies[lane][i - 1].Position.Y - atackRange;

			}


		}

		void calculateAtacking(uint lane)
		{

			if (enemies[lane] == null) return;
			if (enemies[lane].Count == 0) return;
			// { if (minions[lane].Count != 0) minions[lane][0].isAtacking = false; }

			bool atack;

			if (minions[lane].Count == 0) enemies[lane][0].isAtacking = enemies[lane][0].Position.Y > atackPlayerRange;
			else
			{
				atack = enemies[lane][0].Position.Y > minions[lane][0].Position.Y - atackRange;

				enemies[lane][0].isAtacking = atack;
				minions[lane][0].isAtacking = atack;

				if (atack)
				{
					enemies[lane][0].target = minions[lane][0];
					minions[lane][0].target = enemies[lane][0];
				}

			}



		}

		void checkEnemyAtackPlayer(monsterBase enm)
		{

			// if (enm.Position.Y > player.Position.Y - enm.atackRange)
			// {
			// 	enm.isAtacking = true;
			// 	enm.target = player;
			// }
			// else enm.isAtacking = false;



		}



		void calculateAtackingNew(uint lane)
		{


			for (int i = 0; i < enemies[lane].Count; i++) { enemies[lane][i].rangeCalulations(); }
			for (int i = 0; i < minions[lane].Count; i++) { minions[lane][i].rangeCalulations(); }



		}


		for (uint i = 0; i < 4; i++)
		{

			//calculateEnemyTraffic(i);
			calculateAtackingNew(i);




		}





	}






}
