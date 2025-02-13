namespace Cardoni;

using System.Collections.Generic;

public abstract class CardResult
{
	public List<CardTarget> Targets { get; set; }
	public List<IActive> Effects { get; set; }

	public abstract void Activate();
}
