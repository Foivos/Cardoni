namespace Cardoni;

using Godot;

public partial class GoblinSummoner : Entity
{
	public GoblinSummoner(uint lane, int y = 0, uint health = 5)
		: base()
	{
		OccupyingLanes = new OccupyingLanes(lane);
		Y = y;
		MaxHealth = health;
		Health = (int)health;

		Mask = new EntityMask(new EntityMasks[] { EntityMasks.Enemy, EntityMasks.Mobile });
		TargetMask = new EntityMask(new EntityMasks[] { EntityMasks.Friendly });
		Name = "Goblin Summoner";
		BaseMovementSpeed = 60;
		BaseAttackSpeed = 15;
		Height = Constants.GridTicks / 2;
		Parent.Sprite.Texture = GD.Load<Texture2D>("res://resources/EnemySpriteSheet1.png");
		Parent.Sprite.RegionRect = new Rect2(new Vector2(128, 128), new Vector2(32, 32));
		Cooldowns.Add(new Cooldown(this, Activate));
	}

	protected void Activate()
	{
		new GoblinWarrior(OccupyingLanes.From, Y) { Mask = Mask, TargetMask = TargetMask };
	}
}
