namespace Cardoni;

public class Stunned : CountingExpiringEffectCondition<StunnedEffect>
{
	public override EffectType EffectType => EffectType.Stunned;

	public Stunned(Entity entity, uint duration)
		: base(entity, duration) { }
}
