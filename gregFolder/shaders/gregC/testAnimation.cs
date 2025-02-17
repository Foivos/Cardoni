using Godot;
using System;

public partial class testAnimation : Sprite2D
{
	

	
	public override void _Process(double delta)
	{

		timer += delta;
		if (timer < delay) return;

		timer = 0;
		frameNow++;

		if (frameNow < frameCount) setFrame(frameNow);
		else { Visible = false; SetProcess(false); }//if(frameNow == frameCount)



	}


	void setFrame(int frame)
	{


		Vector2 textureSize = Texture.GetSize();

		// Calculate the size of each frame (cell) in the spritesheet
		Vector2 frameSize = new Vector2(textureSize.X / columns, textureSize.Y / rows);

		// Calculate the column and row for the current frame
		int frameColumn = frame % columns;
		int frameRow = frame / columns;

		// Define the region rectangle for the selected frame
		Rect2 regionRect = new Rect2(
			frameColumn * frameSize.X,  // X position in the texture
			frameRow * frameSize.Y,     // Y position in the texture
			frameSize.X,                // Width of each frame
			frameSize.Y                 // Height of each frame
		);

		// Set the region rectangle on the sprite
		RegionRect = regionRect;


	}


	public void playAnimation(Texture2D Texture, int frameCount, int rows, int columns
	, double delay, Vector2 pos , int rotation = 0 , float Scale = 1 , int Z = 0)
	{
		this.Texture = Texture;

		this.frameCount = frameCount;
		this.rows = rows;
		this.columns = columns;
		this.delay = delay;

		RegionEnabled = true;
		Position = pos;
		Visible = true;
		SetProcess(true);

		frameNow = 0;
		setFrame(frameNow);

		RotationDegrees = rotation;
		this.Scale = Vector2.One * Scale;

		ZIndex = Z;

	}

	int frameCount, rows, columns;
	double delay;
	double timer;
	int frameNow;



}
