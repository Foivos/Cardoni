using System;
using Cardoni;
using Godot;

public partial class Player : Entity
{
	public Player(uint health = 15)
		: base()
	{
		OccupyingLanes = new OccupyingLanes(0, 3);
		Y = Constants.TicksPerLane;
		MaxHealth = health;
		Health = (int)health;

		Mask = new EntityMask(new EntityMasks[] { EntityMasks.Friendly, EntityMasks.Structure });
		Name = "Player";
		BaseMovementSpeed = 0;
		Height = Constants.GridTicks / 2;

		//todoParent.Sprite.Texture = GD.Load<Texture2D>("res://resources/batteringRam.png");
		//Parent.Sprite.Scale = new Vector2(0.3f, 0.3f);
	}
}
