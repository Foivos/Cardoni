namespace Cardoni;

public class Haste : CountingExpiringEffectCondition<HasteEffect>
{
	public override EffectType EffectType => EffectType.Haste;

	public Haste(Entity entity, uint duration)
		: base(entity, duration) { }
}
