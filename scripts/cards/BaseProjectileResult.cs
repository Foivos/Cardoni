namespace Cardoni;

using System;
using Godot;

[GlobalClass]
public partial class BaseProjectileResult : CardResult
{
	[Export]
	public int MovementSpeed { get; set; } = -60;

	[Export]
	public EntityMask ProjectileMask { get; set; } = new EntityMask(new EntityMasks[] { EntityMasks.Enemy });

	[Export]
	public int Piercing { get; set; } = 1;

	[Export]
	public int Y { get; set; } = Constants.TicksPerLane;

	public BaseProjectileResult()
	{
		Targets = new CardTarget[] { new LineTarget() };
	}

	public override void Activate()
	{
		Projectile projectile = new()
		{
			Y = Y,
			MovementSpeed = MovementSpeed,
			Actives = Array.ConvertAll(Effects, (effect) => (EntityActive)effect),
			Lane = (uint)TargetView.Instance.GetCurrentOffset(new Vector2(0f, 0f)).X,
			Mask = ProjectileMask,
			Piercing = Piercing,
		};
		projectile.Sprite.Texture = GD.Load<Texture2D>("res://resources/icon.svg");
		projectile.Sprite.Scale = new Vector2(0.2f, 0.2f);
	}
}
