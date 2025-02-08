using Cardoni;

public partial class DamagingEntity : AttackingEntity
{
    public int AttackDamage { get; set; }
	protected override void Attack()
	{
		Target.Damage(AttackDamage);
	}
}