namespace Cardoni;

using System.Collections.Generic;
using Godot;


public partial class GoblinShaman : EffectEntity
{
    public List<Hasted> ActiveConditions { get; set; } = new();
	public GoblinShaman(uint lane, int y = 0, uint health = 5)
		: base()
	{
		OccupyingLanes = (uint)(1 << (int)lane);
		Y = y;
		MaxHealth = health;
		Health = (int)health;

		Mask = new EntityMask(new EntityMasks[] { EntityMasks.Enemy, EntityMasks.Mobile });
        EffectMask = new EntityMask(new EntityMasks[] { EntityMasks.Enemy });
		Name = "Goblin Shaman";
		Height = Constants.GridTicks / 2;
        Parent.Sprite.Texture = GD.Load<Texture2D>("res://resources/EnemySpriteSheet1.png");
        Parent.Sprite.RegionRect = new Rect2(new Vector2(32,128), new Vector2(32, 32));
	}

	public override void ApplyEffect(Entity entity)
	{
		ActiveConditions.Add(new Hasted(entity, 0));
	}

	public override void RemoveEffect(Entity entity)
	{
		foreach (Hasted effect in ActiveConditions) 
        {
            effect.End();
        }
	}
}
