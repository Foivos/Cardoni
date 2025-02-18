namespace Cardoni;

public class StunnedEffect : RestrictedEffect
{
	public new const EffectType Type = EffectType.Stunned;
	public override EffectType EffectType => EffectType.Stunned;

	public StunnedEffect(Entity entity)
		: base(entity) { }

	protected override void Apply()
	{
		base.Apply();
		Entity.AttackSpeedModifier -= 1.5f;
	}

	protected override void Remove()
	{
		base.Remove();
		Entity.AttackSpeedModifier += 1.5f;
	}
}
