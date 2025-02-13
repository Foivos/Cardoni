namespace Cardoni;

public class RestrictedEffect : CountedEffect
{
	public override EffectType EffectType => EffectType.Restricted;

	public RestrictedEffect(Entity entity)
		: base(entity) { }

	protected override void Apply()
	{
		Entity.AttackSpeedModifier -= 1.5f;
	}

	protected override void Remove()
	{
		Entity.AttackSpeedModifier += 1.5f;
	}
}
