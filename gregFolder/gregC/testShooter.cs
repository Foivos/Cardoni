using System;
using Godot;

public partial class testShooter : Node
{
    [Export]
    PackedScene bulletPreffab;

    [Export]
    float speed;

    public override void _Input(InputEvent @event)
    {
        //shoot();
    }

    float lastShoot;
    float minShootDelay = 0.5f;

    void shoot()
    {
        if (Time.GetTicksMsec() - lastShoot < minShootDelay * 1000)
            return;
        lastShoot = Time.GetTicksMsec();

        testBullet bull = bulletPreffab.Instantiate<testBullet>();

        // bull.Position = testTargetC.mousePosCenterZero();

        // bull.LinearVelocity = new Vector2(0, speed);
        // bull.GravityScale = 0;

        // Spawn the mob by adding it to the Main scene.
        AddChild(bull);
    }
}
