namespace Cardoni;

using Godot;

[GlobalClass]
public partial class AttackData : CharacteristicData
{
	public AttackData() { }

	[Export]
	public EntityFilter TargetFilter { get; set; } =
		new AndFilter()
		{
			Filter1 = new LaneFilter() { RelativeLane = 0 },
			Filter2 = new EnemiesFilter(),
		};

	[Export]
	public EntityFilter AttackFilter { get; set; } = new VerticalDistanceFilter() { Within = 0 };

	[Export(PropertyHint.Range, "0,1200")]
	public uint StartingStacks { get; set; } = 600;

	[Export]
	public EntityActive Active { set; get; }

	public override Attack Create(Entity entity)
	{
		return new Attack(entity, this);
	}
}
