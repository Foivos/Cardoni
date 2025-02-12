namespace Cardoni;

public enum EntityMasks
{
	Mobile,
	Structure,
	Enemy,
	Friendly,
	Boss,
}

public class EntityMask
{
	public uint Mask { get; set; }

	public EntityMask(EntityMasks[] masks)
	{
		foreach (EntityMasks mask in masks)
		{
			Mask |= (uint)(1 << (int)mask);
		}
	}

	public bool Matches(EntityMask other)
	{
		return (Mask & other.Mask) != 0;
	}
}
