namespace Cardoni;

using Godot;

[GlobalClass]
public partial class CardResource : Resource {
    [Export]
    public Texture2D Sprite { get; set; }
    [Export]
    public string Name { get; set; }
}