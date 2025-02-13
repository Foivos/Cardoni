namespace Cardoni;

using System.Linq;
using Godot;

public partial class CardView : Node2D
{
	static CardView Instance;

	[Export]
	public PackedScene CardAreaScene { get; set; }

	[Export]
	public PackedScene CardsScene { get; set; }

	Card[] Cards = new Card[4];
	CardArea[] CardAreas = new CardArea[4];

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Instance = this;

		for (int i = 0; i < 4; i++)
		{
			Card card = CardsScene.Instantiate<Card>();
			card.Index = i;
			card.Description = "Card " + i;

			card.CardResults = new CardResult[1];
			//var target = new PatternTarget(new string[] { " X ", "XXX" }, false);
			//var effect = new CardEffectPoison(100, 60);
			//card.CardResults[0] = new EffectResult(new EntityTarget[]{target}.ToList(), new EntityActive[]{effect}.ToList());
			card.CardResults[0] = new BaseProjectileResult(new EntityActive[] { new CardEffectPoison(30, 40) })
			{
				Piercing = 2,
			};

			Cards[i] = card;

			CardArea cardArea = CardAreaScene.Instantiate<CardArea>();
			cardArea.CardView = this;
			cardArea.Index = i;
			cardArea.Position = new Vector2(100 * (int)i - 150, 0);

			cardArea.AddChild(card);

			CardAreas[i] = cardArea;
			AddChild(cardArea);
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) { }

	public void EventInput(int index, InputEvent @event)
	{
		if (@event is InputEventMouseButton eventMouseButton)
		{
			if (eventMouseButton.ButtonIndex != MouseButton.Left)
				return;

			if (eventMouseButton.Pressed)
			{
				GameState.SelectedCard = Cards[index];
			}
			else
			{
				GameState.SelectedCard = null;
			}
		}
	}

	public void _MouseEntered() { }

	public void _MouseExited() { }
}
