using System;
using Godot;

public partial class fallingShordItem : Sprite2D
{
	public void useMe(Vector2 pos, float _scale, Texture2D tex, Color _color = default)
	{
		if (_color != default)
			Modulate = _color;
		else
			Modulate = color;

		speed = startSpeed;
		if (Random.Shared.NextDouble() > 0.5)
			speed.X *= -1;

		rotationSide = 1;
		if (Random.Shared.NextDouble() > 0.5)
			rotationSide *= -1;

		rotationSpeed = startRotationSpeed;
		Position = pos;

		Scale = new Vector2(_scale, _scale);
		Texture = tex;

		Visible = true;
		SetProcess(true);
	}

	public void closeMe()
	{
		Visible = false;
		SetProcess(false);
	}

	public Color color = new Color(0.8f, 0.8f, 0.8f);
	public float gravity = 900; //10000
	public float startRotationSpeed = 50; //60
	public Vector2 startSpeed = new Vector2(300, -200); //300 -200
	public float speedBreak = 400; //400
	public float rotationBreak = 20; //40

	//? temporary values
	Vector2 speed;
	float rotationSpeed;
	int rotationSide;

	public void processMe(float delta)
	{
		if (rotationSpeed > 0)
			rotationSpeed -= rotationBreak * delta;
		else
			rotationSpeed = 0;

		if (speed.X > 10)
			speed.X -= speedBreak * delta;
		else if (speed.X < -10)
			speed.X += speedBreak * delta;

		Rotation += rotationSide * rotationSpeed * (float)delta;

		speed.Y += gravity * (float)delta;
		Position += speed * (float)delta;

		if (Position.Y > 900)
			closeMe();
	}
}
