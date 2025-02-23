namespace Cardoni;

using Godot;

public partial class SpecialState : Node2D
{
	public static SpecialState Instance { get; set; }
	PriorityQueue<ProcessExpiring> Expiring { get; set; } = new();

	public NodePool<Sprite2D> Animations { get; set; } =
		new()
		{
			Instantiate = () =>
			{
				Sprite2D sprite = new Sprite2D();
				Instance.AddChild(sprite);
				return sprite;
			},
			Initialize = sprite =>
			{
				sprite.Visible = true;
			},
			Deactivate = sprite =>
			{
				sprite.Visible = false;
			},
		};

	public static float CurrentTime => (float)Time.GetTicksMsec() / 1000;

	public static void AddExpiring(ProcessExpiring expiring)
	{
		Instance.Expiring.Push(expiring);
	}

	public static bool RemoveExpring(ProcessExpiring expiring)
	{
		return Instance.Expiring.Remove(expiring);
	}

	public override void _Process(double delta)
	{
		base._Process(delta);
		float t = CurrentTime;
		while (Expiring.Count > 0 && Expiring.Top.End <= t)
		{
			ProcessExpiring expiring = Expiring.Pop();
			expiring.OnExpire();
		}
	}
}
