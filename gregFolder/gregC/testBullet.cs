using System;
using Godot;

public partial class testBullet : Area2D
{




    public override void _Ready()
    {

        //var area = GetNode<Area2D>("Area2D");
        //area.Connect("body_entered", this, "OnBodyEntered");//? required?
        //Connect("body_entered", this, nameof(OnBodyEntered));
        //GravityScale = 0;

    }



    //? connected through inspector signal

    // made it work with both bullet and eneme having area2d - collision shape undeneath
    private void OnBodyEntered(Node body)
    {
        GD.Print("OnBodyEntered " + body.Name);


        //if (body.IsInGroup("enemies") == false) { GD.Print("not enemy group"); return; }

        if (body is enemyC enemy) { enemy.onDamage(1); }
        else { GD.Print("not enemy"); return; }
        //var enemy = body.GetNodeOrNull<enemyC>("enemyC");

        //if (enemy == null) { GD.Print("null enemy"); return; }




        QueueFree();

        //enemy.QueueFree();
    }
}
