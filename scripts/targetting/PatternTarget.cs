namespace Cardoni;

using System;
using System.Collections.Generic;
using Godot;

[GlobalClass]
public partial class PatternTarget : EntityTarget
{
	[Export]
	public bool Clip { get; set; }

	public List<Vector2I> Pattern { get; set; }

	[Export]
	public string[] StringPattern
	{
		get
		{
			Vector2I max = new();
			foreach (Vector2I point in Pattern)
			{
				if (max.X < point.X)
				{
					max.X = point.X;
				}
				if (max.Y < point.Y)
				{
					max.Y = point.Y;
				}
			}
			string[] result = new string[max.Y];
			for (int i = 0; i < max.Y; i++)
			{
				result[i] = new string('O', max.X);
			}
			foreach (Vector2I point in Pattern)
			{
				result[point.Y] = result[point.Y][..point.X] + "X" + result[point.Y][(point.X + 1)..];
			}
			return result;
		}
		set
		{
			int height = value.Length;
			if (height < 1)
				throw new Exception("Pattern should have a height of at least 1.");
			int width = value[0].Length;
			Offset = new Vector2(-(width - 1) / 2f, -(height - 1) / 2f);

			Pattern = new List<Vector2I>();
			int y = 0;
			foreach (string line in value)
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
	}

	public Vector2 Offset { get; set; }

	public PatternTarget() { }

	public PatternTarget(string[] pattern, bool clip)
	{
		StringPattern = pattern;
		Clip = clip;
	}

	public override List<Entity> Targets()
	{
		List<Entity> list = new();
		Vector2I offset = TargetView.Instance.GetCurrentOffset(Offset);
		foreach (Entity entity in GameState.Entities)
		{
			if (!EntityMask.Matches(entity.Mask))
			{
				continue;
			}
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
