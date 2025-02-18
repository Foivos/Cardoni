namespace Cardoni;

using Godot;

[GlobalClass]
public partial class LaneFilter : EntityFilter
{
	[Export]
	public int RelativeLane { get; set; }

	public override bool IsValid(Entity source, Entity other)
	{
		return source.OccupyingLanes.From + RelativeLane <= other.OccupyingLanes.To
			&& source.OccupyingLanes.To + RelativeLane >= other.OccupyingLanes.From;
	}
}
