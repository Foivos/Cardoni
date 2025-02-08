using System;
using Cardoni;
using Godot;

public partial class minnionBase : monsterBase
{
	public override void rangeCalulations()
	{
		var enemy = targetDetection(Position, Position + Vector2.Up * atackRange, entityType.enemy);
		if (enemy == null && enemy is Enemy)
		{
			isAtacking = true;
			target = enemy;
		}
		else
			isAtacking = false;
	}
}
