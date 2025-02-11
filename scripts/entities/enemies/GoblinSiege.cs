namespace Cardoni;

public partial class GoblinSiege : DamagingEntity
{
	public GoblinSiege(uint leftLane, int y = 0, uint health = 15, int damage = 3)
		: base()
	{
		OccupyingLanes = (uint)(3 << (int)leftLane);
		Y = y;
		MaxHealth = health;
		Health = (int)health;
		AttackDamage = damage;

		Mask = new EntityMask(new EntityMasks[] { EntityMasks.Enemy, EntityMasks.Mobile });
		TargetMask = new EntityMask(new EntityMasks[] { EntityMasks.Friendly, EntityMasks.Structure });
		Name = "Goblin Sienge Engine";
		BaseMovementSpeed = 40;
		BaseAttackSpeed = 20;
		Height = Constants.GridTicks / 2;
		Range = (uint)Constants.GridTicks / 2;
	}
}
