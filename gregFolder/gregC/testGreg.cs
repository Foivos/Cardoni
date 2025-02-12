using Cardoni;
using Godot;
using System;
using System.Collections;

public partial class testGreg : Node2D
{

	//! set up your math functions and extentions	

	public override void _Input(InputEvent @event)
	{

		if (@event is InputEventMouseButton == false) return;


		if (testAnim) playAnim();
		if (testBlood) spawnParticleThere(GetGlobalMousePosition(), 4);


		if (testAtack && doingEffect == false) atackAnim();
		//var newOne = start atackRoutine();

		if (testTiledAnim) changeTiledFrame();

	}


	#region  tiled animation    


	//! made tileAnim c# that does it well enough 
	//worked through c# changing the texture


	[ExportGroup("TILED ANIMATION")]
	[Export] bool testTiledAnim;
	[Export] TextureRect tiledRect;
	[Export] Texture2D tiledFrame0, tiledFrame1;
	bool tilerFrameBool;

	void changeTiledFrame()
	{
		GD.Print("changeTiledFrame");
		tiledRect.Texture = tiledFrame1;
		//tiledRect.Texture = tilerFrameBool ? tiledFrame0 : tiledFrame1;
		//tilerFrameBool = !tilerFrameBool;
	}




	#endregion


	#region  PARTICLES

	//? PARTICLES
	[Export] bool testBlood;
	[Export] CpuParticles2D particles;
	void spawnParticleThere(Vector2 pos, float scale)
	{
		particles.Emitting = true;
		particles.Position = pos;
		particles.ScaleAmountMin = scale;
		particles.ScaleAmountMax = scale;

		// particles.Direction = Vector2.Up; //! works fine 
		// particles.AngleMin = 180 -45;
		// particles.AngleMax = 180 +45;



		//! to rotate must change angle and Direction both

	}

	#endregion


	#region  ANIMATION

	[ExportGroup("ANIMATION")]
	[Export] bool testAnim;
	[Export] AnimatedSprite2D anim;
	[Export] float animRotaion;
	void playAnim()
	{
		anim.Position = GetGlobalMousePosition();
		anim.Rotation = Time.GetTicksMsec() % 1000 > 500 ? animRotaion : -animRotaion;
		anim.Play();



	}

	#endregion




	#region  ATACK EFFECT


	async void targetDamage()
	{
		await ToSignal(GetTree().CreateTimer(targetDamageDelay), "timeout");

		battleEffectsC.inst.addHitTwo(target);
		battleEffectsC.inst.doShake(target);

	}
	async void atackAnim()
	{
		if (doingEffect) return;
		doingEffect = true;

		targetDamage();


		atacker.RotationDegrees = -atackerRotation;
		shord.Material = redOutline;

		await ToSignal(GetTree().CreateTimer(delay), "timeout");

		atacker.RotationDegrees = 0;
		atacker.Offset = new Vector2(0, atackerOffset);
		shord.ZIndex = 5;
		shord.Offset = shordOffset;
		shord.RotationDegrees = shordRotation;


		await ToSignal(GetTree().CreateTimer(delay), "timeout");

		atacker.Offset = Vector2.Zero;



		await ToSignal(GetTree().CreateTimer(delay), "timeout");

		shord.ZIndex = 0;
		shord.RotationDegrees = 0;
		shord.Offset = Vector2.Zero;
		shord.Material = null;
		//shord.TopLevel = false;




		await ToSignal(GetTree().CreateTimer(0.5f), "timeout");
		doingEffect = false;


	}


	[ExportGroup("atack anim")]
	[Export] bool testAtack;
	[Export] Material redOutline;
	[Export] Sprite2D atacker;
	[Export] Sprite2D shord;
	[Export] Sprite2D target;
	[Export] float atackerRotation;
	[Export] float atackerOffset;
	[Export] float shordRotation;
	[Export] Vector2 shordOffset;
	[Export] float delay;
	[Export] float targetDamageDelay;
	bool doingEffect;


	#endregion




	#region  RAYCAST SHIT

	void raycastProcess(double delta)
	{
		timer += delta;
		if (timer > raycastCooldown)
		{
			raycast();
			timer = 0;
		}


	}


	double timer = 0;
	double raycastCooldown;

	uint layerMask;
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
			else GD.Print("NULL hitObject");

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





	#endregion





}
