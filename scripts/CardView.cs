using System;
using Godot;
using State;

public partial class CardView : Area2D
{


	#region  GREG TESTS



	static CardView inst;
	[Export] Node2D[] cardFathers;



	public static void displayCard(Card card)
	{

		if (inst == null) { GD.Print("why null sir"); return; }
		//card.GetNode<Sprite2D>("Sprite").Visible = true;//!

		if (card.GetParent() != null) card.GetParent().RemoveChild(card);
		inst.cardFathers[card.Index].AddChild(card);
		card.Position = Vector2.Zero;// inst.cardPositionsArray[card.Index];

		card.mySprite.Visible = true;
		card.myText.Text = card.manaCost + "_" + card.description;



		//inst.cardsLabels[card.Index].GetParent().RemoveChild(inst.cardsLabels[card.Index]);
		//card.AddChild(inst.cardsLabels[card.Index]);


		//card.AddChild(inst.cardsLabels[card.Index]);
		//inst.cardsLabels[card.Index].Position = inst.labelOffset;


	}
	public static void hideCard(Card card)//todo
	{
		if (inst == null) { GD.Print("why null sir"); return; }

		if (card.GetParent() != null) card.GetParent().RemoveChild(card);
		card.mySprite.Visible = false;
		card.myText.Text = "disabled";
		
		card.Position = new Vector2(-1000, -1000);
		

	}


	void startCloseTemplates()
	{
		Sprite2D img;
		for (int i = 0; i < cardFathers.Length; i++)
		{
			img = cardFathers[i].GetNode<Sprite2D>("template");
			if (img != null) img.Visible = false;


		}


	}






	#endregion





	[Export]
	public PackedScene CardsScene { get; set; }
	public int SelectedCardIndex = -1;

	Card[] cards = new Card[4];

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{



		inst = this;
		startCloseTemplates();

		for (uint i = 0; i < 3; i++)
		{
			Card card = CardsScene.Instantiate<Card>();
			card.Index = i;
			card.description = "Card " + i;
			cards[i] = card;

			//AddChild(card);//? needed ???????

			displayCard(card);//.SetPosition();
		}

    Card[] cards = new Card[4];

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void _Input(Node viewport, InputEvent @event, long shapeIdx)
	{
		if (@event is InputEventMouseButton eventMouseButton)
		{
			if (eventMouseButton.ButtonIndex != MouseButton.Left) return;

			if (eventMouseButton.Pressed)
			{
				Vector2 screenSize = GetViewport().GetVisibleRect().Size;
				SelectedCardIndex = (int)(eventMouseButton.Position.X * 4 / screenSize.X);
				cards[SelectedCardIndex].Position = GetLocalMousePosition();
			}
			else
			{
				if (SelectedCardIndex > -1)
				{
					cards[SelectedCardIndex].SetPosition();
					SelectedCardIndex = -1;
				}
			}
		}
		else if (@event is InputEventMouseMotion eventMouseMotion)
		{
			if (SelectedCardIndex > -1)
			{
				if (Input.IsMouseButtonPressed(MouseButton.Left))
				{
					cards[SelectedCardIndex].Position = GetLocalMousePosition();
				}
				else
				{
					cards[SelectedCardIndex].SetPosition();
					SelectedCardIndex = -1;
				}
			}
		}
	}

	public void _MouseEntered()
	{
	}

	public void _MouseExited()
	{
		if (SelectedCardIndex > -1)
		{
			cards[SelectedCardIndex].SetPosition();
		}
	}








}
