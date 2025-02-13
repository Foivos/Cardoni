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



	public static void doAtackAnim(Sprite2D entity, Sprite2D weapon)
	{
		if (inst == null || entity == null || weapon == null) return;
		inst.atackAnim(entity, weapon);
	}
	public async void atackAnim(Sprite2D entity, Sprite2D weapon)
	{


		//targetDamage();


		entity.RotationDegrees = -atackerRotation;
		weapon.Material = redOutline;

		await ToSignal(GetTree().CreateTimer(delay), "timeout");

		entity.RotationDegrees = 0;
		entity.Offset = new Vector2(0, atackerOffset);
		weapon.ZIndex = 5;
		weapon.Offset = shordOffset;
		weapon.RotationDegrees = shordRotation;


		await ToSignal(GetTree().CreateTimer(delay), "timeout");

		entity.Offset = Vector2.Zero;



		await ToSignal(GetTree().CreateTimer(delay), "timeout");

		weapon.ZIndex = 0;
		weapon.RotationDegrees = 0;
		weapon.Offset = Vector2.Zero;
		weapon.Material = null;
		//shord.TopLevel = false;




		//await ToSignal(GetTree().CreateTimer(0.5f), "timeout");
		//doingEffect = false;


	}













}
