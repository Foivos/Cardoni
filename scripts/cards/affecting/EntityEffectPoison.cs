namespace Cardoni;

using Godot;

[GlobalClass]
public partial class EntityEffectPoison : EntityActive
{
	[Export]
	public uint Duration { get; set; }

	[Export]
	public uint Strength { get; set; }

	public EntityEffectPoison()
		: base() { }

	public EntityEffectPoison(uint duration, uint strength)
	{
		Duration = duration;
		Strength = strength;
	}

	public override void Activate(Entity entity)
	{
		new Poisoned(entity, Duration, Strength);
	}
}
