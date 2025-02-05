namespace Cardoni;

using System;
using Godot;

public partial class Card : Node2D
{
	public int Index { get; set; }
	public string Description { get; set; }
	public int ManaCost { get; set; }

	public CardTarget[] CardTargets { get; set; }

	[Export] public Sprite2D mySprite { get; set; }
	[Export] public RichTextLabel myText { get; set; }

	public override void _Ready()
	{
		myText.Text = ManaCost + "_" + Description;
	}
}
