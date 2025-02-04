using System;
using Godot;
using State;

public partial class Card : Node2D
{
	public int Index { get; set; }
	public string Description { get; set; }
	public int ManaCost { get; set; }

	public testTargetC.targetTypes CardTarget { get; set; }


	[Export] public Sprite2D mySprite { get; set; }
	[Export] public RichTextLabel myText { get; set; }

	public override void _Ready()
	{
		myText.Text = ManaCost + "_" + Description;
		SetPosition();
	}

	public void SetPosition() { }


}
