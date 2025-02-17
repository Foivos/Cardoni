using Godot;

namespace Cardoni;

public partial class GoblinWarrior : DamagingEntity
{
	public GoblinWarrior(uint lane, int y = 0, uint health = 20, int damage = 2)
		: base()
	{
		OccupyingLanes = new OccupyingLanes(lane);
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
		Range = (uint)Constants.GridTicks / 2;//res://resources/singleGoblin.png
		Parent.Sprite.Texture = GD.Load<Texture2D>("res://resources/enemies/kaktosBase.png");
		Parent.Sprite.Scale = Vector2.One * 0.5f;
		Parent.Sprite.RegionEnabled = false;
		//Parent.Sprite.RegionRect = new Rect2(new Vector2(192, 0), new Vector2(32, 32));
	}
}
