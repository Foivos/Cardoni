namespace Cardoni;

using Godot;

[GlobalClass]
public abstract partial class CardResult : Resource
{
	public abstract CardTarget Target { get; }

	public CardResult() { }

	public abstract void Activate();

	
}
