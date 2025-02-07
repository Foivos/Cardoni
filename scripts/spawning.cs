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






	public override void _Ready()
	{



		new Expiring(5, calculateAtacksAndTraffic, 0);



		inst = this;
		new Expiring(
			20,
			new System.Action<uint>(
				(uint tick) =>
				{
					SpawnEnemy(0);
					new Expiring(
						40,
						new System.Action<uint>(
							(uint tick) =>
							{
								uint[] lanes = { 0, 2, 1, 3 };
								uint lane = lanes[((tick - 20) / 40) % 4];
								SpawnEnemy(lane);
							}
						),
						3
					);
				}
			)
		);

		for (uint i = 0; i < 4; i++)
		{
			enemies[i] = new List<Enemy>();
			minions[i] = new List<minnionBase>();
		}


		spawnMinion(0);
		spawnMinion(3);
	}


	List<Enemy>[] enemies = new List<Enemy>[4];
	List<minnionBase>[] minions = new List<minnionBase>[4];
	public static void monsterDied(monsterBase monster)
	{
		if (inst == null) return;

		if (monster is Enemy) inst.enemies[monster.lane].Remove((Enemy)monster);
		else inst.minions[monster.lane].Remove((minnionBase)monster);



	}


	void SpawnEnemy(uint lane)
	{

		Enemy enm = EnemyPreffab.Instantiate<Enemy>();
		//mob.GameView = GameView;

		enm.Position = new Vector2(spawnX * (2 * lane + 1), 50);
		enm.lane = lane;

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
		minnionBase minion = minionPreffab.Instantiate<minnionBase>();


		minion.Position = new Vector2(spawnX * (2 * lane + 1), minionY);
		minion.lane = lane;


		AddChild(minion);
		//
		if (minions[lane] == null) minions[lane] = new List<minnionBase>();
		//GD.Print("is null ==" +(minions[lane] == null));
		//GD.Print("minion null ==" +minion == null);
		minions[lane].Add(minion);

	}




	[Export] int atackPlayerRange;

	[Export] minnionBase player;
	void calculateAtacksAndTraffic(uint useless)
	{


		// calculates wich enemies are in range and should atack assigns targets to them
		void calculateEnemiesAtacking(uint lane)//! doesnt yet acount for possibility of minion to be spawned above enemy .. 
		{

			//bool enemies;
			bool minionAlive = minions[lane].Count > 0;
			int topMinion = (int)player.Position.Y;

			for (int i = 0; i < minions[lane].Count; i++)
			{
				if (minions[lane][i].Position.Y < topMinion) topMinion = (int)minions[lane][i].Position.Y;
			}


			for (int i = 0; i < enemies[lane].Count; i++)
			{

				if (enemies[lane][i].Position.Y > topMinion - enemies[lane][i].atackRange)
				{
					enemies[lane][i].isAtacking = true;
					if (minionAlive) enemies[lane][i].target = minions[lane][0];
					else enemies[lane][i].target = player;

				}
				else enemies[lane][i].isAtacking = false;


			}







		}

		void calculateMinionsAtacking(uint lane)
		{

			bool enemiesAlive = enemies[lane].Count > 0;
			int bottomEnemy = -200;

			for (int i = 0; i < enemies[lane].Count; i++)
			{
				if (enemies[lane][i].Position.Y > bottomEnemy) bottomEnemy = (int)enemies[lane][i].Position.Y;
			}


			for (int i = 0; i < minions[lane].Count; i++)
			{

				if (enemiesAlive == false) { minions[lane][i].isAtacking = false; minions[lane][i].target = null; continue; }

				if (minions[lane][i].Position.Y < 	 + minions[lane][i].atackRange)
				{

					//GD.Print("minion atacking");
					minions[lane][i].isAtacking = true;
					minions[lane][i].target = enemies[lane][0];


				}
				else { minions[lane][i].isAtacking = false; }//GD.Print("minion NOT");


			}



		}


		for (uint i = 0; i < 4; i++)
		{


			calculateEnemiesAtacking(i);
			calculateMinionsAtacking(i);



		}





	}






}
