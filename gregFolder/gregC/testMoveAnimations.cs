using System;
using Godot;

public partial class testMoveAnimations : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		side = Time.GetTicksMsec() % 2 == 0 ? 1 : -1;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Position += direction * new Vector2(0, moveSpeed) * (float)delta; //? move up and down
		if (Position.Y > moveLimits.Y)
			direction = -1;
		else if (Position.Y < moveLimits.X)
			direction = 1;

		moveTimer += delta;
		if (moveTimer < moveCooldown)
			return;
		moveTimer = 0;

		if (side != 1)
			side = 1;
		else
			side = -1;

		body.Offset = new Vector2(moveDistance, 0) * side;
		RotationDegrees = moveRotation * side;
	}

	int side = 1;

	[Export]
	float moveDistance = 0.3f;

	[Export]
	float moveRotation = 1;

	[Export]
	float moveCooldown = 0.3f;
	double moveTimer;

	[Export]
	Sprite2D body;

	[Export]
	Sprite2D shord;

	[Export]
	float moveSpeed;

	[Export]
	Vector2 moveLimits;
	int direction = 1;
}
