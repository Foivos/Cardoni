using System;

namespace Cardoni;

public class MindControlledEffect : Effect
{
	public const EffectType Type = EffectType.MindControlled;
	public override EffectType EffectType => EffectType.MindControlled;

	public static readonly EntityMask MindControlMask = new(
		new EntityMasks[] { EntityMasks.Friendly, EntityMasks.Enemy }
	);

	public MindControlledEffect(Entity entity)
		: base(entity) { }

	protected override void Apply()
	{
		Entity.TargetMask.Mask = Entity.TargetMask.Mask ^ MindControlMask.Mask;
	}

	protected override void Remove()
	{
		Entity.TargetMask.Mask = Entity.TargetMask.Mask ^ MindControlMask.Mask;
	}
}
