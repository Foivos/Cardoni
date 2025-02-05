namespace Cardoni;

using Godot;

public class Affecting : ICardEffect
{
	public bool Affects(Entity entity)
	{
		return true;
	}

	public void Affect(Entity entity)
	{
		GD.Print("Affecting ", entity);
	}
}
