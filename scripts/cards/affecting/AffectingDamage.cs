namespace Cardoni;

public class AffectingDamage : Affecting
{
	public int Damage { get; set; }

	public AffectingDamage(int damage)
	{
		Damage = damage;
	}

	public override void Affect(Entity entity)
	{
		entity.Damage(Damage);
	}
}
