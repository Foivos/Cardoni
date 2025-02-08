namespace Cardoni;

using Godot;

public partial class Enemy : monsterBase
{
	//public Area2D GameView;
	//[Export] public CollisionShape2D CollisionShape;








	public override void _Process(double delta)
	{
		base._Process(delta);
		processMovement(delta);
		// 	displayHealthBar();

		//GD.Print("enemyC _Process");
	}

	#region  MOVEMENT




	[ExportGroup("movement")]
	[Export]
	public bool stopMoving = true;

	[Export]
	public float speed;

	const float downLimit = 200; // after that die and deal damage for now

	public void processMovement(double delta) // every frame called
	{
		if (stopMoving || isAtacking)
			return;

		Position += new Vector2(0, speed) * (float)delta;

		// if (Position.Y < downLimit)
		// 	return;

		// testPlayer.damage(1);
		// QueueFree();
	}

	#endregion



	public override void rangeCalulations()
	{
		var minion = targetDetection(Position, Position + Vector2.Up * atackRange, entityType.player);
		if (minion == null && minion is minnionBase)
		{
			isAtacking = true;
			target = minion;
		}
		else
			isAtacking = false;
	}

	#region //? DEPRICATED

	// public void _BodyExited(Node body)
	// {
	// 	GD.Print(body);
	// 	if (body != this)
	// 	{
	// 		return;
	// 	}
	// 	GD.Print("Exited");
	// 	QueueFree();
	// }

	// public void _BodyEntered(Node body)
	// {
	// 	GD.Print(body);
	// 	if (body != this)
	// 	{
	// 		return;
	// 	}
	// 	GD.Print("Entered");
	// }


	#endregion
}
