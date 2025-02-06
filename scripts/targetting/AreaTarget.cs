namespace Cardoni;

using System.Collections.Generic;
using Godot;

public class AreaTarget : EntityTarget
{
	Vector2 Position => TargetView.Instance.Position;
	double Radius { get; set; }

	public AreaTarget(double radius)
	{
		Radius = radius;
	}

	public override List<Entity> Targets()
	{
		return new List<Entity>();
	}
}
