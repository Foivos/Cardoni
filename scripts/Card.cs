using System;
using Godot;
using State;

public partial class Card : Area2D
{
    public uint Index;
    public bool selected;

    public Action<Vector2> OnPlay;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        SetPosition();
        InputEvent += _Input;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta) { }

    public void SetPosition()
    {
        Vector2 screenSize = GetViewport().GetVisibleRect().Size;
        Position = new Vector2(screenSize.X * (2.0f * Index - 3.0f) / 8, 0);
    }

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
            GD.Print(Index);
            //GameState.SelectedCard.OnPlay(eventMouseButton.Position);
        }
    }
}
