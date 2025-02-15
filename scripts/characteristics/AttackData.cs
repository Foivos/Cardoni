namespace Cardoni;

using Godot;

[GlobalClass]
public partial class AttackData : CharacteristicData
{
	public AttackData() { }

	[Export]
	public EntityFilter TargetFilter { get; set; } = new LaneFilter() { RelativeLane = 0 };

	[Export]
	public EntityFilter AttackFilter { get; set; } = new VerticalDistanceFilter() { Within = 0 };

	[Export]
	public uint StartingStacks { get; set; } = 600;

	[Export]
	public EntityActive Active { set; get; }

	public override Attack Create(Entity entity)
	{
		return new Attack(entity, this);
	}
}
