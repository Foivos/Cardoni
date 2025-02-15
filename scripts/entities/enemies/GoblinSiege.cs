namespace Cardoni;

using Godot;

public partial class GoblinSiege : DamagingEntity
{
	public GoblinSiege(uint leftLane, int y = 0, uint health = 100, int damage = 3)
		: base()
	{
		OccupyingLanes = new OccupyingLanes(leftLane, leftLane + 1);
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
		
		Parent.Sprite.Texture = GD.Load<Texture2D>("res://resources/singleGoblinTwo.png");
		//Parent.Sprite.RegionRect = new Rect2(new Vector2(0, 0), new Vector2(348, 265));
		Parent.Sprite.RegionEnabled = false; 
		Parent.Sprite.Scale = Vector2.One * 0.5f;
	}
}
