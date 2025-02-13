using Cardoni;

public partial class DamagingAttack : Attack
{
	public int AttackDamage { get; set; }
	public DamageTypes DamageType { get; set; }

	public DamagingAttack(Entity entity, int attackDamage, uint range, DamageTypes damageType = DamageTypes.Physical)
		: base(entity, range)
	{
		AttackDamage = attackDamage;
		DamageType = damageType;
	}

	protected override void Activate()
	{
		Target.DamageTyped(DamageType, AttackDamage);
	}
}
