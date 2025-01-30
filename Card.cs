using Godot;
using System;

public partial class Card : Area2D
{
	public uint Index;
	public bool selected;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SetPosition();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void SetPosition() {
		Vector2 screenSize = GetViewport().GetVisibleRect().Size;
		Position = new Vector2(screenSize.X * (2 * Index + 1) / 8, screenSize.Y / 2);
	}
}
