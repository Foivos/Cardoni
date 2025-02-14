namespace Cardoni;

using Godot;

[GlobalClass]
public abstract partial class CardResult : Resource
{
	[Export]
	public CardTarget[] Targets { get; set; }

	[Export]
	public Active[] Effects { get; set; }

	public CardResult() { }

	public abstract void Activate();
}
