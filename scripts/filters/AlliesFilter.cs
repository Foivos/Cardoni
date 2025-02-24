namespace Cardoni;

using Godot;

[GlobalClass]
public partial class AlliesFilter : EntityFilter
{
	static readonly uint FactionsMask = new EntityMask() { EntityMasks.Friendly, EntityMasks.Enemy }.Mask;

	public override bool IsValid(Entity source, Entity other)
	{
		uint mindControled = source.GetEffect<MindControlledEffect>().Active ? FactionsMask : 0;
		return source.GetEffect<ConfusedEffect>().Active
			|| ((source.Mask.Mask ^ mindControled) & other.Mask.Mask & FactionsMask) > 0;
	}
}
