using System;
using Godot;

public partial class testTargetC : Node
{


    public static testTargetC inst;
    public enum targetTypes { None, Line, Position }//todo future MORE  //, enemy


    [ExportGroup("PROPERTIES")]

    [Export] Sprite2D positionTarget, lineTarget;
    [Export] targetTypes targetNow = targetTypes.None;





    public override void _Ready()
    {
        inst = this;
        screenSize = GetViewport().GetVisibleRect().Size;

        //backround.GlobalPosition = new Vector2(screenSize.X / 2f, screenSize.Y / 2f);
        screenBlock = screenSize.X / 4;
    }


    public void beginTargeting(targetTypes type)
    {

        targetNow = type;
        SetProcess(true);

        positionTarget.Visible = targetNow == targetTypes.Position;
        lineTarget.Visible = targetNow == targetTypes.Line;
        //enemyTarget.Visible = targetNow == targetTypes.enemy;



    }
    public void endTargeting()
    {
        positionTarget.Visible = false;
        lineTarget.Visible = false;
        SetProcess(false);

    }
    public ITarget targetResult()
    {

        if(targetNow == targetTypes.Position)
        {
            return new PositionTarget(positionTarget.GlobalPosition);
        }
        else if(targetNow == targetTypes.Line)
        {
            return new LineTarget(0);
        }


      
        return new InvalidTarget();

    }


    void processGraphics()
    {

        if (targetNow == targetTypes.None)return;

        if (targetNow == targetTypes.Position)
        {
            positionTarget.GlobalPosition = targetPos;
        }
        else if (targetNow == targetTypes.Line)
        {
            //lineTarget.GlobalPosition = targetPos;
            //lineTarget.Rotation = (float)Math.Atan2(targetPos.Y, targetPos.X);
            //lineTarget.Scale = new Vector2(targetPos.Length(), 1);
        }




    }



    [ExportGroup("DEBUG THINGS")]
    [Export] Vector2 screenSize;
    [Export] float screenBlock;

    [Export] Vector2 mousePos;
    [Export] Vector2 targetPos;
    [Export] Label targetGlobalPosLabel;
    [Export] Label targetPosLabel;


    public override void _Process(double delta)
    {
        mousePos = GetViewport().GetMousePosition();
        targetPos = gridTargetPosition(GetViewport().GetMousePosition());
        //targetGraphic.GlobalPosition = gridTargetPosition(GetViewport().GetMousePosition());

        //targetGlobalPosLabel.Text = "GP" + targetGraphic.GlobalPosition.ToString();
        //targetPosLabel.Text = "G" + targetGraphic.Position.ToString();
    }




    public static Vector2 getMouseWorldScreen()
    {
        if (inst == null) return Vector2.Zero;


        return inst.GetViewport().GetMousePosition() - inst.GetViewport().GetVisibleRect().Size / 2;
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

  
}
