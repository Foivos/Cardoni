using Godot;
using System;

public partial class enemyC : RigidBody2D
{

	public new virtual void _Ready()
	{

	}

	
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
