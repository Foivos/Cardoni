namespace Cardoni;

using Godot;

[GlobalClass]
public partial class ConditionData : Resource
{
	[Export]
	public uint Duration { get; set; } = 0;

	[Export]
	public uint Strength { get; set; } = 1;

	[Export]
	public EffectType EffectType { get; set; }

	public ConditionData() { }

	public ConditionData(EffectType effectType, uint duration = 0, uint strength = 1)
	{
		EffectType = effectType;
		Duration = duration;
		Strength = strength;
	}
}
