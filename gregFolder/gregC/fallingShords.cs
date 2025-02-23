using System;
using System.Collections.Generic;
using Cardoni;
using Godot;

public partial class fallingShords : Node
{
	//todo change Name

	static fallingShords inst;

	public override void _Ready()
	{
		inst = this;
	}

	List<fallingShordItem> fallingItems;

	fallingShordItem getFallingItem()
	{
		if (fallingItems == null)
			fallingItems = new List<fallingShordItem>();

		for (int i = 0; i < fallingItems.Count; i++)
		{
			if (fallingItems[i].Visible == false)
				return fallingItems[i];
		}

		//? HERE SPAWNS MORE
		fallingShordItem tempItem = null;
		for (int i = 0; i < 5; i++)
		{
			tempItem = new fallingShordItem();
			//tempItem = fallingItemPreffab.Instantiate<fallingShordItem>();
			tempItem.closeMe();

			fallingItems.Add(tempItem);

			AddChild(tempItem);
		}

		return tempItem;
	}

	public override void _Process(double delta)
	{
		if (fallingItems == null)
			return;

		for (int i = 0; i < fallingItems.Count; i++)
		{
			if (fallingItems[i].Visible == false)
				continue;
			fallingItems[i].processMe((float)delta);
		}
	}

	ulong lastInput;

	public override void _Input(InputEvent @event)
	{
		//return;

		// if (Time.GetTicksMsec() - lastInput < 200) return;
		// lastInput = Time.GetTicksMsec();


		// if (@event is InputEventMouseButton == false) return;
		// throwItem(testSprite);
	}

	public static void throwItem(Sprite2D thatOne)
	{
		if (inst == null)
			return;

		Color color = new Color(0.8f, 0.8f, 0.8f);
		const int gravity = 900; //10000
		const int startRotationSpeed = 50; //60

		const int speedBreak = 400; //400
		const int rotationBreak = 20; //40

		const int speedX = 300;
		const int speedY = -200;

		inst.getFallingItem()
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

	public static void testStrikeThere(Entity ent)
	{
		const int speedValue = 1200; //1200;
		const int offsetValue = 20;
		const float lifetime = 0.04f; //; 0.04f;

		int sideX = 1.rDir();
		//int sideY = ent.lookingDirection;

		Vector2 pos = ent.Sprite.GlobalPosition;
		Vector2 offset = -new Vector2(offsetValue * sideX, offsetValue); //* sideY
		Vector2 speed = new Vector2(speedValue * sideX, speedValue); //* sideY

		testStrike(pos + offset, speed, -45 * sideX, lifetime, Colors.Yellow);
	}

	static void testStrike(Vector2 pos, Vector2 speed, int degrees, float lifeTime, Color color)
	{
		if (inst == null)
			return;
		if (inst.strikeTexture == null)
			inst.strikeTexture = GD.Load<Texture2D>("res://resources/hollowStrikeMedioumOutline.png");

		const float size = 0.6f;

		var item = inst.getFallingItem();
		item.useMe(pos, size, inst.strikeTexture, _degrees: degrees, _speed: speed, _color: color);

		new battleEffectsC.invisibleLater(item, lifeTime);
	}
}
