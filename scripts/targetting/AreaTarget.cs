namespace Cardoni;

using Godot;

public class AreaTarget : EntityTarget
{
	Vector2 Position { get; set; }
	double Radius { get; set; }

	public AreaTarget(double radius)
	{
		Radius = radius;
	}

	public override Entity[] Targets()
	{
		return new Entity[0];
	}
}
