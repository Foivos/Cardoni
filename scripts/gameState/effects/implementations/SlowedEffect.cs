namespace Cardoni;

public class SlowedEffect : CountedEffect
{
	public override EffectType EffectType => EffectType.Slowed;

	public SlowedEffect(Entity entity)
		: base(entity) { }

	protected override void Apply()
	{
		Entity.MovementSpeedModifier -= 0.5f;
		Entity.AttackSpeedModifier -= 0.5f;
	}

	protected override void Remove()
	{
		Entity.MovementSpeedModifier += 0.5f;
		Entity.AttackSpeedModifier += 0.5f;
	}
}
