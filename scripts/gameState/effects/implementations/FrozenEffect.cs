namespace Cardoni;

public class FrozenEffect : StunnedEffect
{
	public new const EffectType Type = EffectType.Frozen;
	public override EffectType EffectType => EffectType.Frozen;

	public FrozenEffect(Entity entity)
		: base(entity) { }
}
