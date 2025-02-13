using Cardoni;

public partial class DamagingEntity : AttackingEntity
{
	public int AttackDamage { get; set; }
	public DamageTypes DamageType { get; set; }

	protected override void Attack()
	{
		Target.DamageTyped(DamageType, AttackDamage);
	}
}
