namespace Cardoni;

using System.Collections.Generic;
using System.Linq;
using Godot;

[GlobalClass]
public partial class EntityActiveChain : EntityActive
{
	[Export]
	public uint Number;

	[Export]
	public EntityFilter ValidTargets;

	[Export]
	public EntityActive Active;

	public EntityActiveChain()
		: base() { }

	public override void Activate(Entity entity)
	{
		List<Entity> affected = new() { entity };

		for (int i = 0; i < Number; i++)
		{
			var entities = GameState.Entities.FindAll(
				(other) =>
				{
					return ValidTargets.IsValid(entity, other) && !affected.Contains(other);
				}
			);

			if (entities.Count == 0)
				break;
			entity = entities.First();
			affected.Add(entity);
			Active.Activate(entity);
		}
	}
}
