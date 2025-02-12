namespace Cardoni;

using Godot;

public class BaseProjectileResult : CardResult
{
	public override void Activate()
	{
		Projectile projectile = new Projectile() {
            Y = Constants.TicksPerLane,
            MovementSpeed = -60,
            Actives = new EntityActive[1],
            Lane = 1,
            Mask = new EntityMask(new EntityMasks[]{EntityMasks.Enemy}),
        };
        projectile.Sprite.Texture = GD.Load<Texture2D>("res://resources/icon.svg");
        projectile.Sprite.Scale = new Vector2(0.2f, 0.2f);
        projectile.Actives[0] = new CardEffectDamage(1);
        GameState.Projectiles.Add(projectile);
	}
}