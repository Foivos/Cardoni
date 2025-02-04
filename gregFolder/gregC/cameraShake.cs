using Godot;
using System;

public partial class cameraShake : Camera2D
{

	static cameraShake inst;
	[Export] private float shakeDuration = 0f;
	[Export] private float shakeMagnitude = 0f;
	[Export] private Vector2 originalPosition;

	public override void _Ready() { originalPosition = Position; inst = this; }

	public override void _Process(double delta)
	{
		if (shakeDuration > 0)
		{

			//GD.Print("shakeDuration " + shakeDuration);

			// Generate random offsets to create the shake effect
			Vector2 shakeOffset = new Vector2(
				(float)GD.RandRange(-1, 1) * shakeMagnitude,
				(float)GD.RandRange(-1, 1) * shakeMagnitude
			);

			// Apply the offset to the camera position
			Position = originalPosition + shakeOffset;

			// Reduce the shake duration over time
			shakeDuration -= (float)delta;
		}
		else
		{
			// Reset the camera to its original position when the shake is over
			Position = originalPosition;
			SetProcess(false);
		}
	}

	// Method to start the shake with a specific duration and magnitude
	public static void StartShake(float duration, float magnitude)
	{


		if (inst == null) { GD.Print("NO CAMERA SHAKE"); return; }

		if (inst.IsProcessing()) return;

		inst.SetProcess(true);
		inst.shakeDuration = duration;
		inst.shakeMagnitude = magnitude;
	}
}
