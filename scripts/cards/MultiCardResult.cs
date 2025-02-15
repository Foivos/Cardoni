namespace Cardoni;

using System;
using Godot;

[GlobalClass]
public partial class MultiCardResult : CardResult
{
	CardResult[] results;

	[Export]
	public CardResult[] Results
	{
		get => results;
		set
		{
			results = value;
			MultiTarget = new MultiTarget() { Targets = Array.ConvertAll(value, (result) => result.Target) };
		}
	}

	public override CardTarget Target => MultiTarget;

	MultiTarget MultiTarget { get; set; }

	public MultiCardResult()
		: base() { }

	public override void Activate()
	{
		foreach (CardResult result in Results)
		{
			result.Activate();
		}
	}
}
