using System;

namespace Cardoni;

public class MindControlledEffect : CountedEffect
{
	public const EffectType Type = EffectType.MindControlled;
	public override EffectType EffectType => EffectType.MindControlled;

	readonly public static EntityMask MindControlMask = new(new EntityMasks[]{EntityMasks.Friendly, EntityMasks.Enemy});

	public MindControlledEffect(Entity entity)
		: base(entity) { }

	protected override void Apply()
	{
		Entity.Mask.Mask = Entity.Mask.Mask ^ MindControlMask.Mask;
	}

	protected override void Remove()
	{
		Entity.Mask.Mask = Entity.Mask.Mask ^ MindControlMask.Mask;
	}
}
