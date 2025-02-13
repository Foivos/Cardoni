namespace Cardoni;

public class Restricted : CountingExpiringEffectCondition<RestrictedEffect>
{
	public override EffectType EffectType => EffectType.Restricted;

	public Restricted(Entity entity, uint duration)
		: base(entity, duration) { }
}
