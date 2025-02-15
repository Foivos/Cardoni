using Godot;
using System;

public static class gregF
{


	public static bool rBool() { return new Random().Next(0, 2) == 0; }
	public static double r(float a, float b)
	{ return GD.RandRange((double)a, (double)b); }


}
