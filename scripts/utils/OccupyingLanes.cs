namespace Cardoni;

using System;

public class OccupyingLanes
{
	readonly int from,
		to;

	public int From => from;
	public int To => to;

	public OccupyingLanes(int _from, int _to)
	{
		if (from > to)
			throw new Exception("Occupying Lanes cannot have a larger from than to");
		from = _from;
		to = _to;
	}

	public OccupyingLanes(int single)
		: this(single, single) { }

	public bool Intersects(OccupyingLanes other)
	{
		return From <= other.To && To >= other.From;
	}

	public bool IsIn(uint lane)
	{
		return From <= lane && To >= lane;
	}
}
