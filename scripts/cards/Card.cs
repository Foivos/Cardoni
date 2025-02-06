namespace Cardoni;

using System;
using Godot;

public partial class Card : Node2D
{
	public int Index { get; set; }
	public string Description { get; set; }


	[Export] int _ManaCost;
	public int ManaCost
	{
		get { return _ManaCost; }
		set
		{
			_ManaCost = value;
			displayMana();
		}
	}

	void displayMana() { manaLabel.Text = _ManaCost.ToString(); }


	public CardTarget[] CardTargets { get; set; }

	[Export]
	public Sprite2D mySprite { get; set; }

	[Export] public Sprite2D mySprite { get; set; }
	[Export] public RichTextLabel myText { get; set; }
	[Export] RichTextLabel manaLabel;

	public override void _Ready()
	{



		_ManaCost = ManaCost;
		displayMana();

		myText.Text = ManaCost + "_" + Description;
	}
}
