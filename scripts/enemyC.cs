using System;
using Godot;

public partial class enemyC : RigidBody2D
{



    public override void _Ready()
    {

        setUpHealthBar();



    }


    //[Export] RigidBody2D rb;
    [Export] TextureProgressBar hpBar;
    [Export] Sprite2D sprite;

    [Export] int hp;
    public void onDamage(int amount)
    {
        hp -= amount;


        if (hp <= 0) QueueFree();
        else
        {
            if (hpBar != null) hpBar.Value = hp;
            battleEffectsC.inst.addHitTwo(sprite);
            battleEffectsC.inst.doShake(sprite);
        }

        GD.Print("dmg: " + amount + " HP: " + hp);

    }

    void setUpHealthBar()
    {
        if (hpBar != null)
        {

            hpBar.MaxValue = hp;
            hpBar.Value = hp;
            hpBar.MinValue = 0;

        }

    }
    void displayHealthBar()
    {
        if (hpBar == null) return;




        hpBar.Value = Math.Clamp(hp, 5, hpBar.Value - 5);

        hpBar.Visible = true;


    }

    #region  MOVEMENT

    [Export] float speed;

    [Export] float temorarySpeed;
    public bool speedAdjusted;

    public void processMovement() // every frame called
    {


        Position += new Vector2(0, temorarySpeed);
    }


    #endregion





    private void OnVisibleOnScreenNotifier2DScreenExited()
    {
        GD.Print("Screen exited DIE");
        QueueFree();
    }



}
