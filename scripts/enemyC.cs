using System;
using Godot;

public partial class enemyC : RigidBody2D
{
    #region  MOVEMENT STUFF

    //?SYSTEM FOR SLOWS and SPEED BUFFS

    // do 0.5 adjust to slow to half , do 1.5 to speed up to 1.5x
    // changes are additive so if it has one 0.5f and one 1.5f its gona be basic speed
    //! TIME MEASURED IN MILI - SECONDS	 so 2 seconds == 2000

    [Export]
    float speed;

    [Export]
    float temorarySpeed;
    public bool speedAdjusted;
    float speedAdjustUntill;

    public void processMovement() // every frame called
    {
        if (speedAdjusted && speedAdjustUntill < Time.GetTicksMsec())
            calculateSpeedAdjust();

        Position += new Vector2(0, temorarySpeed);
    }

    void calculateSpeedAdjust()
    {
        speedAdjusted = false;
        temorarySpeed = 1;

        for (int i = 0; i < speedModifiers.Length; i++)
        {
            if (speedModifiers[i].imEnabled == false)
                continue;
            else if (speedModifiers[i].until < Time.GetTicksMsec())
            {
                speedModifiers[i].imEnabled = false;
                continue;
            }

            speedAdjusted = true;

            if (speedModifiers[i].adjust < 0)
                temorarySpeed -= speedModifiers[i].adjust;
            else
                temorarySpeed += speedModifiers[i].adjust - 1;

            if (speedAdjustUntill > speedModifiers[i].until)
                speedAdjustUntill = speedModifiers[i].until;
        }

        if (temorarySpeed < 0)
            temorarySpeed = 0f;
        else
            temorarySpeed *= speed;
    }

    public void addSpeedAdjust(float adjust, float duration)
    {
        speedAdjusted = true;

        for (int i = 0; i < speedModifiers.Length; i++)
        {
            if (!speedModifiers[i].imEnabled)
            {
                speedModifiers[i].imEnabled = true;
                speedModifiers[i].adjust = adjust;
                speedModifiers[i].until = Time.GetTicksMsec() + duration * 1000;
                return;
            }
        }

        calculateSpeedAdjust();
        GD.Print("No more speed modifiers available L == " + speedModifiers.Length);
    }

    speedModifier[] speedModifiers = new speedModifier[5];

    struct speedModifier
    {
        public bool imEnabled;
        public float until;
        public float adjust;
    }

    #endregion




    public new virtual void _Ready() { }

    public override void _Process(double delta)
    {
        //QueueFree();
    }

    private void OnVisibleOnScreenNotifier2DScreenExited()
    {
        GD.Print("Screen exited DIE");
        QueueFree();
    }
}
