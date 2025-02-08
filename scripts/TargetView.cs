namespace Cardoni;

using System;
using System.Collections.Generic;
using Godot;

public partial class TargetView : Node2D
{
	public static TargetView Instance;

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
	public int Line { get; set; }

	Vector2 PatternOffset;

	public override void _Ready()
	{
		Instance = this;
		ScreenSize = GetViewport().GetVisibleRect().Size;

		EndTargeting();
	}

	public override void _Process(double delta)
	{
		ProcessGraphics();
	}

	public static void BeginTargeting()
	{
		Instance._BeginTargeting();
	}

	public static void EndTargeting()
	{
		Instance._EndTargeting();
	}

	public List<Entity> GetPatternEntities()
	{
		List<Entity> list = new List<Entity>();
		foreach (Entity entity in GameState.Entities)
		{
			foreach (Sprite2D sprite in PatternTarget.GetChildren())
			{
				RectangleShape2D rectShape = new RectangleShape2D();
				rectShape.Size = sprite.RegionRect.Size;
				if (rectShape.Collide(sprite.GlobalTransform, entity.Shape, entity.GlobalTransform))
				{
					list.Add(entity);
					break;
				}
			}
		}
		return list;
	}

	void _BeginTargeting() //! MAIN FUNCTION
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
				LineTarget.Visible = true;
			}
			else if (target is PositionTarget)
			{
				PositionTarget.Visible = true;
			}
			else if (target is PatternTarget)
			{
				PatternTarget.Visible = true;
				PatternTarget patternTarget = (PatternTarget)target;
				PatternOffset = patternTarget.Offset;
				foreach (Vector2I item in patternTarget.Pattern)
				{
					Sprite2D sprite = (Sprite2D)PositionTarget.Duplicate();
					PatternTarget.AddChild(sprite);
					sprite.Visible = true;
					sprite.Position = ((Vector2)(item)) * LineLength;
				}
			}
		}
		//enemyTarget.Visible = targetNow == targetTypes.enemy;
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
		Vector2 MousePosition = GetLocalMousePosition();

		Line = GetSelectedLine(MousePosition);

		if (PositionTarget.Visible)
		{
			PositionTarget.Position = PositionToGrid(MousePosition);
		}
		if (LineTarget.Visible)
		{
			LineTarget.Position = new Vector2((-1.5f + Line) * LineLength, LineTarget.Position.Y);
		}
		if (PatternTarget.Visible)
		{
			PatternTarget.Position = PositionToGrid(MousePosition, PatternOffset);
		}
	}

	int GetSelectedLine(Vector2 MouseWorldPos)
	{
		return (int)Math.Floor(MouseWorldPos.X / LineLength) + 2;
	}

	float ToGrid(float coord, float offset)
	{
		return ((float)Math.Floor(coord / LineLength + offset) + 0.5f) * LineLength;
	}

	Vector2 PositionToGrid(Vector2 pos, Vector2 offset)
	{
		return new Vector2(ToGrid(pos.X, offset.X), ToGrid(pos.Y, offset.Y));
	}

	Vector2 PositionToGrid(Vector2 pos)
	{
		return PositionToGrid(pos, new Vector2());
	}
}
