using System;

namespace Cardoni;

public class ConditionPassive : Passive
{
	public Func<Entity, Condition> GenerateCondition { get; set; }

	public ConditionPassive(Entity entity, EntityMask targetMask, Func<Entity, Condition> generateCondition)
		: base(entity, targetMask)
	{
		GenerateCondition = generateCondition;
	}

	public override void ApplyEffect(Entity entity)
	{
		ActiveConditions.Add(GenerateCondition(entity));
	}
}
