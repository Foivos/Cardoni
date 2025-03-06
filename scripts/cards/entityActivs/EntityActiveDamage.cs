namespace Cardoni;

using Godot;

[GlobalClass]
public partial class EntityActiveDamage : EntityActive
{
	[Export]
	public int Damage { get; set; }

	[Export]
	public DamageTypes DamageType { get; set; }

	public EntityActiveDamage() { }

	public EntityActiveDamage(DamageTypes damageType, int damage)
	{
		Damage = damage;
		DamageType = damageType;
	}

	public override void Activate(Entity entity)
	{
		entity.DamageTyped(DamageType, Damage);
	}
}
