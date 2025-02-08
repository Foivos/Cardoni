using System;
using Godot;

public partial class testPlayer : minnionBase
{
	uint damageTaken;

	public override void onDamage(uint amount)
	{
		damageTaken += amount;
		GD.Print("Damage taken: " + damageTaken);
	}
}
