using Godot;
using System;


public partial class fallingShordItem : Sprite2D
{


	public void useMe(Vector2 pos, float _scale, Texture2D tex)
	{
		speed = startSpeed;
		if (Random.Shared.NextDouble() > 0.5) speed.X *= -1;

		rotationSide = 1;
		if (Random.Shared.NextDouble() > 0.5) rotationSide *= -1;

		rotationSpeed = startRotationSpeed;
		Position = pos;

		Scale = new Vector2(_scale, _scale);
		Texture = tex;

		Visible = true;
		SetProcess(true);

	}
	public void closeMe() { Visible = false; SetProcess(false); }


	[Export] float gravity;
	[Export] float startRotationSpeed;
	[Export] Vector2 startSpeed;
	[Export] float speedBreak;
	[Export] float rotationBreak;

	Vector2 speed;
	float rotationSpeed;
	int rotationSide;

	public void processMe(float delta)
	{

		if (rotationSpeed > 0) rotationSpeed -= rotationBreak * delta;
		else rotationSpeed = 0;

		if (speed.X > 10) speed.X -= speedBreak * delta;
		else if (speed.X < -10) speed.X += speedBreak * delta;




		Rotation += rotationSide * rotationSpeed * (float)delta;

		speed.Y += gravity * (float)delta;
		Position += speed * (float)delta;


		if (Position.Y > 900) closeMe();

	}




}
