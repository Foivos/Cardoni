namespace Cardoni;

using Godot;

public class BaseProjectileResult : CardResult
{
	public int MovementSpeed { get; set; } = -60;

	public EntityActive[] ProjectileActives { get; set; }

	public EntityMask ProjectileMask { get; set; } = new EntityMask(new EntityMasks[] { EntityMasks.Enemy });

	public uint Piercing { get; set; }

	public int Y { get; set; } = Constants.TicksPerLane;

	public BaseProjectileResult(EntityActive[] projectileActives)
	{
		ProjectileActives = projectileActives;
		Targets = new() { new LineTarget() };
	}

	public override void Activate()
	{
		Projectile projectile = new()
		{
			Y = Y,
			MovementSpeed = MovementSpeed,
			Actives = ProjectileActives,
			Lane = (uint)TargetView.Instance.GetCurrentOffset(new Vector2(0f, 0f)).X,
			Mask = ProjectileMask,
			Piercing = Piercing,
		};
		projectile.Sprite.Texture = GD.Load<Texture2D>("res://resources/icon.svg");
		projectile.Sprite.Scale = new Vector2(0.2f, 0.2f);
	}
}
