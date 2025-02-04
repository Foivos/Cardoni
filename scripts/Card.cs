using System;
using Godot;
using State;

public partial class Card : Area2D
{
	public uint Index;
	public bool selected;
	public string description;
	public int manaCost;
	[Export] public Sprite2D mySprite;
	[Export] public RichTextLabel myText;

	public override void _Ready() { SetPosition(); }



	public void SetPosition()
	{
		Vector2 screenSize = GetViewport().GetVisibleRect().Size;
		Position = new Vector2(screenSize.X * (2.0f * Index - 3.0f) / 8, 0);
	}



}
