namespace Cardoni;

using System.Collections.Generic;
using Godot;

public abstract partial class EntityValidation : Resource
{
	public abstract bool IsValid(Entity source, Entity other);

	public virtual IEnumerable<Entity> Entities(Entity source)
	{
		return GameState.Entities.FindAll((entity) => IsValid(source, entity));
	}
}
