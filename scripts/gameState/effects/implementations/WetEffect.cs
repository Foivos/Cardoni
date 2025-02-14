namespace Cardoni;

public class WetEffect : Effect
{
	public const EffectType Type = EffectType.Wet;
	public override EffectType EffectType => EffectType.Wet;

	public WetEffect(Entity entity)
		: base(entity) { }

	protected override void Apply() { }

	protected override void Remove() { }
}
