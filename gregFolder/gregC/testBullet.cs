namespace Cardoni;

using System;
using Godot;

public partial class testBullet : Area2D
{
	private void OnBodyEntered(Node body)
	{
		if (body.IsInGroup("enemies") == false)
			return;

		var enemy = body.GetNodeOrNull<enemyC>("enemyC");
		if (enemy == null)
			return;

		enemy.QueueFree();
	}
}
