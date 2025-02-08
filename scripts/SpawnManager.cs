namespace Cardoni;

using System;
using Godot;

public partial class SpawnManager : Node
{
	[Export]
	public Area2D GameView;

	[Export]
	public PackedScene EnemyPreffab { get; set; }

	public override void _Ready()
	{

		var mob1 = Spawn<testEnemy>();
		mob1.Position = new Vector2(Constants.GridWidth * ((int)1 - 1.5f), -300);
		mob1.Y = 0;
		mob1.Mask = 1;
		mob1.TargetMask = 2;
		mob1.Name = "Mob 1";
		mob1.MovementSpeed = 60;
		mob1.OccupyingLanes = 2;
		mob1.AttackSpeed = 30;
		mob1.Height = Constants.GridTicks / 2;
		mob1.Range = (uint)Constants.GridTicks / 2;
		mob1.AttackDamage = 1;


		var mob2 = Spawn<testEnemy>();
		mob2.Position = new Vector2(Constants.GridWidth * ((int)1 - 1.5f), -300);
		mob2.Y = Constants.TicksPerLane;
		mob2.Mask = 2;
		mob2.TargetMask = 1;
		mob2.Name = "Mob 2";
		mob2.MovementSpeed = 60;
		mob2.OccupyingLanes = 2;
		mob2.AttackSpeed = 30;
		mob2.Height = Constants.GridTicks / 2;
		mob2.Range = (uint)Constants.GridTicks / 2 * 4;
		mob2.AttackDamage = 1;
	}

	public T Spawn<T>() where T: Entity{

		EntityParent parent = EnemyPreffab.Instantiate<EntityParent>();
		T entity = Activator.CreateInstance<T>();

		entity.Parent = parent;

		entity.Spawn();

		parent.AddChild(entity);
		AddChild(parent);

		return entity;
	}

	void SpawnEnemy(uint lane)
	{
		testEnemy mob = EnemyPreffab.Instantiate<testEnemy>();

		mob.Position = new Vector2(Constants.GridWidth * ((int)lane - 1.5f), -300);

		// Add some randomness to the direction.
		//direction += (float)GD.RandRange(-Mathf.Pi / 4, Mathf.Pi / 4);
		//float direction = GD.RandRange(-45, 45);
		//mob.Rotation = direction;

		// Choose the velocity.
		//var velocity = new Vector2(0, -enemySpeed);//(float)GD.RandRange(2.0, 2.0)

		// Spawn the mob by adding it to the Main scene.
		AddChild(mob);
	}
}
