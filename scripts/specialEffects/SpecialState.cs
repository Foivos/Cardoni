namespace Cardoni;

using System.Collections.Generic;
using System.Text.RegularExpressions;
using Godot;

public partial class SpecialState : Node
{
	[Export]
	Node2D AnimationNode { get; set; }

	[Export]
	Node2D LabelNode { get; set; }

	public static SpecialState Instance { get; set; }
	PriorityQueue<ProcessExpiring> Expiring { get; set; } = new();

	public static float CurrentTime => (float)Time.GetTicksMsec() / 1000;

	public Node2D Background { get; set; }

	public NodePool<Animation> Animations { get; set; } =
		new()
		{
			Instantiate = () =>
			{
				Animation animation = new Animation();
				Instance.AnimationNode.AddChild(animation);
				return animation;
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

	public NodePool<Label> Labels { get; set; } =
		new()
		{
			Instantiate = () =>
			{
				Label label = new Label();
				Instance.AnimationNode.AddChild(label);
				return label;
			},
			Initialize = label =>
			{
				label.Visible = true;
			},
			Deactivate = label =>
			{
				label.Visible = false;
			},
		};

	public override void _Ready()
	{
		Instance = this;
		Background = (Node2D)FindParent("MainScene").FindChild("Background");
	}

	public override void _Process(double delta)
	{
		base._Process(delta);
		float t = CurrentTime;
		while (Expiring.Count > 0 && Expiring.Top.End <= t)
		{
			ProcessExpiring expiring = Expiring.Pop();
			expiring.OnExpire();
			if (expiring.Repeat != 1)
			{
				if (expiring.Repeat > 1)
				{
					expiring.Repeat--;
				}
				expiring.End = expiring.End + expiring.Duration;
				AddExpiring(expiring);
			}
		}
	}

	public static void AddExpiring(ProcessExpiring expiring)
	{
		Instance.Expiring.Push(expiring);
	}

	public static bool RemoveExpring(ProcessExpiring expiring)
	{
		return Instance.Expiring.Remove(expiring);
	}

	public static Animation GetAnimation()
	{
		return Instance.Animations.Get();
	}

	public static ProcessExpiring RemoveLater(Node2D node, float duration)
	{
		return new ProcessExpiring(
			duration,
			() =>
			{
				node.Visible = false;
				node.SetProcess(false);
			}
		);
	}

	public static ProcessExpiring RotateLater(Node2D node, float degrees, float duration)
	{
		return new ProcessExpiring(
			duration,
			() =>
			{
				node.Rotate(degrees);
			}
		);
	}

	public static ProcessExpiring TransformLater(Node2D node, Transform2D transform, float duration)
	{
		return new ProcessExpiring(
			duration,
			() =>
			{
				node.Transform = transform;
			}
		);
	}

	public static ProcessExpiring ResizeLater(Node2D node, float scale, float duration)
	{
		return new ProcessExpiring(
			duration,
			() =>
			{
				node.Scale *= scale;
			}
		);
	}

	public static ProcessExpiring HitEffect(Node2D node, float duration)
	{
		bool visible = true;
		return new ProcessExpiring(
			duration,
			() =>
			{
				visible = !visible;
				node.Visible = visible;
			},
			2
		);
	}

	public static ProcessExpiring HitDamage(
		Sprite2D sprite,
		float duration = 0.02f,
		float blackColor = 0.1f,
		float hitDmgDelay = 0.03f,
		int possitionOffset = -20
	)
	{
		uint counter = 0;
		void onExpire()
		{
			if (counter == 100 || !IsInstanceValid(sprite)) { counter = 100; return; }


			counter++;
			//GD.Print(counter);
			if (counter == 1)
			{
				sprite.Modulate = new Color(blackColor, blackColor, blackColor);
			}
			else if (counter == 2)
			{
				sprite.Modulate = Colors.DarkRed;
				sprite.Offset = Vector2.Up * possitionOffset;
			}
			else
			{
				sprite.Modulate = Colors.White;
				sprite.Offset = Vector2.Zero;
			}
		}
		;
		return new ProcessExpiring(duration, onExpire, 3);
	}

	public static ProcessExpiring pushShadowTrail(
			Entity entity //, int pushDistance
		)
	{
		//int distanceForOneShadow = 300;
		int shadowStep = 45;
		float delay = 0.04f;// 0.04f;
		float aStep = 0.3f;//0.2f;
		float aStart = 0.9f;
		List<Sprite2D> shadows = new();

		int shadowCount = 2;// (int)((float)pushDistance / shadowStep);
							//if (shadowCount < 1) shadowCount = 1;
							//GD.Print("shadowCount: " + shadowCount + " pushDistance: " + pushDistance);

		for (int i = 0; i < shadowCount; i++)
		{
			var s = new Sprite2D();

			s.Texture = entity.Sprite.Texture;
			s.Modulate = new Color(1, 1, 1, aStart - i * aStep);//(shadowCount - i - 1)

			s.Scale = entity.Sprite.Scale;
			s.GlobalPosition = entity.GlobalPosition + entity.FacingDirection
			* new Vector2(0, shadowStep * (i + 1));  //pushDistance 


			//GD.Print("ENTITY: " + entity.GlobalPosition);
			//GD.Print("POS: " + s.GlobalPosition);


			Instance.AddChild(s);
			shadows.Add(s);

		}



		void onExpire()
		{


			//counter++;uint counter = 0;
			//GD.Print("push shadow " +counter);

			if (shadows.Count == 0) return;

			bool done = true;

			for (int i = 0; i < shadows.Count; i++)
			{
				//if (shadows[i].Modulate.A < 0.05f) continue;


				shadows[i].Modulate = new Color(1, 1, 1, shadows[i].Modulate.A - aStep);
				//GD.Print("A ==  " + shadows[i].Modulate.A);

				if (shadows[i].Modulate.A > 0.1f) done = false;


			}

			if (done)
			{
				for (int i = shadows.Count - 1; i >= 0; i--)
				{
					shadows[i].QueueFree();
				}
				shadows.Clear();

			}


		}

		return new ProcessExpiring(delay, onExpire, 5);
	}



	public static ProcessExpiring BackgroundFlash(Color flashColor, float duration)
	{
		if (Instance.Background == null)
			return null;

		Color previousModulation = Instance.Background.Modulate;
		Instance.Background.Modulate = flashColor;

		return new ProcessExpiring(
			duration,
			() =>
			{
				Instance.Background.Modulate = previousModulation;
			}
		);
	}

	public static ProcessExpiring BackgroundFlash()
	{
		return BackgroundFlash(new Color(0, 0, 0), 0.02f);
	}

	public static void EntityBlood(Entity entity)
	{
		bool dead = !entity.IsAlive;
		int direction = entity.FacingDirection;


		const int offsetY = -50;
		const int offsetX = -6;
		const int randomX = 3;
		const int randomRotation = 5;

		const float scale = 2.5f, scaleDead = 3.5f;


		float XXX = offsetX * direction
		+ (float)gregF.r((float)randomX) * gregF.rDir();

		Animation animation = GetAnimation();
		animation.Position = entity.GlobalPosition + new Vector2(XXX, direction * offsetY);
		animation.RotationDegrees = (direction == 1 ? 180 : 0)
		 + randomRotation.rDir();
		animation.Scale = Vector2.One * (dead ? scaleDead : scale);
		animation.ZIndex = -5;

		(float, int)[] frames = new (float, int)[7];
		for (int i = 0; i < 7; i++) { frames[i] = (0.02f, i); }
		animation.Play(GD.Load<Texture2D>("res://gregFolder/images/testBlood.png"), 3, 3, frames, 1);
	}
}
