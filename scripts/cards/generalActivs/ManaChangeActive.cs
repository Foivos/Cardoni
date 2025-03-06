
using Cardoni;
using Godot;
using System;

[GlobalClass]
public partial class ManaChangeActive : GeneralActive
{


    [Export] int manaChange = 0;

    public ManaChangeActive() { }

    public override void Activate()
    {
        GameState.Instance.Mana += manaChange;
    }

}
