namespace Cardoni;

using System;
using Godot;

public partial class testTargetC : Node
{
	public static testTargetC inst;

	[ExportGroup("PROPERTIES")]
	[Export]
	Sprite2D positionTarget,
		lineTarget;

	public Vector2 Position { get; set; }
	public int Line { get; set; }

	public override void _Ready()
	{
		inst = this;
		screenSize = GetViewport().GetVisibleRect().Size;

		endTargeting();

		//lineTarget.
	}

	public void beginTargeting() //! MAIN FUNCTION
	{
		if (GameState.SelectedCard == null || GameState.SelectedCard.CardTargets == null)
			return;
		SetProcess(true);
		foreach (CardTarget target in GameState.SelectedCard.CardTargets)
		{
			GD.Print(target.GetType());
			if (target is LineTarget)
			{
				GD.Print("Line");
				lineTarget.Visible = true;
			}
			else if (target is PositionTarget)
			{
				positionTarget.Visible = true;
			}
		}
		//enemyTarget.Visible = targetNow == targetTypes.enemy;
	}

	public void endTargeting() //! MAIN FUNCTION
	{
		GD.Print("End targetting");
		positionTarget.Visible = false;
		lineTarget.Visible = false;

		SetProcess(false);
	}

	void processGraphics() //? GRAGHICS HERE - like line posision
	{
		Vector2 mouseWorldPos = mousePosCenterZero();

		Position = positionToGrid(mouseWorldPos);
		Line = getSelectedLine(mouseWorldPos);

		if (positionTarget.Visible)
		{
			positionTarget.Position = Position;
		}
		if (lineTarget.Visible)
		{
			lineTarget.Position = new Vector2((-1.5f + Line) * lineLength, lineTarget.Position.Y);
		}
	}

	int getSelectedLine(Vector2 mouseWorldPos)
	{
		return (int)Math.Floor(mouseWorldPos.X / lineLength) + 2;
	}

	[ExportGroup("DEBUG THINGS")]
	[Export]
	Vector2 screenSize;

	[Export]
	float lineLength;

	[Export]
	Vector2 mousePos,
		mouseWorldPos;

	public override void _Process(double delta)
	{
		processGraphics();
	}

	public override void _Input(InputEvent @event) //? EDIT ONLY
	{
		if (@event.IsActionPressed("ui_accept"))
			beginTargeting();
	}

	Vector2 mousePosCenterZero()
	{
		return GetViewport().GetMousePosition() - inst.GetViewport().GetVisibleRect().Size / 2;
	}

	Vector2 positionToGrid(Vector2 pos)
	{
		pos.X = (float)Math.Floor(pos.X / lineLength) + 0.5f;
		pos.Y = (float)Math.Floor(pos.Y / lineLength) + 0.5f;
		return pos * lineLength;
	}
}
