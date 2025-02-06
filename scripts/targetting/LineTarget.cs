namespace Cardoni;

using System.Collections.Generic;

public class LineTarget : EntityTarget
{
	int Line => TargetView.Instance.Line;

	public LineTarget() { }

	public override List<Entity> Targets()
	{
		return new List<Entity>();
	}
}
