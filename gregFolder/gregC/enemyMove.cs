using Cardoni;
using Godot;
using System;

public partial class enemyMove : Sprite2D
{

	//! moved to entity parent this is not used anymore
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		side = Time.GetTicksMsec() % 2 == 0 ? 1 : -1;

	}


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		moveTimer += delta;
		if (moveTimer < moveCooldown) return;
		moveTimer = 0;

		if (side != 1) side = 1;
		else side = -1;


		Offset = new Vector2(moveDistance, 0) * side;
		RotationDegrees = moveRotation * side;



	}

	int side = 1;
	float moveDistance = 0.3f;
	float moveRotation = 1;
	float moveCooldown = 0.3f;
	double moveTimer;



}
