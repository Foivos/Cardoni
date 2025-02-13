namespace Cardoni;

public class Hasted : CountingExpiringEffectCondition<HasteEffect>
{
	public override EffectType EffectType => EffectType.Hasted;

	public Hasted(Entity entity, uint duration) : base(entity, duration)
	{}
}
