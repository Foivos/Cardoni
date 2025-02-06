using Cardoni;
using Godot;
using System;

public partial class monsterBase : Node2D
{


	[Export] Sprite2D sprite;
	public uint lane;

	public override void _Ready()
	{

		if (healthBar != null)
		{
			healthBar.MaxValue = hp;
			healthBar.MinValue = 0;

		}


		hp = _hp;
		attackDamage = _attackDamage;
	}


	public override void _Process(double delta)
	{
		if (isAtacking) atack(delta);


	}




	#region  TAKE DAMAGE + HEALTH BAR + DEATH

	[Export] public TextureProgressBar healthBar;
	[Export] uint _hp;
	uint hp
	{
		get { return _hp; }
		set
		{
			_hp = value;
			if (healthBar == null) { GD.Print("no health bar " + Name); return; }
			if (_hp == 0) return;


			healthBar.Value = _hp;


		}
	}

	public virtual void onDamage(uint amount)
	{
		hp = hp - amount;

		if (hp <= 0) onDeath();
		else
		{

			battleEffectsC.inst.addHitTwo(sprite);
			battleEffectsC.inst.doShake(sprite);

			GD.Print("dmg: " + amount + " HP: " + hp);
		}


	}
	public void onDeath()
	{

		spawning.monsterDied(this);
		QueueFree();

	}


	#endregion


	#region  shord that dynamicaly changes with atack damage

	[ExportGroup("shord dmg assumed 1-10")]
	[Export]
	uint _attackDamage;

	uint attackDamage
	{ // ? the fact that im here i mean this >> { << is a gift to you foivos ( if this is not the case sorry my formatter changed it .. )
		get { return _attackDamage; }
		set
		{
			_attackDamage = value;
			if (shord == null) { GD.Print("no shord " + Name); return; }

			if (_attackDamage == 0) { shord.Visible = false; return; }

			shord.Scale = Vector2.One * Mathf.Lerp(sizes.X, sizes.Y, _attackDamage / 10f);
		}
	}



	[Export]
	Sprite2D shord;

	[Export]
	Vector2 sizes;

	#endregion


	#region  ATACKING  test version  controlled by spawner for now .. 


	public bool isAtacking;
	[Export] bool isRanged;
	[Export] float _atackRange;
	const float defaultMeeleRange = 100;
	public float atackRange
	{
		get { if (isRanged == false) return defaultMeeleRange; return _atackRange; }
		private set { }

	}
	[Export] float atackCooldown;
	double atackTimer;
	public monsterBase target;



	public virtual void atack(double time)
	{

		if (isAtacking == false) return;

		atackTimer += time;
		if (atackTimer < atackCooldown) return;
		atackTimer = 0;


		if (target != null && target.hp > 0) target.onDamage(attackDamage);


	}




	public virtual void rangeCalulations() { }

	public enum entityType { player, enemy, all }
	public monsterBase targetDetection(Vector2 startPos, Vector2 endPos, entityType type)
	{
		GD.Print("RAY");

		// Function that performs a raycast and returns the first object hit in a given direction
		// public Node2D PerformDirectRaycast(Vector2 startPosition, Vector2 direction, float length)
		// {
		// Get the space state for performing physics queries
		var spaceState = GetWorld2D().DirectSpaceState;


		// Perform the raycast and store the result in a dictionary
		PhysicsRayQueryParameters2D par = new PhysicsRayQueryParameters2D();
		par.From = startPos;
		par.To = endPos;

		if (type == entityType.player) par.CollisionMask = 2;
		else if (type == entityType.enemy) par.CollisionMask = 1;

		var result = spaceState.IntersectRay(par);

		// Check if the ray hit anything
		if (result.Count > 0)
		{


			// Get the collider (the object hit by the ray)
			Node2D hitObject = (Node2D)result["collider"];
			if (hitObject is monsterBase) return (monsterBase)hitObject;
			else GD.Print("NULL hitObject");

		}
		else GD.Print("o results");



		return null;





	}





	#endregion




}
