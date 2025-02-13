using Cardoni;
using Godot;
using System;

public partial class atackAnimation : Node
{


	static atackAnimation inst;

	public override void _Process(double delta) { inst = this; }



	[Export] Material redOutline;

	[Export] float atackerRotation;
	[Export] float atackerOffset;
	[Export] float shordRotation;
	[Export] Vector2 shordOffset;
	[Export] float delay;
	[Export] float targetDamageDelay;



	public static void doAtackAnim(AttackingEntity attacker)
	{
		if (inst == null) return;
		if (attacker.Parent.Sprite == null || attacker.Parent.Sprite == null) return;
		inst.attackAnim(attacker);
	}
	public async void attackAnim(AttackingEntity attacker)
	{


		bool problemDetection() { return attacker == null || attacker.IsAlive == false; }


		Sprite2D sprite = attacker.Parent.Sprite;
		Sprite2D weapon = attacker.Parent.Weapon;
		float shordStartRotation = weapon.RotationDegrees;


		sprite.RotationDegrees = -atackerRotation;
		weapon.Material = redOutline;

		await ToSignal(GetTree().CreateTimer(delay), "timeout");
		if (problemDetection()) return;

		sprite.RotationDegrees = 0;
		sprite.Offset = new Vector2(0, atackerOffset) * attacker.Direction;
		weapon.ZIndex = 5;
		weapon.Offset = new Vector2(shordOffset.X, attacker.Direction * shordOffset.Y);


		weapon.RotationDegrees = shordStartRotation +  shordRotation * attacker.Direction;


		await ToSignal(GetTree().CreateTimer(delay), "timeout");
		if (problemDetection()) return;

		sprite.Offset = Vector2.Zero;



		await ToSignal(GetTree().CreateTimer(delay), "timeout");
		if (problemDetection()) return;

		weapon.ZIndex = 0;
		weapon.RotationDegrees = shordStartRotation;
		weapon.Offset = Vector2.Zero;
		weapon.Material = null;
		//shord.TopLevel = false;




		//await ToSignal(GetTree().CreateTimer(0.5f), "timeout");
		//doingEffect = false;


	}













}
