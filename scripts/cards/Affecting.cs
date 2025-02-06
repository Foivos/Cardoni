namespace Cardoni;

using Godot;

public class Affecting : ICardEffect
{
	public virtual bool Affects(Entity entity)
	{
		return true;
	}

	public virtual void Affect(Entity entity)
	{
		GD.Print("Affecting ", entity);
	}
}
