namespace Cardoni;

using Godot;

public class PositionTarget: CardTarget {
    Vector2 Position { get; set; }

    public PositionTarget(Vector2 positions) {
        Position = positions;
    }
}
