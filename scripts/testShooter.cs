namespace Cardoni;

using System;
using Godot;

public partial class testShooter : Node
{



	static testShooter inst;
	public override void _Ready()
	{
		inst = this;
	}
	[Export] PackedScene bulletPreffab;


	//[Export] Vector2 testPos;
	// public override void _Input(InputEvent @event)
	// {
	// 	shoot(testPos, true);
	// 	shoot(testPos, false);

	// }



	public static void shootEnemies(Vector2 pos, int damage) { shoot(pos, damage, true); }
	public static void shootMinions(Vector2 pos, int damage) { shoot(pos, damage, false); }
	static void shoot(Vector2 pos, int damage, bool targetEnemies)
	{
		if (inst == null) return;



		testBullet bull = inst.bulletPreffab.Instantiate<testBullet>();

		bull.Position = pos;
		bull.damage = (uint)damage;
		inst.AddChild(bull);

		if (targetEnemies) bull.targerEnemies();
		else bull.targetMinions();

		//GD.Print("shoot " + targetEnemies + "  " + bull.goingUp);


	}
}
