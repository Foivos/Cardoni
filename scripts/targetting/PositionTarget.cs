namespace Cardoni;

using Godot;

[GlobalClass]
public partial class PositionTarget : CardTarget
{
	Vector2 Position => TargetView.Instance.Position;

	public PositionTarget() { }
}
