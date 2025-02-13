namespace Cardoni;

using System;
using System.Collections.Generic;
using Godot;

public partial class battleEffectsC : Node
{
	public static battleEffectsC inst;

	public override void _Ready()
	{
		inst = this;
	}

	[ExportGroup("test group ")]
	[Export]
	Sprite2D testSprite;

	[Export]
	Material whiteMat,
		blackMat;

	[Export]
	public float effectDelay;

	[ExportGroup("test camera shake")]
	[Export]
	public float shakeCameraDuration = 0.08f;

	[Export]
	public float shakeCameraStrenght = 10;

	public override void _Process(double delta)
	{
		_ProcessSpriteEffects();
	}

	public override void _Input(InputEvent @event)
	{
		if (testSprite == null)
			return;

		if (@event.IsActionPressed("ui_right"))
			addHitOne(testSprite);
		// if (@event.IsActionPressed("ui_left"))
		// 	dmgEffect(testSprite);

		if (@event.IsActionPressed("ui_up"))
			cameraShake.StartShake(shakeCameraDuration, shakeCameraStrenght);

		if (@event.IsActionPressed("ui_down"))
			doBackroundFlash();

		if (@event.IsActionPressed("ui_accept"))
			doShake(testSprite);
	}



	#region  SPRITE EFFECTS

	List<spriteEffect> _spriteEffect = new List<spriteEffect>();

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

	public class spriteEffect //? origin
	{
		public Sprite2D sprite;
		public int counter;
		public float untill;

		public virtual void update(float time, out bool removeMe)
		{
			removeMe = false;
		}
	}

	class hitOne : spriteEffect
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

	public class dmgEffect : spriteEffect
	{

		public dmgEffect(Sprite2D victim)
		{
			if (inst == null || sprite == null) return;
			
			sprite = victim;
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
			untill = time + inst.effectDelay * 1000;

			if (counter == 1)
				sprite.Modulate = new Color(0.2f, 0.2f, 0.2f);
			else if (counter == 2)
				sprite.Modulate = new Color(1f, 1f, 1f);
			else
				removeMe = true;
		}
	}



	public void addHitOne(Sprite2D sprite)
	{
		_spriteEffect.Add(new hitOne() { sprite = sprite });
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
