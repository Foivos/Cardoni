namespace Cardoni;

using System;
using Godot;

public partial class gregEditTests : Node
{
	[Export]
	enemyC enemy;

	public override void _Input(InputEvent @event) //? EDIT ONLY
	{
		if (@event.IsActionPressed("ui_accept"))
			enemy.onDamage(5);
	}
}
