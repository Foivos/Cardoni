using System;
using Godot;

namespace Cardoni;

public partial class AuraData : CharacteristicData
{
	[Export]
	public EntityFilter EntityFilter { get; set; }

	[Export]
	public ConditionData Condition;

	public override Aura Create(Entity entity)
	{
		return new Aura(entity, this);
	}
}
