namespace Cardoni;

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


        //lineTarget.
    }


    public void beginTargeting(targetTypes type)//! MAIN FUNCTION
    {

        targetNow = type;
        SetProcess(true);

        positionTarget.Visible = targetNow == targetTypes.Position;
        lineTarget.Visible = targetNow == targetTypes.Line;
        //enemyTarget.Visible = targetNow == targetTypes.enemy;



    }
    public void endTargeting()//! MAIN FUNCTION
    {
        positionTarget.Visible = false;
        lineTarget.Visible = false;

        SetProcess(false);

    }
    public CardTarget targetResult()//! MAIN FUNCTION
    {

        if (targetNow == targetTypes.Position)
            return new PositionTarget(positionTarget.GlobalPosition);

        else if (targetNow == targetTypes.Line) return new LineTarget(getSelectedLine());




        return new InvalidTarget();

    }




    void processGraphics()//? GRAGHICS HERE - like line posision
    {

        if (targetNow == targetTypes.None) return;

        if (targetNow == targetTypes.Position)
        {
            positionTarget.Position = positionToGrid(mouseWorldPos);
        }
        else if (targetNow == targetTypes.Line)
        {


            tempLine = getSelectedLine();
            lineTarget.Position = new Vector2((-1.5f + tempLine) * lineLenght, lineTarget.Position.Y);

        }




    }
    int getSelectedLine()
    {

        if (mouseWorldPos.X < -lineLenght) return 0;
        else if (mouseWorldPos.X < 0) return 1;
        else if (mouseWorldPos.X < lineLenght) return 2;
        else return 3;

    }



    [ExportGroup("DEBUG THINGS")]
    [Export] Vector2 screenSize;
    [Export] float lineLenght;
    [Export] int tempLine = 0;

    [Export] Vector2 mousePos, mouseWorldPos;



    public override void _Process(double delta)
    {
        mousePos = GetViewport().GetMousePosition();
        mouseWorldPos = mousePosCenterZero();

        processGraphics();



    }
    public override void _Input(InputEvent @event)//? EDIT ONLY
    {
        if (@event.IsActionPressed("ui_accept")) beginTargeting(targetNow);

    }



    Vector2 mousePosCenterZero()
    {
        if (inst == null) return Vector2.Zero;


        return inst.GetViewport().GetMousePosition() - inst.GetViewport().GetVisibleRect().Size / 2;
    }

    Vector2 positionToGrid(Vector2 pos)
    {
        //pos -= screenSize / 2;

        // new Vector2(pos.X / screenBlock, pos.Y / screenBlock);

        if (pos.X < -lineLenght)
            pos.X = -1.5f * lineLenght;
        else if (pos.X < 0)
            pos.X = -0.5f * lineLenght;
        else if (pos.X < lineLenght)
            pos.X = 0.5f * lineLenght;
        else
            pos.X = 1.5f * lineLenght;


        float remainder = pos.Y % lineLenght;
        int yBlockCount = (int)MathF.Round(pos.Y / lineLenght);


        if (remainder < 0.125f * lineLenght) remainder = 0;
        else if (remainder < 0.375 * lineLenght) remainder = 0.25f * lineLenght;
        else if (remainder < 0.625 * lineLenght) remainder = 0.5f * lineLenght;
        else if (remainder < 0.875 * lineLenght) remainder = 0.75f * lineLenght;
        else remainder = lineLenght;

        pos.Y = (yBlockCount * lineLenght) + remainder;



        return pos;
    }





}
