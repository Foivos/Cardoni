using Godot;
using System;
using System.Collections.Generic;

public partial class battleEffectsC : Node
{


	public static battleEffectsC inst;
	public override void _Ready() { inst = this; }


	[Export] Sprite2D testSprite;
	[Export] Material whiteMat, blackMat;
	[Export]public float effectDelay;



	public override void _Process(double delta) { _ProcessEffects(); }

	public override void _Input(InputEvent @event)
	{

		addHitTwo(testSprite);
	}


	void _ProcessEffects()
	{

		if (effects.Count == 0) return;
		float time = Time.GetTicksMsec();

		for (int i = effects.Count - 1; i >= 0; i--)
		{
			if (effects[i].untill > time) continue;
			effects[i].update(time, out bool removeMe);
			if (removeMe) effects.RemoveAt(i);
		}


	}
	List<spriteEffect> effects = new List<spriteEffect>();


	class spriteEffect//? origin
	{
		public Sprite2D sprite;
		public int counter;
		public float untill;

		public virtual void update(float time, out bool removeMe) { removeMe = false; }
	}
	class hitOne : spriteEffect
	{


		public override void update(float time, out bool removeMe)
		{
			if (sprite == null) { removeMe = true; return; }

			removeMe = false;
			counter++;
			untill = time + 80;



			if (counter == 1) { sprite.Visible = false; }
			else { sprite.Visible = true; removeMe = true; }


		}


	}
	class hitTwo : spriteEffect
	{


		public override void update(float time, out bool removeMe)
		{
			if (sprite == null) { removeMe = true; return; }

			removeMe = false;
			counter++;
			untill = time + inst.effectDelay * 1000;



			if (counter == 1) { sprite.Material = inst.blackMat; }
			else if (counter == 2) { sprite.Material = inst.whiteMat; }
			else { sprite.Material = null; removeMe = true; }


		}



	}


	public void addHitOne(Sprite2D sprite) { effects.Add(new hitOne() { sprite = sprite }); }
	public void addHitTwo(Sprite2D sprite) { effects.Add(new hitTwo() { sprite = sprite }); }






}
