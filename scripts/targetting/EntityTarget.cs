namespace Cardoni;

using System.Collections.Generic;

public abstract class EntityTarget : CardTarget
{
	public abstract List<Entity> Targets();
}
