namespace Cardoni;

using Godot;

[GlobalClass]
public partial class CardRegistry : Resource {
    [Export]
    public CardResource[] Cards { get; set; }
    [Export]
    public int Test { get; set; }
}