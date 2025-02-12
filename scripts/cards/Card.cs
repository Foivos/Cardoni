namespace Cardoni;

using System;
using Godot;

public partial class Card : Node2D
{
	public int Index { get; set; }
	public string Description { get; set; }

	[Export]
	int manaCost;
	public int ManaCost
	{
		get { return manaCost; }
		set
		{
			manaCost = value;
			DisplayMana();
		}
	}

	public CardResult[] CardResults { get; set; }

	[Export]
	public Sprite2D MySprite { get; set; }

	[Export]
	public RichTextLabel MyText { get; set; }

	[Export]
	public RichTextLabel ManaLabel { get; set; }

	public override void _Ready()
	{
		DisplayMana();

		MyText.Text = ManaCost + "_" + Description;
	}

	void DisplayMana()
	{
		ManaLabel.Text = manaCost.ToString();
	}
}
