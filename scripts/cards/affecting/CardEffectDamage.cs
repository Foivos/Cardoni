namespace Cardoni;

public class CardEffectDamage : EntityActive
{
	public int Damage { get; set; }

	public CardEffectDamage(int damage)
	{
		Damage = damage;
	}

	public override void Activate(Entity entity)
	{
		entity.Damage(Damage);
	}
}
