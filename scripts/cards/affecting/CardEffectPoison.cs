namespace Cardoni;

public class CardEffectPoison : EntityActive
{
	public uint Duration { get; set; }
	public uint Strength { get; set; }

	public CardEffectPoison(uint duration, uint strength)
	{
		Duration = duration;
		Strength = strength;
	}

	public override void Activate(Entity entity)
	{
		Poisoned.Apply(entity, Duration, Strength);
	}
}
