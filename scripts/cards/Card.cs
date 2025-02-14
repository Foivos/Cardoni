namespace Cardoni;

using Godot;

public partial class Card : Node2D
{
	public int Index { get; set; }

	public CardData Data
	{
		get => null;
		set
		{
			if (value == null)
				return;
			Name = value.Name;
			ManaCost = value.ManaCost;
			Description = value.Description;
			if (value.Sprite != null)
				Sprite.Texture = value.Sprite;
			CardResults = value.CardResults;
		}
	}

	string description;
	public string Description
	{
		get => description;
		set
		{
			description = value;
			Text.Text = value;
		}
	}

	int manaCost;

	[Export]
	public int ManaCost
	{
		get { return manaCost; }
		set
		{
			manaCost = value;
			DisplayMana();
		}
	}

	[Export]
	public CardResult[] CardResults { get; set; }

	[Export]
	public Sprite2D Sprite { get; set; }

	[Export]
	public RichTextLabel Text { get; set; }

	[Export]
	public RichTextLabel ManaLabel { get; set; }

	void DisplayMana()
	{
		ManaLabel.Text = manaCost.ToString();
	}
}
