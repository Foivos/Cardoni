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

		var friend1 = new GoblinSiege(1, Constants.TicksPerLane)
		{
			Mask = new EntityMask(new EntityMasks[] { EntityMasks.Friendly }),
			TargetMask = new EntityMask(new EntityMasks[] { EntityMasks.Enemy }),
		};
		var friend2 = new GoblinShaman(1, Constants.TicksPerLane)
		{
			Mask = new EntityMask(new EntityMasks[] { EntityMasks.Friendly }),
			TargetMask = new EntityMask(new EntityMasks[] { EntityMasks.Friendly }),
		};
		var enemy1 = new GoblinWarrior(1);
		var enemy2 = new GoblinRanged(2);
	}

	public static void Spawn(Entity entity)
	{
		EntityParent parent = Instance.EnemyPreffab.Instantiate<EntityParent>();

		entity.Parent = parent;

		Instance.AddChild(parent);

		GameState.Spawning.Add(entity);
	}
}
