namespace Cardoni;

using Godot;
using Godot.Collections;

[Tool]
[GlobalClass]
public partial class CardRegistry : Resource
{
	[Export]
	public Array<CardResource> Cards = new();
}
