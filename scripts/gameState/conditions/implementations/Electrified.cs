namespace Cardoni;

public class Electrified : CountingExpiringEffectCondition<ElectrifiedEffect>
{
	public override EffectType EffectType => EffectType.Electrified;

	public Electrified(Entity entity, uint duration)
		: base(entity, duration) { }
}
