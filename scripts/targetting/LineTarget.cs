namespace Cardoni;

using System.Collections.Generic;
using Godot;

[GlobalClass]
public partial class LineTarget : EntityTarget
{
	int Line => TargetView.Instance.Line;

	public LineTarget() { }

	public override List<Entity> Targets()
	{
		return new List<Entity>();
	}
}
