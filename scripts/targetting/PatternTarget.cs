namespace Cardoni;

using System;
using System.Collections.Generic;
using Godot;

public class PatternTarget : EntityTarget
{
	public bool Clip { get; set; }

	public List<Vector2I> Pattern { get; set; }

	public Vector2 Offset { get; set; }

	public PatternTarget(string[] pattern, bool clip)
	{
		Clip = clip;
		int height = pattern.Length;
		if (height < 1)
			throw new Exception("Pattern should have a height of at least 1.");
		int width = pattern[0].Length;
		Offset = new Vector2(-(width - 1) / 2f, -(height - 1) / 2f);

		Pattern = new List<Vector2I>();
		int y = 0;
		foreach (string line in pattern)
		{
			if (line.Length != width)
				throw new Exception("All lines should have the same length.");
			int x = 0;
			foreach (char character in line)
			{
				if (character == 'X')
				{
					Pattern.Add(new Vector2I(x, y));
				}
				x += 1;
			}
			y += 1;
		}
	}

	public override List<Entity> Targets()
	{
		List<Entity> list = new List<Entity>();
		Vector2I offset = TargetView.Instance.GetCurrentOffset(Offset);
		foreach (Entity entity in GameState.Entities)
		{
			foreach (Vector2I patternSquare in Pattern)
			{
				Vector2I square = patternSquare + offset;
				if (!entity.OccupyingLanes.IsIn((uint)square.X))
					continue;
				if (Math.Abs(entity.Y - square.Y * Constants.GridTicks) >= entity.Height + Constants.GridTicks)
					continue;

				list.Add(entity);
				break;
			}
		}
		return list;
	}
}
