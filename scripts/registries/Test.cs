using Godot;

namespace Cardoni;

public partial class Test : Node
{
	[Export]
	CardRegistry Resource { get; set; }
}
