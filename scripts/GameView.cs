namespace Cardoni;

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
            
            CardTarget result = testTargetC.inst.targetResult();
            GD.Print(result);
            GameState.SelectedCard = null;
        }
        else if (@event is InputEventMouseMotion eventMouseMotion)
        {
            if (
                GameState.SelectedCard == null
            )
                return;
            GD.Print(GameState.SelectedCard.Index);
        }
    }

    public void _MouseEntered()
    {
        if (GameState.SelectedCard == null) return;
        testTargetC.targetTypes targetType = testTargetC.targetTypes.None;
        switch (GameState.SelectedCard.Index) {
            case 0: {
                targetType = testTargetC.targetTypes.Position;
                break;
            }
            case 1: {
                targetType = testTargetC.targetTypes.Line;
                break;
            }
            case 2: {
                targetType = testTargetC.targetTypes.None;
                break;
            }
        }

        testTargetC.inst.beginTargeting(targetType);//! CHOOSE TYPE
    }

    public void _MouseExited()
    {

        if (GameState.SelectedCard == null) return;

        testTargetC.inst.endTargeting();
    }
}
