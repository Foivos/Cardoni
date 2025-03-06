using Cardoni;
using Godot;
using System;

[GlobalClass]
public partial class MultiGeneralActive : GeneralActive
{

    [Export] GeneralActive[] actions = new GeneralActive[0];

    public MultiGeneralActive() { }

    public override void Activate()
    {
        foreach (GeneralActive action in actions)
        {
            action.Activate();
        }
    }

}
