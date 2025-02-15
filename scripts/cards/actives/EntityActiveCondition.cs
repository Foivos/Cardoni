namespace Cardoni;

using Godot;

[GlobalClass]
public partial class EntityActiveCondition : EntityActive
{
	[Export]
	public ConditionData Data;

	public EntityActiveCondition()
		: base() { }

	public override void Activate(Entity entity)
	{
		new Condition(entity, Data);
	}
}
