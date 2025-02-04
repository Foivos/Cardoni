using System;
using Godot;

public partial class GameView : Area2D
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        InputEvent += _Input;
        MouseExited += _MouseExited;
        MouseEntered += _MouseEntered;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta) { }

    public void _Input(Node viewport, InputEvent @event, long shapeIdx)
    {
        if (@event is InputEventMouseButton eventMouseButton)
        {
            if (
                eventMouseButton.ButtonIndex != MouseButton.Left
                || eventMouseButton.Pressed
                || GameState.SelectedCard == null
            )
                return;

            GameState.SelectedCard.OnPlay(eventMouseButton.Position);
        }
        else if (@event is InputEventMouseMotion eventMouseMotion) { }
    }

    public void _MouseEntered() { }

    public void _MouseExited() { }
}
