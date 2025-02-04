using Godot;

public class PositionTarget: ITarget {
    Vector2 Position { get; set; }

    public PositionTarget(Vector2 positions) {
        Position = positions;
    }
}
