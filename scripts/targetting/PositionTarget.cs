namespace Cardoni;

using Godot;

public class PositionTarget : CardTarget
{
	Vector2 Position => testTargetC.inst.Position;

	public PositionTarget() { }
}
