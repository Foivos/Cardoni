namespace Cardoni;

public class ConfusedEffect : CountedEffect
{
	public const EffectType Type = EffectType.Confused;
	public override EffectType EffectType => EffectType.Confused;

	public ConfusedEffect(Entity entity)
		: base(entity) { }

	protected override void Apply()
	{
		Entity.MovementSpeedModifier += 0.5f;
		Entity.AttackSpeedModifier += 0.5f;
	}

	protected override void Remove()
	{
		Entity.MovementSpeedModifier -= 0.5f;
		Entity.AttackSpeedModifier -= 0.5f;
	}
}
