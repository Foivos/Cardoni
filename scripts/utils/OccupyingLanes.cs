namespace Cardoni;

using System;

public class OccupyingLanes
{
	readonly uint from,
		to;

	public uint From => from;
	public uint To => to;

	public OccupyingLanes(uint _from, uint _to)
	{
		if (from > to)
			throw new Exception("Occupying Lanes cannot have a larger from than to");
		from = _from;
		to = _to;
	}

	public OccupyingLanes(uint single)
		: this(single, single) { }

	public bool Intersects(OccupyingLanes other)
	{
		return !(From > other.To && To < other.From);
	}

	public bool IsIn(uint lane)
	{
		return From <= lane && To >= lane;
	}
}
