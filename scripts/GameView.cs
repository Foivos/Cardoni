namespace Cardoni;

using System;
using Godot;

public partial class GameView : Area2D
{
	public static GameView Instance { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Instance = this;
		InputEvent += _Input;
		MouseExited += _MouseExited;
		MouseEntered += _MouseEntered;

		Mana = 4;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) { processMana(delta); }

	[Export] Label manaLabel;
	int mana = 4;
	public int Mana
	{
		get => mana;
		set
		{
			if (value > 8) value = 8;

			mana = value;
			manaLabel.Text = mana.ToString();
		}
	}
	[Export] double manaRegen = 1;
	double manaTimer;
	void processMana(double delta)
	{


		manaTimer += delta;
		if (manaTimer < 1 / manaRegen) return;

		manaTimer = 0;
		Mana++;
		manaLabel.Text = mana.ToString();



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


			if (GameView.Instance.Mana >= GameState.SelectedCard.ManaCost)
			{
				GameView.Instance.Mana -= GameState.SelectedCard.ManaCost;
				GameState.SelectedCard.CardResult.Activate();
				CardView.Instance.CardPlayed(GameState.SelectedCard);
			}
			else GD.Print("Not enough mana");


			TargetView.EndTargeting();
			GameState.SelectedCard = null;
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

		TargetView.BeginTargeting(); //! CHOOSE TYPE
	}

	public void _MouseExited()
	{
		if (GameState.SelectedCard == null)
			return;

		TargetView.EndTargeting();
	}
}
