using System;
using System.Collections.Generic;
using Godot;

public partial class tileAnim : TextureRect
{
	//! KEEP IN MIND THE TILE ANIM MATERIAL IS WORKING


	[Export]
	int frame;

	[Export]
	float cooldown;
	double timer;

	[Export]
	Texture2D[] framesArray;

	public override void _Process(double delta)
	{
		timer += delta;
		if (timer < cooldown)
			return;
		{
			timer = 0;

			frame++;
			if (frame >= framesArray.Length)
				frame = 0;
			Texture = framesArray[frame];
		}
	}
}
