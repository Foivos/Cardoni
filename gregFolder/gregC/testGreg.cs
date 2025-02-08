using System;
using Godot;

public partial class testGreg : Node2D
{
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		timer += delta;
		if (timer > raycastCooldown)
		{
			raycast();
			timer = 0;
		}
	}

	double timer = 0;

	[Export]
	double raycastCooldown;

	[Export]
	uint layerMask;

	[Export]
	Node2D testTarget;

	void raycast()
	{
		GD.Print("testTarget POS  " + testTarget.Position);

		// Get the space state for performing physics queries
		var spaceState = GetWorld2D().DirectSpaceState;

		// Perform the raycast and store the result in a dictionary
		PhysicsRayQueryParameters2D par = new PhysicsRayQueryParameters2D();
		//var query = PhysicsRayQueryParameters2D.Create(this.Position, Target.Position);//
		par.From = GlobalPosition;
		par.To = GlobalPosition + new Vector2(0, -1000);
		GD.Print("raycast from " + par.From + " to " + par.To);
		//par.CollisionMask = layerMask;
		//if (type == entityType.player)
		//else if (type == entityType.enemy) par.CollisionMask = 1;

		var result = spaceState.IntersectRay(par);

		//Object Collider = result["collider"] as Object;

		// if(Collider == Target)
		// {
		// 	GD.Print("u wot m8");
		// 	return true;
		// }

		GD.Print(" results " + result.Count);
		GD.Print(" results " + result.Count);
		GD.Print(" results " + result.Count);
		GD.Print(" results " + result.Count);
		GD.Print(" results " + result.Count);

		if (result.Count > 0)
		{
			// Get the collider (the object hit by the ray)
			Node2D hitObject = (Node2D)result["collider"];
			if (hitObject is monsterBase)
			{
				GD.Print(" results " + ((monsterBase)hitObject).Name);
				//return (monsterBase)hitObject;
			}
			else
				GD.Print("NULL hitObject");
		}
	}

	//if (body.IsInGroup("enemies") == false) { GD.Print("not enemy group"); return; }
	//! looks usefull






	//? RAYCAST  detects only rigidBody with kid collider for now   ..  not just area with collider i think
	public void targetDetection(Vector2 startPos, Vector2 endPos)
	{
		// GD.Print("RAY");

		// // Function that performs a raycast and returns the first object hit in a given direction
		// // public Node2D PerformDirectRaycast(Vector2 startPosition, Vector2 direction, float length)
		// // {
		// // Get the space state for performing physics queries
		// var spaceState = GetWorld2D().DirectSpaceState;


		// // Perform the raycast and store the result in a dictionary
		// PhysicsRayQueryParameters2D par = new PhysicsRayQueryParameters2D();
		// par.From = startPos;
		// par.To = endPos;

		// if (type == entityType.player) par.CollisionMask = 2;
		// else if (type == entityType.enemy) par.CollisionMask = 1;

		// var result = spaceState.IntersectRay(par);

		// // Check if the ray hit anything
		// if (result.Count > 0)
		// {


		// 	// Get the collider (the object hit by the ray)
		// 	Node2D hitObject = (Node2D)result["collider"];
		// 	if (hitObject is monsterBase) return (monsterBase)hitObject;
		// 	else GD.Print("NULL hitObject");

		// }
		// else GD.Print("o results");



		// return null;
	}
}
