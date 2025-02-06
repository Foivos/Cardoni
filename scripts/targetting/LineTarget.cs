namespace Cardoni;

public class LineTarget : EntityTarget
{
	int Line => testTargetC.inst.Line;

	public LineTarget() { }

	public override Entity[] Targets()
	{
		return new Entity[0];
	}
}
