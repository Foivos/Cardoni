namespace Cardoni;

using Godot;

[GlobalClass]
public partial class RangeFilter : EntityFilter
{
	[Export]
	public uint MaxRange { get; set; }

	public override bool IsValid(Entity source, Entity other)
	{
		return MaxRange * MaxRange >= source.DistanceSquared(other);
	}
}
