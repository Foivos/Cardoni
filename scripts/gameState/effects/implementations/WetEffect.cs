namespace Cardoni;

public class WetEffect : CountedEffect
{
	public override EffectType EffectType => EffectType.Wet;

	public WetEffect(Entity entity)
		: base(entity) { }

	protected override void Apply()
	{
	}

	protected override void Remove()
	{
	}
}
