using System;

namespace Cardoni;

public class BleedingEffect : CountedEffect
{
	public const EffectType Type = EffectType.Bleeding;
	public override EffectType EffectType => Type;

	public BleedingEffect(Entity entity)
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
