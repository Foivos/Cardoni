using Godot;
using System;

public partial class CardView : Node
{
	
	[Export]
	public PackedScene CardsScene { get; set; }
	public int SelectedCardIndex = - 1;
	
	Card[] cards = new Card[4];
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		for (uint i = 0; i<4; i++) {
			Card card = CardsScene.Instantiate<Card>();
			card.Index = i;
			cards[i] = card;
			AddChild(card);
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseButton eventMouseButton)
		{
			if (eventMouseButton.Pressed)
			{
				Vector2 screenSize = GetViewport().GetVisibleRect().Size;
				SelectedCardIndex = (int)(eventMouseButton.Position.X * 4 / screenSize.X);
				cards[SelectedCardIndex].Position = eventMouseButton.Position;
			} 
			else 
			{
				if (SelectedCardIndex > -1)
				{
					cards[SelectedCardIndex].SetPosition();
				}
				SelectedCardIndex = -1;
			}
		}
		else if (@event is InputEventMouseMotion eventMouseMotion)
		{
			if (SelectedCardIndex > -1)
			{
				cards[SelectedCardIndex].Position = eventMouseMotion.Position;
			}
		}

		// Print the size of the viewport.
		//GD.Print("Viewport Resolution is: ", GetViewport().GetVisibleRect().Size);
	}
}
