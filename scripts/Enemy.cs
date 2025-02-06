namespace Cardoni;

using Godot;

public partial class Enemy : Node2D
{
	//public Area2D GameView;
	//[Export] public CollisionShape2D CollisionShape;
	[Export] Sprite2D sprite;
	[Export] public TextureProgressBar healthBar;
	public uint lane;


	public override void _Ready()
	{
		//GameView.BodyExited += _BodyExited;
		healthBar.Value = 100;

		hp = _hp;
		attackDamage = _attackDamage;
	}


	public override void _Process(double delta)
	{
		processMovement(delta);
		// 	displayHealthBar();

		//GD.Print("enemyC _Process");
	}




	#region  TAKE DAMAGE + HEALTH BAR + DEATH


	[Export] uint _hp;
	uint hp
	{
		get { return _hp; }
		set
		{
			_hp = value;
			if (healthBar == null) { GD.Print("no health bar " + Name); return; }

			healthBar.MaxValue = hp;
			healthBar.Value = hp;
			healthBar.MinValue = 0;

		}
	}

	public void onDamage(uint amount)
	{
		hp -= amount;

		if (hp <= 0) onDeath();
		else
		{
			if (healthBar != null)
				healthBar.Value = hp;
			battleEffectsC.inst.addHitTwo(sprite);
			battleEffectsC.inst.doShake(sprite);
		}

		GD.Print("dmg: " + amount + " HP: " + hp);
	}
	public void onDeath()
	{

		spawning.enemyDied(this);
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

	#region  MOVEMENT




	[ExportGroup("movement")]
	[Export]
	public bool stopMoving = true;

	[Export]
	public float speed;

	const float downLimit = 200; // after that die and deal damage for now

	public void processMovement(double delta) // every frame called
	{
		if (stopMoving)
			return;

		Position += new Vector2(0, speed) * (float)delta;

		if (Position.Y < downLimit)
			return;

		testPlayer.damage(1);
		QueueFree();
	}



	#endregion




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
