namespace Cardoni;

using System;
using Godot;

public partial class testBullet : Area2D
{
	[Export]
	int speed;
	public uint damage;
	public bool goingUp;

	public override void _Process(double delta) //? mmoved from here
	{
		if (goingUp)
			Position += new Vector2(0, -1) * (float)delta * speed;
		else
			Position += new Vector2(0, 1) * (float)delta * speed;
	}

	//? connected through inspector signal

	// made it work with both bullet and eneme having area2d - collision shape undeneath
	private void OnBodyEntered(Node body)
	{
		GD.Print("OnBodyEntered " + body.Name);

		if (body is monsterBase monster)
		{
			monster.onDamage(damage);
		}
		else if (body is minnionBase minion)
		{
			minion.onDamage(damage);
		}
		else if (body is Enemy enm)
		{
			enm.onDamage(damage);
		}

		//GD.Print("DEATH BULLET " + body.Name );
		SetProcess(false);
		Visible = false;
		QueueFree();
	}

	const int enemyMask = 1;
	const int minionMask = 2;

	public void targerEnemies()
	{
		CollisionMask = enemyMask;
		goingUp = true;
	}

	public void targetMinions()
	{
		CollisionMask = minionMask;
		goingUp = false;

		Rotation = Mathf.DegToRad(180);
	}
}
