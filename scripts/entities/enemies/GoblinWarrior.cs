using Godot;

namespace Cardoni;

public partial class GoblinWarrior : DamagingEntity
{
	public GoblinWarrior(uint lane, int y = 0, uint health = 5, int damage = 2)
		: base()
	{
		OccupyingLanes = (uint)(1 << (int)lane);
		Y = y;
		MaxHealth = health;
		Health = (int)health;
		AttackDamage = damage;

		Mask = new EntityMask(new EntityMasks[] { EntityMasks.Enemy, EntityMasks.Mobile });
		TargetMask = new EntityMask(new EntityMasks[] { EntityMasks.Friendly });
		Name = "Goblin Warrior";
		BaseMovementSpeed = 60;
		BaseAttackSpeed = 40;
		Height = Constants.GridTicks / 2;
		Range = (uint)Constants.GridTicks / 2;
        Parent.Sprite.Texture = GD.Load<Texture2D>("res://resources/EnemySpriteSheet1.png");
        Parent.Sprite.RegionRect = new Rect2(new Vector2(192,0), new Vector2(32, 32));
	}
}
