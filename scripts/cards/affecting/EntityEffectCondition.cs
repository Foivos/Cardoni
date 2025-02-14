namespace Cardoni;

using Godot;

[GlobalClass]
public partial class EntityEffectCondition : EntityActive
{
	[Export]
	public ConditionData Data;

	public EntityEffectCondition()
		: base() { }

	public override void Activate(Entity entity)
	{
		new Condition(entity, Data);
	}
}
