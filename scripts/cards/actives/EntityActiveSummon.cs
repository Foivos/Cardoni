namespace Cardoni;

using Godot;

[GlobalClass]
public partial class EntityActiveSummon : EntityActive
{
	[Export]
	public EntityData Summon { get; set; }

	[Export]
	public int RelativeLane { get; set; } = 0;

	public EntityActiveSummon() { }

	public override void Activate(Entity entity)
	{
		SpawnManager.Spawn(entity.OccupyingLanes.From + RelativeLane, entity.Y, Summon);
	}
}
