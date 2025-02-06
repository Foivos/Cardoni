namespace Cardoni;

public class AffectingPoison : Affecting
{
	public uint Duration { get; set; }
	public uint Strength { get; set; }

	public AffectingPoison(uint duration, uint strength) {
		Duration = duration;
		Strength = strength;
	}

	public override void Affect(Entity entity)
	{
		Poisoned.Apply(entity, Duration, Strength);
	}
}
