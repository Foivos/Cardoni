namespace Cardoni;

using Godot;

public class PositionTarget : CardTarget
{
	Vector2 Position => TargetView.Instance.Position;

	public PositionTarget() { }
}
