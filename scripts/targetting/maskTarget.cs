namespace Cardoni;

using Godot;
using System.Collections.Generic;


[GlobalClass]
public partial class maskTarget : EntityTarget
{


    public maskTarget() { }

    public override List<Entity> Targets()
    {
        return GameState.Entities.FindAll((entity)
        =>  EntityMask.Matches(entity.Mask));
    }

    
}
