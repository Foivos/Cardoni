namespace Cardoni;

using System.Collections.Generic;

public abstract class EntityTarget : CardTarget
{
	public EntityMask EntityMask { get; set; } = new(new EntityMasks[0]);

	public abstract List<Entity> Targets();
}
