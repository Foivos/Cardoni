using System;
using Cardoni;
using Godot;

[GlobalClass]
public partial class EntityActiveProjectile : EntityActive
{
	[Export]
	public int MovementSpeed { get; set; } = -60;

	[Export]
	public EntityMask ProjectileMask { get; set; } = new EntityMask() { EntityMasks.Enemy };

	[Export]
	public int Piercing { get; set; } = 1;

	[Export]
	public int Y { get; set; } = 0; // Constants.TicksPerLane;

	[Export]
	EntityActive Active { get; set; }

	public EntityActiveProjectile() { }

	public override void Activate(Entity entity)
	{
		// if entity is not oving from birth facing direction == 0   FIX THIS
		//GD.Print("projectile Dir : " + Attack.EntityAttackingNow.FacingDirection);

		int direction = Attack.EntityAttackingNow.FacingDirection;
		if (direction == 0)
		{
			bool enemy = Attack.EntityAttackingNow.Mask.Matches(new EntityMask() { EntityMasks.Enemy });
			direction = enemy ? 1 : -1;
		}

		var projectile = new Projectile()
		{
			Y = Attack.EntityAttackingNow.Y + direction * Y,
			MovementSpeed = MovementSpeed * direction,
			Active = Active,
			Lane = (uint)Attack.EntityAttackingNow.OccupyingLanes.From,
			Mask = ProjectileMask,
			Piercing = Piercing,
		};
		//png not svg ok ??
		projectile.Sprite.Texture = GD.Load<Texture2D>("res://resources/spear.png");
		projectile.Sprite.Scale = new Vector2(1, 1);

		projectile.Sprite.RotationDegrees = MovementSpeed < 0 ? 180 : 0;
	}
}
