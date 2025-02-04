
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
		

		for (int i = 0; i < 4; i++)
		{
			Card card = CardsScene.Instantiate<Card>();
			card.Index = i;
			card.description = "Card " + i;
			Cards[i] = card;

			CardArea cardArea = CardAreaScene.Instantiate<CardArea>();
			cardArea.CardView = this;
			cardArea.Index = i;
			cardArea.Position = new Vector2(100 * (int) i - 150, 0);

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
				GD.Print(index);
				GameState.SelectedCard = Cards[index];
			}
			else
			{
				GD.Print(index);
				GameState.SelectedCard = null;
			}
		}
	}

	public void _MouseEntered() { }

	public void _MouseExited()
	{
	}
}
