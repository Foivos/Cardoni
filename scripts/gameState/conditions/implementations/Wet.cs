namespace Cardoni;

public class Wet : CountingExpiringEffectCondition<WetEffect>
{
	public override EffectType EffectType => EffectType.Wet;

	public Wet(Entity entity, uint duration)
		: base(entity, duration) { }
}
