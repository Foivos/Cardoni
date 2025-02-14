using Godot;

namespace Cardoni;

public partial class CardTest : Node
{
	public CardTest() { }

	[Export]
	public Card Card { get; set; }
}
