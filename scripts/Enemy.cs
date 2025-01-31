using Godot;

public partial class Enemy: RigidBody2D {
	public Area2D GameView;
	[Export]
	public CollisionShape2D CollisionShape;

	public override void _Ready()
	{
		GameView.BodyExited += _BodyExited;
	}

	public void _BodyExited(Node body) {
		GD.Print(body);
		if (body != this) {
			return;
		}
		GD.Print("Exited");
        QueueFree();
	}

		public void _BodyEntered(Node body) {
		GD.Print(body);
		if (body != this) {
			return;
		}
		GD.Print("Entered");
	}
}
