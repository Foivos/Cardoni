using System;
using System.Collections.Generic;
using Cardoni;
using Godot;

public partial class GravityBodyView : Node
{
	static GravityBodyView inst;

	public override void _Ready()
	{
		inst = this;
	}

	public static void returnBody(GravityBody body)
	{
		inst.GravityBodyPool.Remove(body);
	}

	public static GravityBody getGravityBody()
	{
		return inst.GravityBodyPool.Get();
	}

	NodePool<GravityBody> GravityBodyPool { get; set; } =
		new()
		{
			Instantiate = () =>
			{
				GravityBody body = new GravityBody();
				inst.AddChild(body);
				return body;
			},
			Initialize = body =>
			{
				body.Visible = true;
				body.SetProcess(true);
			},
			Deactivate = body =>
			{
				body.Visible = false;
				body.SetProcess(false);
			},
		};

	public static void throwItem(Sprite2D thatOne)
	{
		if (inst == null)
		{
			return;
		} //GD.Print("null throwItem");

		Color color = new Color(0.8f, 0.8f, 0.8f);
		const int gravity = 900; //10000
		const int startRotationSpeed = 50; //60

		const int speedBreak = 400; //400
		const int rotationBreak = 20; //40

		const int speedX = 300;
		const int speedY = -200;

		inst.GravityBodyPool.Get()
			.useMe(
				thatOne.GlobalPosition,
				thatOne.Scale.X,
				thatOne.Texture,
				_gravity: gravity,
				_speed: new Vector2(speedX.rDir(), speedY),
				_rotationSpeed: startRotationSpeed.rDir(),
				_speedBreak: speedBreak,
				_rotationBreak: rotationBreak,
				_color: color
			);
	}

	Texture2D strikeTexture;

	public static void EntityStrike(Entity ent)
	{
		const int speedValue = 1200; //1200;
		const int offsetValue = 20;
		const float lifetime = 0.04f; //; 0.04f;

		int sideX = 1.rDir();
		//int sideY = ent.lookingDirection;

		Vector2 pos = ent.Sprite.GlobalPosition;
		Vector2 offset = -new Vector2(offsetValue * sideX, offsetValue); //* sideY
		Vector2 speed = new Vector2(speedValue * sideX, speedValue); //* sideY

		Strike(pos + offset, speed, -45 * sideX, lifetime, Colors.Yellow);
	}

	static void Strike(Vector2 pos, Vector2 speed, int degrees, float lifeTime, Color color)
	{
		if (inst == null)
			return;
		if (inst.strikeTexture == null)
			inst.strikeTexture = GD.Load<Texture2D>("res://resources/hollowStrikeMedioumOutline.png");

		const float size = 0.6f;

		var item = getGravityBody();
		item.useMe(pos, size, inst.strikeTexture, _degrees: degrees, _speed: speed, _color: color);

		//new battleEffectsC.invisibleLater(item, lifeTime);
		SpecialState.closeGravityBody(item, lifeTime);
	}
}
