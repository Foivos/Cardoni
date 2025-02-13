namespace Cardoni;

public class CardEffectDamage : EntityActive
{
	public int Damage { get; set; }
	public DamageTypes DamageType { get; set; }

	public CardEffectDamage(DamageTypes damageType, int damage)
	{
		Damage = damage;
		DamageType = damageType;
	}

	public override void Activate(Entity entity)
	{
		entity.DamageTyped(DamageType, Damage);
	}
}
