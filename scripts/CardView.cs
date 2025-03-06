namespace Cardoni;

using System.Collections.Generic;
using System.Linq;
using Godot;
using Godot.Collections;

public partial class CardView : Node2D
{
	public static CardView Instance;

	[Export]
	public PackedScene CardAreaScene { get; set; }

	[Export]
	public PackedScene CardsScene { get; set; }

	Card[] Cards = new Card[4];
	CardArea[] CardAreas = new CardArea[4];

	[Export]
	public Array<CardData> DeckData;

	public List<Card> Deck { get; set; } = new();

	public List<Card> DrawPile { get; set; } = new();

	long lastCardDrawTime = -2000;

	void StartSetDeck()
	{
		for (int i = 0; i < DeckData.Count(); i++)
		{
			Card card = CardsScene.Instantiate<Card>();
			card.Data = DeckData[i];
			Deck.Add(card);
		}

		ResetDrawPile();

		for (int i = 0; i < 4; i++)
		{
			RequestDrawCard(i);
		}
	}

	public void CardPlayed(Card card)
	{
		CardAreas[card.Index].RemoveChild(card);

		int index = card.Index;
		card.Index = -1;
		Cards[index] = null;

		RequestDrawCard(index);
	}

	bool HandFull()
	{
		if (Cards[0] != null && Cards[1] != null && Cards[2] != null && Cards[3] != null)
			return true;
		return false;
	}

	int FirstEmptyCard()
	{
		for (int i = 0; i < 4; i++)
		{
			if (Cards[i] == null)
				return i;
		}

		return -1;
	}

	void RequestDrawCard(int drawTo)
	{
		long minDrawTimeDistance = 180;
		long now = (long)Time.GetTicksMsec();
		if (now < lastCardDrawTime + minDrawTimeDistance)
		{
			float diff = (lastCardDrawTime + minDrawTimeDistance - now) / 1000f;
			new ProcessExpiring(
				diff,
				() =>
				{
					DrawCard(drawTo);
				},
				1
			);

			lastCardDrawTime = lastCardDrawTime + minDrawTimeDistance;
		}
		else
		{
			DrawCard(drawTo);
			lastCardDrawTime = now;
		}
	}

	void DrawCard(int drawTo)
	{
		if (Cards[drawTo] != null)
			return;

		if (DrawPile.Count == 0)
			ResetDrawPile();

		Card card = DrawPile[0];
		DrawPile.RemoveAt(0);
		DrawCard(card, drawTo);
	}

	void DrawCard(Card card, int index)
	{
		Cards[index] = card;
		CardAreas[index].AddChild(card);
		card.Index = index;

		drawCardEffect(card);
	}

	void drawCardEffect(Card card)
	{
		float delay = 0.18f;
		int offsetValue = 4;
		Vector2I offset = new Vector2I(offsetValue.rDir(), offsetValue.rDir());
		int rotation = 1.rDir();
		Color color = Colors.Gray;

		card.Sprite.Modulate = color;
		card.Sprite.Offset = offset;
		card.RotationDegrees = rotation;

		new ProcessExpiring(
			delay,
			() =>
			{
				if (!IsInstanceValid(card))
					return;

				card.Modulate = Colors.White;
				card.Sprite.Offset = Vector2.Zero;
				card.RotationDegrees = 0;
			},
			1
		);
	}

	void ResetDrawPile()
	{
		DrawPile = Deck.Where((card) => card.Index == -1).ToList();

		ShuffleDrawPile();
	}

	void ShuffleDrawPile()
	{
		for (int i = 0; i < DrawPile.Count; i++)
		{
			int j = i + (int)(GD.Randi() % (DrawPile.Count - i));
			if (j == i)
				continue;
			Card temp = DrawPile[i];
			DrawPile[i] = DrawPile[j];
			DrawPile[j] = temp;
		}
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Instance = this;

		for (int i = 0; i < 4; i++)
		{
			CardArea cardArea = CardAreaScene.Instantiate<CardArea>();
			cardArea.CardView = this;
			cardArea.Index = i;
			cardArea.Position = new Vector2(100 * i - 150, 0);

			CardAreas[i] = cardArea;
			AddChild(cardArea);
		}

		StartSetDeck();
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
