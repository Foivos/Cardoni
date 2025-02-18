namespace Cardoni;

using System;
using System.Collections.Generic;
using Godot;

public partial class TargetView : Node2D
{
	public static TargetView Instance { get; set; }

	[ExportGroup("Game Objects")]
	[Export]
	Sprite2D PositionTarget { get; set; }

	[Export]
	Sprite2D LineTarget { get; set; }

	[Export]
	Node2D PatternTarget { get; set; }

	[ExportGroup("Variables")]
	[Export]
	Vector2 ScreenSize { get; set; }

	[Export]
	float LineLength { get; set; }

	[Export]
	public Vector2 MousePosition { get; set; }
	public int Line => GetSelectedLine(MousePosition);

	Vector2 PatternOffset { get; set; }

	public override void _Ready()
	{
		Instance = this;
		ScreenSize = GetViewport().GetVisibleRect().Size;

		EndTargeting();
	}

	public override void _Process(double delta)
	{
		if (GameState.SelectedCard == null || GameState.SelectedCard.CardResult == null)
			return;
		ProcessGraphics();
		ProcessTargets();
	}

	private void ProcessTargets()
	{
		CardResult result = GameState.SelectedCard.CardResult;
		if (result is EffectResult effectResult)
		{
			foreach (Entity entity in effectResult.EntityTarget.Targets())
			{
				// You can do something to each target here.
			}
		}
	}

	public static void BeginTargeting()
	{
		Instance._BeginTargeting();
	}

	public static void EndTargeting()
	{
		Instance._EndTargeting();
	}

	void _BeginTargeting() //! MAIN FUNCTION
	{
		if (GameState.SelectedCard == null || GameState.SelectedCard.CardResult == null)
			return;
		SetProcess(true);

		DisplayTarget(GameState.SelectedCard.CardResult.Target);
		//enemyTarget.Visible = targetNow == targetTypes.enemy;
	}

	void DisplayTarget(CardTarget target)
	{
		if (target == null)
			return;
		if (target is LineTarget)
		{
			LineTarget.Visible = true;
		}
		else if (target is PositionTarget)
		{
			PositionTarget.Visible = true;
		}
		else if (target is PatternTarget patternTarget)
		{
			PatternTarget.Visible = true;
			PatternOffset = patternTarget.Offset;
			foreach (Vector2I item in patternTarget.Pattern)
			{
				Sprite2D sprite = (Sprite2D)PositionTarget.Duplicate();
				PatternTarget.AddChild(sprite);
				sprite.Visible = true;
				sprite.Position = ((Vector2)item) * LineLength;
			}
		}
		else if (target is MultiTarget multiTarget)
		{
			foreach (CardTarget inner in multiTarget.Targets)
			{
				DisplayTarget(inner);
			}
		}
		else if (target is MultiEntityTarget multiEntityTarget)
		{
			foreach (EntityTarget inner in multiEntityTarget.EntityTargets)
			{
				DisplayTarget(inner);
			}
		}
	}

	void _EndTargeting() //! MAIN FUNCTION
	{
		GD.Print("End targetting");
		PositionTarget.Visible = false;
		LineTarget.Visible = false;
		foreach (Node child in PatternTarget.GetChildren())
		{
			child.QueueFree();
		}

		SetProcess(false);
	}

	void ProcessGraphics() //? GRAGHICS HERE - like line posision
	{
		MousePosition = GetLocalMousePosition();

		if (PositionTarget.Visible)
		{
			PositionTarget.Position = PositionToGrid();
		}
		if (LineTarget.Visible)
		{
			LineTarget.Position = new Vector2((-1.5f + Line) * LineLength, LineTarget.Position.Y);
		}
		if (PatternTarget.Visible)
		{
			PatternTarget.Position = PositionToGrid(PatternOffset);
		}
	}

	int GetSelectedLine(Vector2 MouseWorldPos)
	{
		return (int)Math.Floor(MouseWorldPos.X / LineLength) + 2;
	}

	public Vector2I GetCurrentOffset(Vector2 offset)
	{
		int x = (int)Math.Floor(MousePosition.X / LineLength + offset.X) + Constants.NumberOfLanes / 2;
		int y = (int)Math.Floor(MousePosition.Y / LineLength + offset.Y) + Constants.NumberOfRows / 2;
		return new(x, y);
	}

	public Vector2I GetCurrentPosition()
	{
		int x = (int)Math.Floor(MousePosition.X / LineLength) + Constants.NumberOfLanes / 2;
		int y = (int)Math.Floor(MousePosition.Y / LineLength * Constants.GridTicks) + Constants.TicksPerLane / 2;
		return new(x, y);
	}

	float ToGrid(float coord, float offset)
	{
		return ((float)Math.Floor(coord / LineLength + offset) + 0.5f) * LineLength;
	}

	public Vector2 PositionToGrid(Vector2 offset)
	{
		return new Vector2(ToGrid(MousePosition.X, offset.X), ToGrid(MousePosition.Y, offset.Y));
	}

	public Vector2 PositionToGrid()
	{
		return PositionToGrid(new Vector2());
	}
}
