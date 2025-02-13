namespace Cardoni;

using Godot;

[GlobalClass]
public partial class TestRegistry : Resource
{
	[Export]
	CardRegistry[] Registry { get; set; }
}
