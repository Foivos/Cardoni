using System;
using Godot;

public partial class testPlayer : Node2D
{
	static testPlayer inst;
	int damageTaken;

	public override void _Ready()
	{
		inst = this;
	}

	public static void damage(int damage)
	{
		if (inst == null)
		{
			GD.Print("no player inscene ");
			return;
		}

		inst.damageTaken += damage;
		GD.Print("Damage taken: " + inst.damageTaken);
	}
}
