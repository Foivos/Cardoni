namespace Cardoni;

using Godot;

[GlobalClass]
public partial class MaskFilter : EntityFilter
{
	[Export]
	public EntityMask Mask;

	public override bool IsValid(Entity source, Entity other)
	{
		return Mask.Matches(other.Mask);
	}
}
