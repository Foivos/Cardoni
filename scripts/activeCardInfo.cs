using System;
using Cardoni;
using Godot;

public partial class activeCardInfo : Node2D
{
	static activeCardInfo Instance;

	public override void _Ready()
	{
		Instance = this;
		Visible = false;
	}

	[Export]
	Label cardName,
		cardInfo;

	[Export]
	float wordDelay = 0.05f;

	[Export]
	Vector2 panelPositions;

	string[] infoWords = new string[0];
	int wordCounter;
	float timer;

	Card selectedCardHolder;
	int cardOffset = -10;
	float sizeHolder,
		positionHolder;
	bool setUpDone;
	float sizeAdjust = 0.03f;

	void selectedCardGraphics()
	{
		selectedCardHolder = GameState.SelectedCard;

		if (!setUpDone)
		{
			sizeHolder = selectedCardHolder.Scale.X;
			positionHolder = selectedCardHolder.Position.Y;
			setUpDone = true;
		}

		selectedCardHolder.Sprite.Material = shaderHolder.outlineBlue;
		selectedCardHolder.Position = new Vector2(selectedCardHolder.Position.X, positionHolder + cardOffset);
		//selectedCardHolder.Sprite.Offset = new Vector2(0, cardOffset);


		selectedCardHolder.Scale = new Vector2(sizeHolder + sizeAdjust, sizeHolder + sizeAdjust);
	}

	void resetSelectedCardGraphics()
	{
		if (!IsInstanceValid(selectedCardHolder))
			return;

		selectedCardHolder.Sprite.Material = null;
		//selectedCardHolder.Sprite.Offset = Vector2.Zero;
		selectedCardHolder.Position = new Vector2(selectedCardHolder.Position.X, positionHolder);

		selectedCardHolder.Scale = new Vector2(sizeHolder, sizeHolder);
	}

	public static void displayCard(Card card)
	{
		Instance.timer = 0;
		Instance.wordCounter = 0;
		Instance.cardInfo.Text = "";
		Instance.infoWords = card.Description.Split(' ');

		for (int i = 0; i < Instance.infoWords.Length; i++)
		{
			GD.Print(Instance.infoWords[i]);
		}

		float x = card.Index >= 2 ? Instance.panelPositions.X : Instance.panelPositions.Y;
		//GD.Print("x: " + x);
		Instance.Position = new Vector2(x, Instance.Position.Y);

		Instance.cardName.Text = card.Name;

		Instance.selectedCardGraphics();

		Instance.Visible = true;
		Instance.SetProcess(true);
	}

	public static void closeDisplay()
	{
		Instance.resetSelectedCardGraphics();

		Instance.Visible = false;
		Instance.SetProcess(false);
	}

	public override void _Process(double delta)
	{
		timer += (float)delta;
		if (timer < wordDelay)
			return;
		if (wordCounter >= infoWords.Length)
			return;
		timer = 0;

		cardInfo.Text += infoWords[wordCounter] + " ";
		wordCounter++;
	}
}
