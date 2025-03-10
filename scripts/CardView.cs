namespace Cardoni;

using System.Collections.Generic;
using System.Linq;
using Godot;

public partial class CardView : Node2D
{
	public static CardView Instance;

	[Export]
	public PackedScene CardAreaScene { get; set; }

	[Export]
	public PackedScene CardsScene { get; set; }

	Card[] Cards = new Card[4];
	CardArea[] CardAreas = new CardArea[4];

	[Export] public CardData[] deck;// GREGORY

	[Export]
	public Godot.Collections.Array<int> drawPile;
	//List<int> drawPile;
	void StartSetDeck()
	{
		drawPile = new();
		for (int i = 4; i < deck.Length; i++)
		{
			drawPile.Add(i);
		}
		GD.Print("drawPile.Count: " + drawPile.Count);

		for (int i = 0; i < 4; i++)
		{

			drawCard(deck[i], i);
		}



		suffleDrawPile();


	}
	public void CardPlayed(Card card)
	{
		return;// FOR EDIT TO REPLAY SAME CARD

		Cards[card.Index] = null;
		card.QueueFree();

		requestDrawCard();
		//drawCard();

	}

	bool handFull()
	{
		if (Cards[0] != null &&
					Cards[1] != null &&
					Cards[2] != null &&
					Cards[3] != null) return true;
		return false;

	}

	ulong lastCardDrawTime;
	void requestDrawCard()
	{

		if (handFull()) return;

		ulong minDrawTimeDistance = 300;
		if (Time.GetTicksMsec() < lastCardDrawTime + minDrawTimeDistance)
		{

			float diff = (lastCardDrawTime + minDrawTimeDistance) / 1000f;
			new ProcessExpiring(diff, () =>
				{
					drawCard();
				}, 1);

			lastCardDrawTime = lastCardDrawTime + minDrawTimeDistance;

		}
		else { drawCard(); lastCardDrawTime = Time.GetTicksMsec(); }
	}
	void drawCard()
	{

		if (handFull()) return;

		if (drawPile.Count == 0)
		{
			if (Cards[0] == null &&
				Cards[1] == null &&
				Cards[2] == null &&
				Cards[3] == null) resetDrawPile();

			return;

		}

		int firstEmpty()
		{
			for (int i = 0; i < 4; i++)
			{
				if (Cards[i] == null)
					return i;
			}
			{ GD.PushError("didnt find first empty"); return 0; }
		}

		drawCard(deck[drawPile[0]], firstEmpty());
		drawPile.RemoveAt(0);




	}

	void drawCardEffect(Card card)
	{
		float delay = 0.3f;
		int offsetValue = 8;
		Vector2I offset = new Vector2I(offsetValue.rDir(), offsetValue.rDir());
		int rotation = 4.rDir();
		Color color = Colors.Gray;

		card.Sprite.Modulate = color;
		card.Sprite.Offset = offset;
		card.RotationDegrees = rotation;


		new ProcessExpiring(delay, () =>
					{
						if (!IsInstanceValid(card)) return;

						card.Modulate = Colors.White;
						card.Sprite.Offset = Vector2.Zero;
						card.RotationDegrees = 0;
					}, 1);



	}


	void resetDrawPile()// also draws 4 cards
	{

		drawPile.Clear();

		for (int i = 0; i < deck.Length; i++)
		{
			drawPile.Add(i);
		}

		suffleDrawPile();

		requestDrawCard();
		requestDrawCard();
		requestDrawCard();
		requestDrawCard();

		// drawCard();
		// drawCard();
		// drawCard();
		// drawCard();




	}
	void suffleDrawPile()
	{
		int a, b, holder;
		for (int i = 0; i < 10; i++)
		{
			a = (int)GD.RandRange(0, drawPile.Count - 1);
			b = (int)GD.RandRange(0, drawPile.Count - 1);
			holder = drawPile[a];
			drawPile[a] = drawPile[b];
			drawPile[b] = holder;
		}

	}

	void drawCard(CardData data, int index)
	{
		Card card = CardsScene.Instantiate<Card>();
		card.Index = index;

		card.Data = data;

		Cards[index] = card;
		CardAreas[index].AddChild(card);

	}





	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

		// "arrow_turret", //net
		//greg/spawnWarrior



		Instance = this;



		//string[] names = new string[] { "greg/pushCard", "earthquake", "toxic_fumes", "slowing_field" };
		for (int i = 0; i < 4; i++)
		{


			//Card card = CardsScene.Instantiate<Card>();
			//card.Index = i;

			//card.Data = GD.Load<CardData>("res://resources/cards/" + names[i] + ".tres");
			//card.Data = deck[i];

			//Cards[i] = card;

			CardArea cardArea = CardAreaScene.Instantiate<CardArea>();
			cardArea.CardView = this;
			cardArea.Index = i;
			cardArea.Position = new Vector2(100 * i - 150, 0);

			//cardArea.AddChild(card);

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
