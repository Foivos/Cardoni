namespace Cardoni;

using Godot;

public partial class GoblinRanged : DamagingEntity
{
	public GoblinRanged(uint lane, int y = 0, uint health = 3, int damage = 2)
		: base()
	{
		OccupyingLanes = new OccupyingLanes(lane);
		Y = y;
		MaxHealth = health;
		Health = (int)health;
		AttackDamage = damage;

		Mask = new EntityMask(new EntityMasks[] { EntityMasks.Enemy, EntityMasks.Mobile });
		TargetMask = new EntityMask(new EntityMasks[] { EntityMasks.Friendly });
		Name = "Goblin Spearhead";
		BaseMovementSpeed = 60;
		BaseAttackSpeed = 40;
		Height = Constants.GridTicks / 2;
		Range = (uint)Constants.GridTicks / 2 * 4;
		Parent.Sprite.Texture = GD.Load<Texture2D>("res://resources/enemies/kaktosBandage.png");
		Parent.Sprite.RegionRect = new Rect2(new Vector2(128, 0), new Vector2(32, 32));
	}
}
