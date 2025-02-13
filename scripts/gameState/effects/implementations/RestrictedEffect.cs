namespace Cardoni;

public class RestrictedEffect : CountedEffect
{
	public const EffectType Type = EffectType.Restricted;
	public override EffectType EffectType => EffectType.Restricted;

	public RestrictedEffect(Entity entity)
		: base(entity) { }

	protected override void Apply()
	{
		Entity.MovementSpeedModifier -= 1.5f;
	}

	protected override void Remove()
	{
		Entity.MovementSpeedModifier += 1.5f;
	}
}
