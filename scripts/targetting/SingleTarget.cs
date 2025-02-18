namespace Cardoni;

using System;
using System.Collections.Generic;
using Godot;

[GlobalClass]
public partial class SingleTarget : EntityTarget
{
	public const uint MaxDistance = 600;

	public SingleTarget() { }

	public override List<Entity> Targets()
	{
		Vector2I offset = TargetView.Instance.GetCurrentPosition();
		Entity chosen = null;
		int min = (int)MaxDistance + 1;
		foreach (Entity entity in GameState.Entities)
		{
			if (!entity.OccupyingLanes.IsIn((uint)offset.X))
				continue;
			int dy = Math.Abs(entity.Y - offset.Y);
			if (dy > MaxDistance)
				continue;
			if (dy < min)
			{
				min = dy;
				chosen = entity;
			}
		}
		if (chosen != null)
		{
			return new() { chosen };
		}
		return new();
	}
}
