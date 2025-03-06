namespace Cardoni;

using Godot;

public partial class Card : Node2D
{
	public int Index { get; set; } = -1;

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
			CardResult = value.CardResult;

			if (value.Sprite != null)
				value.Sprite.setUp(Sprite); // GREGORY
			// if (value.Sprite != null)
			// {
			// 	Sprite.Texture = value.Sprite.Texture;
			// 	Sprite.RegionRect = value.Sprite.RegionRect;
			// }
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

	public CardResult CardResult { get; set; }

	[Export]
	public Sprite2D Sprite { get; set; }

	[Export]
	public RichTextLabel Text { get; set; }

	[Export]
	public RichTextLabel ManaLabel { get; set; }

	[Export]
	Sprite2D ManaSprite { get; set; }

	void DisplayMana()
	{
		//if (ManaLabel != null)
		//	ManaLabel.Text = manaCost.ToString();

		if (ManaSprite != null)
		{
			ManaSprite.RegionEnabled = false;

			ManaSprite.Hframes = 4;
			ManaSprite.Vframes = 3;

			ManaSprite.Frame = manaCost;
			//GD.Print("ManaCost: " + manaCost);
		}
	}
}
