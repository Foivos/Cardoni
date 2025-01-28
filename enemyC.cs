using Godot;
using System;

public partial class enemyC : RigidBody2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		//QueueFree();

	}


	private void OnVisibleOnScreenNotifier2DScreenExited()
	{
		GD.Print("Screen exited DIE");
		QueueFree();
	}
	




}
