namespace Cardoni;

using Godot;

public partial class TestEntity : Node2D
{
	[Export]
	EntityData data1;

	[Export]
	EntityData data2;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SpawnManager.Spawn(1, 0, data1);
		SpawnManager.Spawn(1, 6000, data2);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) { }
}
