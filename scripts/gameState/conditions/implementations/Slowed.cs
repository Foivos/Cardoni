namespace Cardoni;

public class Slowed : CountingExpiringEffectCondition<SlowedEffect>
{
	public override EffectType EffectType => EffectType.Slowed;

	public Slowed(Entity entity, uint duration)
		: base(entity, duration) { }
}
