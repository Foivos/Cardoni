using Cardoni;
using Godot;
using System;

public static class gregF
{


	public static bool rBool() { return new Random().Next(0, 2) == 0; }
	public static int rDir() { return rBool() ? 1 : -1; }
	public static double r(float a, float b)
	{ return GD.RandRange((double)a, (double)b); }
	public static double r(float a)
	{ return GD.RandRange(0, (double)a); }




	public static float angle(Vector2 p_vector2)
	{
		// WORKSSS //
		if (p_vector2.X < 0)
		{
			//return (Mathf.Atan2(p_vector2.X, p_vector2.Y) * Mathf.Rad2Deg * -1);
			return Mathf.RadToDeg(Mathf.Atan2(p_vector2.X, p_vector2.Y)) * -1;
		}
		else
		{
			return 360 - Mathf.RadToDeg(Mathf.Atan2(p_vector2.X, p_vector2.Y));
		}

	}


	//!EXTENTIONS
	public static float rDir(this float a) { return a * rDir(); }
	public static int rDir(this int a) { return a * rDir(); }


	public static int getDirection(this Entity ent)
	{

		//! GOT TIRED OF THIS SHIT
		//! GOT TIRED OF THIS SHIT
		//! GOT TIRED OF THIS SHIT
		//! GOT TIRED OF THIS SHIT

		bool lookingDown = false;

		if (ent == null) { GD.PushError("dead enemy get direction"); return 0; }

		if (ent.Direction != 0) lookingDown = ent.Direction == 1;
		else if (ent is AttackingEntity atacker
		&& atacker.Target != null &&
		atacker.Target.Parent != null &&
		atacker.Target.Parent.Sprite != null

		&& ent.Parent != null && ent.Parent.Sprite != null)
		{
			lookingDown = atacker.Target.Parent.Sprite.GlobalPosition.Y
		> ent.Parent.Sprite.GlobalPosition.Y;
		}
		//else GD.PushError("NEED BETTER WAY OF KNOWING DIRECTION");

		return lookingDown ? 1 : -1;

	}



}
