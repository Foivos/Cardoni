namespace Cardoni;

using System;
using Godot;

public partial class GameView : Area2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		InputEvent += _Input;
		MouseExited += _MouseExited;
		MouseEntered += _MouseEntered;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) { }

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

			testTargetC.inst.endTargeting();
			GD.Print(testTargetC.inst);
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

		testTargetC.inst.beginTargeting(); //! CHOOSE TYPE
	}

	public void _MouseExited()
	{
		if (GameState.SelectedCard == null)
			return;

		testTargetC.inst.endTargeting();
	}
}
