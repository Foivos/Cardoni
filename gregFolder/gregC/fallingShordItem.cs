using System;
using Godot;

public partial class fallingShordItem : Sprite2D
{
	//! GENERALISE TO HAVE STRIKES AND EVERYTHING
	//todo   velocity , angular velocity, gravity,
	//todo   damping , angular damping
	//todo   texture color , scale, START rotation, position


	public void useMe(
		Vector2 pos,
		float _scale,
		Texture2D tex,
		int _degrees = default,
		Color _color = default,
		Vector2 _speed = default,
		float _rotationSpeed = default,
		float _gravity = default,
		float _speedBreak = default,
		float _rotationBreak = default
	)
	{
		if (_color != default)
			Modulate = _color;
		else
			Modulate = Colors.White;

		rotationSpeed = _rotationSpeed;

		rotationBreak = _rotationBreak;

		if (_rotationSpeed > 0)
			rotationSide = 1;
		else
		{
			rotationSide = -1;
			rotationSpeed *= -1;
		}

		speed = _speed;
		gravity = _gravity;
		speedBreak = _speedBreak;

		Position = pos;
		RotationDegrees = _degrees;

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

	float gravity = 900;
	Vector2 speed;
	float rotationSpeed;
	int rotationSide;

	float speedBreak = 400; //400
	float rotationBreak = 20; //40

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

		if (gravity > 0)
			speed.Y += gravity * (float)delta;

		Position += speed * (float)delta;

		ifOutClose();
	}

	void ifOutClose()
	{
		if (Position.X < -20 || Position.X > 400 || Position.Y > 900 || Position.Y < -20)
			closeMe();
	}
}
