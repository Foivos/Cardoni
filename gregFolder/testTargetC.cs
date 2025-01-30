using Godot;
using System;

public partial class testTargetC : Node
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		screenSize = GetViewport().GetVisibleRect().Size;

		backround.GlobalPosition = new Vector2(screenSize.X / 2f, screenSize.Y / 2f);
		screenBlock = screenSize.X / 4;

	}
	[Export] Vector2 screenSize;
	[Export] float screenBlock;


	[Export] Node2D backround;
	[Export] Node2D targetGraphic;
	[Export] Vector2 mousePos;
	public override void _Process(double delta)
	{

		mousePos = GetViewport().GetMousePosition();
		targetGraphic.GlobalPosition = gridTargetPosition(GetViewport().GetMousePosition());



	}
	Vector2 gridTargetPosition(Vector2 pos)
	{
		pos -= Vector2.One * screenBlock / 2;


		
		pos /= screenBlock;// new Vector2(pos.X / screenBlock, pos.Y / screenBlock);
		pos.X = (float)Math.Round(pos.X);
		pos.Y = (float)Math.Round(pos.Y);

		return pos * screenBlock + Vector2.One * screenBlock / 2;
	}

	//GetViewport().GetMousePosition();



}