namespace Cardoni;

using System;
using Godot;

public partial class SpawnManager : Node
{
	[Export]
	public Area2D GameView;

	[Export]
	public PackedScene EnemyPreffab { get; set; }

	public static SpawnManager Instance;

	public override void _Ready()
	{
		Instance = this;

		Spawn(0, 0, GD.Load<EntityData>("res://resources/entities/goblin_summoner.tres"));
		Spawn(1, 0, GD.Load<EntityData>("res://resources/entities/goblin_summoner.tres"));
		Spawn(2, 0, GD.Load<EntityData>("res://resources/entities/goblin_summoner.tres"));
		Spawn(3, 0, GD.Load<EntityData>("res://resources/entities/goblin_summoner.tres"));

		Spawn(0, 6000, GD.Load<EntityData>("res://resources/entities/player.tres"));
	}

	public static Entity Spawn(int lane, int y, EntityData data)
	{
		Entity entity = Instance.EnemyPreffab.Instantiate<Entity>();

		entity.SetData(data, lane, y);

		Instance.AddChild(entity);

		GameState.Spawning.Add(entity);

		return entity;
	}
}
