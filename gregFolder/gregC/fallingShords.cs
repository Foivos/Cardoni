using Godot;
using System;

public partial class fallingShords : Node
{

	static fallingShords inst;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready() { inst = this; }



	[Export] Sprite2D testSprite;


	[Export] PackedScene fallingPreffab;
	[Export] int rotation;
	[Export] float gravity;
	[Export] float speed;
	[Export] float sizeAdjust;


	// public override void _Input(InputEvent @event)
	// {
	// 	//return;
	// 	if (@event is InputEventMouseButton == false) return;
	// 	throwItem(testSprite);

	// }



	public static void throwItem(Sprite2D thatOne)
	{
		GD.Print("throwItem " + thatOne == null);
		GD.Print("throwItem");
		GD.Print("throwItem");
		GD.Print("throwItem");
		if (inst == null || thatOne == null) return; inst.throwItemLocal(thatOne);
	}



	public void throwItemLocal(Sprite2D thatOne)
	{


		fallingShordItem item = fallingPreffab.Instantiate<fallingShordItem>();
		AddChild(item);

		item.sprite.Texture = thatOne.Texture;
		item.Position = thatOne.GlobalPosition;
		item.Scale = thatOne.Scale * sizeAdjust;
		//item.Scale = thatOne.GlobalTransform.Scale;

		GD.Print(thatOne.GlobalPosition + "__" + thatOne.Scale);
		//+ "__" + thatOne.GlobalTransform.Scale + "__" + thatOne.Texture);


		item.GravityScale = gravity;
		item.LinearVelocity = new Vector2(0.5f, -0.5f) * speed;
		item.AngularVelocity = rotation;





	}





}
