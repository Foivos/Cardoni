namespace Cardoni;

using Godot;

public class AreaTarget : EntityTarget
{
	Vector2 Position => testTargetC.inst.Position;
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
