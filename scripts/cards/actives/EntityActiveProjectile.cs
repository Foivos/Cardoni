using Cardoni;
using Godot;
using System;

[GlobalClass]
public partial class EntityActiveProjectile : EntityActive
{


	//! need to be able to acces data from the entity that is doing it 
	//! or this is not nesesary used ny an entity ?? ??


	[Export]
	public int MovementSpeed { get; set; } = -60;
	[Export]
	public EntityMask ProjectileMask { get; set; } = new EntityMask() { EntityMasks.Enemy };

	[Export]
	public int Piercing { get; set; } = 1;

	[Export]
	public int Y { get; set; } = 0;// Constants.TicksPerLane;


	[Export]
	EntityActive Active { get; set; }

	public EntityActiveProjectile() { }

	public override void Activate(Entity entity)
	{




		var projectile = new Projectile()
		{
			Y = Y,
			MovementSpeed = MovementSpeed,
			Active = Active,
			Lane = 2,// (uint)TargetView.Instance.GetCurrentOffset(new Vector2(0f, 0f)).X,
			Mask = ProjectileMask,
			Piercing = Piercing,
		};
		//png not svg ok ??
		projectile.Sprite.Texture = GD.Load<Texture2D>("res://resources/spear.png");
		projectile.Sprite.Scale = new Vector2(1, 1);
		projectile.Sprite.RotationDegrees = MovementSpeed > 0 ? 180 : 0;


	}



}
