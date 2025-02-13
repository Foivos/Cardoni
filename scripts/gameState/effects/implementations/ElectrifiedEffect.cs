namespace Cardoni;

public class ElectrifiedEffect : CountedEffect
{
	public override EffectType EffectType => EffectType.Electrified;

	public ElectrifiedEffect(Entity entity)
		: base(entity) { }

	protected override void Apply()
	{
	}

	protected override void Remove()
	{
	}
}
