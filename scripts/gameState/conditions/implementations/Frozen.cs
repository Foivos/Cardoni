namespace Cardoni;

public class Frozen : CountingExpiringEffectCondition<FrozenEffect>
{
	public override EffectType EffectType => EffectType.Frozen;

	public Frozen(Entity entity, uint duration)
		: base(entity, duration) { }
}
