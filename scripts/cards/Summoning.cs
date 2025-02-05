namespace Cardoni;

using Godot;

public class Summoning : ICardEffect
{
	public void Summon(Vector2 position)
	{
		GD.Print("Summoning at ", position);
	}
}
