namespace Cardoni;

using System;
using System.Collections.Generic;
using Godot;

public partial class battleEffectsC : Node
{

	#region  BASICS
	public static battleEffectsC inst;
	public override void _Ready() { inst = this; }

	public override void _Input(InputEvent @event)// for testing only
	{
		// if (testSprite == null)
		// 	return;

		// if (@event.IsActionPressed("ui_right"))
		// 	addHitOne(testSprite);
		// if (@event.IsActionPressed("ui_left"))
		// 	addHitTwo(testSprite);

		// if (@event.IsActionPressed("ui_up"))
		// 	cameraShake.StartShake(shakeCameraDuration, shakeCameraStrenght);

		// if (@event.IsActionPressed("ui_down"))
		// 	doBackroundFlash();

		// if (@event.IsActionPressed("ui_accept"))
		// 	doShake(testSprite);
	}



	public override void _Process(double delta) { _ProcessSpriteEffects(); }






	#endregion


	#region  particle effects 

	[ExportGroup("particles")]
	[Export] CpuParticles2D hitParticles;
	[Export] float hitParticlesOffset;

	public static void doHitParticles(Vector2 pos)
	{

		bool side = gregF.rBool();
		//GD.Print("side: " + side);

		inst.hitParticles.Position = pos
		 + new Vector2(side ? -inst.hitParticlesOffset : inst.hitParticlesOffset, inst.hitParticlesOffset);

		inst.hitParticles.RotationDegrees = side ? 45 : -45;
		inst.hitParticles.RotationDegrees += (float)gregF.r(-10f, 10f);

		inst.hitParticles.Emitting = true;

	}



	#endregion

	#region  SPRITE EFFECTS





	List<effect> _spriteEffect = new List<effect>();

	void _ProcessSpriteEffects()
	{
		if (_spriteEffect.Count == 0)
			return;
		float time = Time.GetTicksMsec();

		for (int i = _spriteEffect.Count - 1; i >= 0; i--)
		{
			if (_spriteEffect[i].untill > time)
				continue;
			_spriteEffect[i].update(time, out bool removeMe);
			if (removeMe)
				_spriteEffect.RemoveAt(i);
		}
	}

	public class effect //? ORIGIN
	{

		public int counter;
		public float untill;
		public virtual void update(float time, out bool removeMe)
		{
			removeMe = false;
		}

	}




	public class invisibleLater : effect
	{

		Node2D theNode;

		public invisibleLater(Node2D _node, float delay)
		{

			if (inst == null || _node == null) return;

			untill = Time.GetTicksMsec() + delay * 1000;
			theNode = _node;
			inst._spriteEffect.Add(this);

		}

		public override void update(float time, out bool removeMe)
		{

			removeMe = true;
			if (theNode != null) theNode.Visible = false;


		}


	}
	public class rotateLater : effect
	{
		Node2D theNode;
		int degrees;

		public rotateLater(Node2D _node, float delay, int _degrees)
		{

			if (inst == null || _node == null) return;

			untill = Time.GetTicksMsec() + delay * 1000;
			theNode = _node;
			degrees = _degrees;
			inst._spriteEffect.Add(this);

		}

		public override void update(float time, out bool removeMe)
		{

			removeMe = true;
			theNode.RotationDegrees += degrees;



		}



	}
	public class resizeLater : effect
	{
		Node2D theNode;
		float sizeAdjust;

		public resizeLater(Node2D _node, float delay, float _sizeAdjust)
		{

			if (inst == null || _node == null) return;

			untill = Time.GetTicksMsec() + delay * 1000;
			theNode = _node;
			sizeAdjust = _sizeAdjust;
			inst._spriteEffect.Add(this);

		}

		public override void update(float time, out bool removeMe)
		{

			removeMe = true;
			theNode.Scale *= sizeAdjust;



		}



	}



	public class spriteEffect : effect //? origin
	{
		public Sprite2D sprite;



	}


	class hitWithInvisbleNotNice : spriteEffect
	{
		public override void update(float time, out bool removeMe)
		{
			if (sprite == null)
			{
				removeMe = true;
				return;
			}

			removeMe = false;
			counter++;
			untill = time + 80;

			if (counter == 1)
			{
				sprite.Visible = false;
			}
			else
			{
				sprite.Visible = true;
				removeMe = true;
			}
		}
	}

	public class hitDmg : spriteEffect
	{


		const float blackColor = 0.1f;
		const float hitDmgDelay = 0.03f;
		const int possitionOffset = -20;

		public hitDmg(Sprite2D _sprite)
		{

			if (inst == null || _sprite == null)
				return;

			sprite = _sprite;
			inst._spriteEffect.Add(this);

		}
		public override void update(float time, out bool removeMe)
		{
			if (sprite == null)
			{
				removeMe = true;
				return;
			}

			removeMe = false;
			counter++;
			untill = time + hitDmgDelay * 1000;

			if (counter == 1)
				sprite.Modulate = new Color(blackColor, blackColor, blackColor);
			else if (counter == 2)
			{
				sprite.Modulate = Colors.DarkRed;
				sprite.Offset = Vector2.Up * possitionOffset;
			}
			else
			{
				sprite.Modulate = Colors.White;
				sprite.Offset = Vector2.Zero;
				removeMe = true;
			}

		}
	}

	public void addHitOne(Sprite2D sprite)
	{
		_spriteEffect.Add(new hitWithInvisbleNotNice() { sprite = sprite });
	}



	class shakeSprite : spriteEffect
	{
		public override void update(float time, out bool removeMe)
		{
			if (sprite == null)
			{
				removeMe = true;
				return;
			}

			removeMe = true;
			sprite.Offset = Vector2.Zero;
			sprite.Rotation = 0;
		}
	}

	[ExportGroup("shake sprite effec")]
	[Export]
	public float shakeSpriteDuration = 0.08f;

	[Export]
	public float shakeSpriteOffset = 2f;

	[Export]
	public float shakeSpriteRotation = 5f;

	private Vector2 GetRandomDirection()
	{
		// Generate a random angle between 0 and 2*PI (full circle)
		float angle = (float)GD.RandRange(0, Mathf.Pi * 2);

		// Convert the angle to a normalized direction vector
		float x = Mathf.Cos(angle);
		float y = Mathf.Sin(angle);

		return new Vector2(x, y).Normalized();
	}

	public void doShake(Sprite2D sprite)
	{
		if (sprite == null)
			return;

		sprite.Offset = GetRandomDirection() * shakeSpriteOffset;
		if (GD.Randi() % 2 == 0)
			sprite.RotationDegrees = shakeSpriteRotation;
		else
			sprite.RotationDegrees = -shakeSpriteRotation;

		_spriteEffect.Add(
			new shakeSprite() { sprite = sprite, untill = Time.GetTicksMsec() + shakeSpriteDuration * 1000 }
		);
	}

	#endregion

	#region  poolable markers

	List<Sprite2D> markersPool;

	public Sprite2D getMarker(Vector2 position = default
	, Color color = default, float lifetime = -1, string message = "")
	{
		if (markersPool == null) markersPool = new List<Sprite2D>();
		Sprite2D marker = null;

		for (int i = 0; i < markersPool.Count; i++)
		{
			if (markersPool[i].Visible == false) { marker = markersPool[i]; break; }

		}

		if (marker == null)// spwawn more
		{

			for (int i = 0; i < 5; i++)
			{
				marker = new Sprite2D();

				AddChild(marker);
				marker.Name = "marker with text child " + markersPool.Count;
				markersPool.Add(marker);

				marker.Texture = GD.Load<Texture2D>("res://gregFolder/images/square.png");
				marker.Visible = false;
			}



		}



		marker.Position = position;

		if (color != default) marker.SelfModulate = color;
		else marker.Modulate = new Color(1, 1, 1, 0.5f);

		if (lifetime > 0) new invisibleLater(marker, lifetime);




		// GD.PushError("1111");
		// GD.PushWarning("2222");


		if (marker.GetChildCount() == 0)
		{
			var label = new Label();
			marker.AddChild(label);
			label.Scale = new Vector2(2f, 2f);
			label.Position = new Vector2(0, 0);
			label.Text = message;
			label.SelfModulate = Colors.Coral;


		}
		else if (marker.GetChild(0) is Label)
		{

			marker.GetChild<Label>(0).Text = message;
		}
		else
		{
			GD.Print("BATTLE EFFECTS MARKER ERROR marker has no label");
			GD.Print("BATTLE EFFECTS MARKER ERROR marker has no label");
			GD.Print("BATTLE EFFECTS MARKER ERROR marker has no label");
		}




		marker.Visible = true;
		return marker;


	}


	#endregion

	#region  BACKROUND FLASH

	class backroundFlassEffect : spriteEffect
	{
		public override void update(float time, out bool removeMe)
		{
			if (sprite == null)
			{
				removeMe = true;
				return;
			}

			removeMe = true;
			inst.backround.Modulate = new Color(1, 1, 1, 1);
		}
	}

	[ExportGroup("flash backround")]
	[Export]
	Sprite2D backround;

	[Export]
	float backroundEffectDelay;

	[Export]
	Color backroundEffeColor;

	void doBackroundFlash()
	{
		if (backround == null)
			return;

		backround.Modulate = backroundEffeColor;

		_spriteEffect.Add(
			new backroundFlassEffect()
			{
				sprite = backround,
				untill = Time.GetTicksMsec() + backroundEffectDelay * 1000,
			}
		);
	}

	#endregion




}
