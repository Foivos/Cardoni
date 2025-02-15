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
	}

	public static void Spawn(Entity entity)
	{
		EntityParent parent = Instance.EnemyPreffab.Instantiate<EntityParent>();

		entity.Parent = parent;

		Instance.AddChild(parent);

		GameState.Spawning.Add(entity);
	}

	public static void Spawn(uint lane, int y, EntityData data)
	{
		throw new NotImplementedException();
	}
}
