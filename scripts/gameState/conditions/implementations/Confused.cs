namespace Cardoni;

public class Confused : CountingExpiringEffectCondition<ConfusedEffect>
{
	public override EffectType EffectType => EffectType.Confused;

	public Confused(Entity entity, uint duration) : base(entity, duration)
	{}
}
