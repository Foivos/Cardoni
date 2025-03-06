namespace Cardoni;

using Godot;
using System;


[GlobalClass]
public partial class ActionCardResult : CardResult
{
    static readonly maskTarget target = new();// will be useless 
    public override CardTarget Target => target;// will be useless 



    [Export] GeneralActive active;


    public ActionCardResult() { }

    public override void Activate()
    {

        active.Activate();


    }



}
