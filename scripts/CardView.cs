namespace Cardoni;

using System;
using Godot;

public partial class CardView : Node2D
{
	static CardView inst;

	[Export]
	public PackedScene CardAreaScene { get; set; }

	[Export]
	public PackedScene CardsScene { get; set; }

	Card[] Cards = new Card[4];
	CardArea[] CardAreas = new CardArea[4];

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		inst = this;

		for (int i = 0; i < 4; i++)
		{
			Card card = CardsScene.Instantiate<Card>();
			card.Index = i;
			card.Description = "Card " + i;

			card.CardTargets = new CardTarget[1];
			card.CardTargets[0] = new PatternTarget(new string[] { "XXX", "XXX", "XXX" }, false);

			card.CardTargets[0].CardEffects = new Affecting[]{ new AffectingDamage(1) };

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
