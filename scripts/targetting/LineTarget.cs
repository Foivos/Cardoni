namespace Cardoni;

using System.Collections.Generic;
using Godot;

[GlobalClass]
public partial class LineTarget : EntityTarget
{
	static int Line => TargetView.Instance.Line;

	public LineTarget() { }

	public override List<Entity> Targets()
	{
		return GameState.Entities.FindAll(
			(entity) => entity.OccupyingLanes.IsIn((uint)Line) && EntityMask.Matches(entity.Mask)
		);
	}
}
