using Cardoni;
using Godot;

public partial class atackAnimation : Node
{
	static atackAnimation inst;

	public override void _Ready()
	{
		inst = this;
	}

	[Export]
	Material redOutline;

	[Export]
	float atackerRotation;

	[Export]
	float atackerOffset;

	[Export]
	float shordRotation;

	[Export]
	Vector2 shordOffset;

	[Export]
	float delay;

	[Export]
	float targetDamageDelay;

	public static void doAtackAnim(Entity attacker)
	{
		if (inst == null)
			return;
		if (attacker.Sprite == null || attacker.Sprite == null)
			return;
		inst.attackAnim(attacker);
	}

	public async void attackAnim(Entity attacker)
	{
		bool problemDetection()
		{
			return attacker == null || attacker.IsAlive == false;
		}

		bool lookingDown = attacker.FacingDirection > 0;
		int atackDirection = lookingDown ? 1 : -1;

		Sprite2D sprite = attacker.Sprite;
		Sprite2D weapon = attacker.Weapon;
		float shordStartRotation = weapon.RotationDegrees;

		sprite.RotationDegrees = -atackerRotation;
		weapon.Material = redOutline;

		await ToSignal(GetTree().CreateTimer(delay), "timeout");
		if (problemDetection())
			return;

		sprite.RotationDegrees = 0;
		sprite.Offset = new Vector2(0, atackerOffset) * atackDirection;

		weapon.ZIndex = 5;
		weapon.Offset = new Vector2(atackDirection * shordOffset.X, shordOffset.Y);
		weapon.RotationDegrees = shordStartRotation + shordRotation * atackDirection;

		await ToSignal(GetTree().CreateTimer(delay), "timeout");
		if (problemDetection())
			return;

		sprite.Offset = Vector2.Zero;

		await ToSignal(GetTree().CreateTimer(delay), "timeout");
		if (problemDetection())
			return;

		weapon.ZIndex = 0;
		weapon.RotationDegrees = shordStartRotation;
		weapon.Offset = Vector2.Zero;
		weapon.Material = null;
	}
}
