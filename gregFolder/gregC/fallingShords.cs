using Godot;
using System;
using System.Collections.Generic;

public partial class fallingShords : Node
{

	static fallingShords inst;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready() { inst = this; }


	[Export] Sprite2D testSprite;
	//[Export] PackedScene fallingItemPreffab;
	List<fallingShordItem> fallingItems;
	fallingShordItem getFallingItem()
	{

		if (fallingItems == null) fallingItems = new List<fallingShordItem>();

		for (int i = 0; i < fallingItems.Count; i++)
		{
			if (fallingItems[i].Visible == false) return fallingItems[i];

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


		if (fallingItems == null) return;

		for (int i = 0; i < fallingItems.Count; i++)
		{
			if (fallingItems[i].Visible == false) continue;
			fallingItems[i].processMe((float)delta);

		}




	}



	//[Export] PackedScene fallingPreffab;


	ulong lastInput;
	public override void _Input(InputEvent @event)
	{
		//return;

		if (Time.GetTicksMsec() - lastInput < 200) return;
		lastInput = Time.GetTicksMsec();


		if (@event is InputEventMouseButton == false) return;
		throwItem(testSprite);

	}



	public static void throwItem(Sprite2D thatOne) { if (inst == null) return; inst.throwItemLocal(thatOne); }



	public void throwItemLocal(Sprite2D thatOne)
	{


		//fallingShordItem item = fallingPreffab.Instantiate<fallingShordItem>();
		//AddChild(item);

		getFallingItem().useMe(thatOne.GlobalPosition, thatOne.Scale.X, thatOne.Texture
		, default);

		// resetSpeed();
		// testFallingShord.Visible = true;
		// testFallingShord.Texture = thatOne.Texture;
		// testFallingShord.Position = thatOne.GlobalPosition;
		// testFallingShord.Scale = thatOne.Scale * sizeAdjust;



		GD.Print(thatOne.GlobalPosition + "__" + thatOne.Scale);






	}





}
