using System;
using Godot;

public partial class testTargetC : Node
{
    // Called when the node enters the scene tree for the first time.


    public static testTargetC inst;

    public override void _Ready()
    {
        inst = this;
        screenSize = GetViewport().GetVisibleRect().Size;

        //backround.GlobalPosition = new Vector2(screenSize.X / 2f, screenSize.Y / 2f);
        screenBlock = screenSize.X / 4;
    }

    [Export]
    Vector2 screenSize;

    [Export]
    float screenBlock;

    public static Vector2 getMouseWorldScreen()
    {
        if (inst == null)
            return Vector2.Zero;

        return inst.GetViewport().GetMousePosition() - inst.GetViewport().GetVisibleRect().Size / 2;
    }

    [Export]
    Node2D backround;

    [Export]
    Node2D targetGraphic;

    [Export]
    Vector2 mousePos;

    [Export]
    Vector2 targetPos;

    [Export]
    Label targetGlobalPosLabel;

    [Export]
    Label targetPosLabel;

    public override void _Process(double delta)
    {
        mousePos = GetViewport().GetMousePosition();

        targetPos = gridTargetPosition(GetViewport().GetMousePosition());
        targetGraphic.GlobalPosition = gridTargetPosition(GetViewport().GetMousePosition());

        targetGlobalPosLabel.Text = "GP" + targetGraphic.GlobalPosition.ToString();
        targetPosLabel.Text = "G" + targetGraphic.Position.ToString();
    }

    Vector2 gridTargetPosition(Vector2 pos)
    {
        pos -= screenSize / 2;

        // new Vector2(pos.X / screenBlock, pos.Y / screenBlock);

        if (pos.X < -screenBlock)
            pos.X = -1.5f;
        else if (pos.X < 0)
            pos.X = -0.5f;
        else if (pos.X < screenBlock)
            pos.X = 0.5f;
        else
            pos.X = 1.5f;

        pos.Y /= (screenBlock / 4);

        //float remainer = pos.Y % 4;
        pos.Y = (float)Math.Round(pos.Y);
        pos.Y /= 4;
        // if (remainer > 0.25f)
        // {
        // 	if(pos.Y > 0) pos.Y += 0.5f;
        // 	else pos.Y -= 0.5f;

        // }




        return pos * screenBlock; // + Vector2.One * screenBlock / 2;
    }

    //GetViewport().GetMousePosition();
}
