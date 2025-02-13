namespace Cardoni;

public class MindControlled : CountingExpiringEffectCondition<MindControlledEffect>
{
	public override EffectType EffectType => EffectType.MindControlled;

	public MindControlled(Entity entity, uint duration) : base(entity, duration)
	{}
}
