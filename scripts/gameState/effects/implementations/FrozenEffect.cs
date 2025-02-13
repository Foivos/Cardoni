namespace Cardoni;

public class FrozenEffect : StunnedEffect
{
	public override EffectType EffectType => EffectType.Frozen;

	public FrozenEffect(Entity entity)
		: base(entity) { }
}
