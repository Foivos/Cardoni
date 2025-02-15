using System;
using System.Collections.Generic;
using Godot;

namespace Cardoni;

public partial class Aura : Characteristic<AuraData>
{
	public EntityFilter EntityFilter => Data.EntityFilter;

	public ConditionData Condition => Data.Condition;

	public List<Condition> ActiveConditions { get; set; } = new();

	public Aura(Entity entity, AuraData data)
		: base(entity, data) { }

	public override void Start()
	{
		GameState.Entities.ForEach(OnSpawn);
		Events.OnSpawn += OnSpawn;
	}

	public override void End()
	{
		ActiveConditions.ForEach((condition) => condition.End());
		Events.OnSpawn -= OnSpawn;
	}

	protected virtual void OnSpawn(Entity entity)
	{
		if (EntityFilter.IsValid(Entity, entity))
		{
			ActiveConditions.Add(new Condition(entity, Condition));
		}
	}
}
