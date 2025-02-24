using System;

namespace Cardoni;

public class MindControlledEffect : Effect
{
	public const EffectType Type = EffectType.MindControlled;
	public override EffectType EffectType => EffectType.MindControlled;

	public static readonly EntityMask MindControlMask = new() { EntityMasks.Friendly, EntityMasks.Enemy };

	public MindControlledEffect(Entity entity)
		: base(entity) { }

	protected override void Apply() { }

	protected override void Remove() { }
}
