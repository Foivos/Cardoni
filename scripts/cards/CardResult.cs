namespace Cardoni;

using System.Collections.Generic;

public abstract class CardResult {
    public List<CardTarget> Targets;
    public List<IActive> Effects;

    public abstract void Activate();
}