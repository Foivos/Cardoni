namespace Cardoni;

using System.Collections.Generic;
using Godot;

[GlobalClass]
public partial class MultiTarget : CardTarget
{
	[Export]
	public CardTarget[] Targets { get; set; }

	public MultiTarget()
		: base() { }
}
