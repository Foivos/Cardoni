using System;

namespace Cardoni;

public class MindControlledEffect : CountedEffect
{
	public const EffectType Type = EffectType.MindControlled;
	public override EffectType EffectType => EffectType.MindControlled;

	public MindControlledEffect(Entity entity)
		: base(entity) { }

	protected override void Apply()
	{
		throw new NotImplementedException();
	}

	protected override void Remove()
	{
		throw new NotImplementedException();
	}
}
