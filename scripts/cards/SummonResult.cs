namespace Cardoni;

using Godot;

[GlobalClass]
public partial class SummonResult : CardResult
{
	static readonly PositionTarget target = new();
	public override CardTarget Target => target;

	[Export]
	public EntityData Summon { get; set; }

	public override void Activate()
	{
		Vector2I offset = TargetView.Instance.GetCurrentOffset(new());
		SpawnManager.Spawn(offset.X, offset.Y * Constants.GridTicks, Summon);
	}
}
