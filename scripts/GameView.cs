namespace Cardoni;

using System;
using Godot;

public partial class GameView : Area2D
{
	public static GameView Instance { get; set; }

	[Export] public Label ManaLabel;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Instance = this;
		InputEvent += _Input;
		MouseExited += _MouseExited;
		MouseEntered += _MouseEntered;
	}

	public void _Input(Node viewport, InputEvent @event, long shapeIdx)
	{
		if (@event is InputEventMouseButton eventMouseButton)
		{
			if (
				eventMouseButton.ButtonIndex != MouseButton.Left
				|| eventMouseButton.Pressed
				|| GameState.SelectedCard == null
			)
				return;


			if (GameState.Instance.Mana >= GameState.SelectedCard.ManaCost)
			{
				GameState.Instance.Mana -= GameState.SelectedCard.ManaCost;
				GameState.SelectedCard.CardResult.Activate();
				CardView.Instance.CardPlayed(GameState.SelectedCard);
			}
			else GD.Print("Not enough mana");


			TargetView.EndTargeting();
			GameState.SelectedCard = null;
			activeCardInfo.closeDisplay();
		}
		else if (@event is InputEventMouseMotion eventMouseMotion)
		{
			if (GameState.SelectedCard == null)
				return;
		}
	}

	public void _MouseEntered()
	{
		if (GameState.SelectedCard == null)
			return;

		activeCardInfo.displayCard(GameState.SelectedCard);
		TargetView.BeginTargeting(); //! CHOOSE TYPE
	}

	public void _MouseExited()
	{
		if (GameState.SelectedCard == null)
			return;

		activeCardInfo.closeDisplay();
		TargetView.EndTargeting();
	}
}
