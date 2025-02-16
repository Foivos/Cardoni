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

		Spawn(1, 0, GD.Load<EntityData>("res://resources/entities/goblin_warrior.tres"));
		Spawn(1, 0, GD.Load<EntityData>("res://resources/entities/goblin_shaman.tres"));
		Spawn(3, 0, GD.Load<EntityData>("res://resources/entities/goblin_summoner.tres"));
		var friend = Spawn(1, 6000, GD.Load<EntityData>("res://resources/entities/goblin_warrior.tres"));
		friend.Mask = new EntityMask(new EntityMasks[] { EntityMasks.Friendly });
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
