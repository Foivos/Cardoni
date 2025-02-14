using Cardoni;
using Godot;
using System;
using System.Collections.Generic;

public partial class textEffects : Node2D
{


	static textEffects inst;
	public override void _Ready()
	{


		inst = this;

		texts = new List<(Node2D, Label)> { new(labelHolderPresset, labelPresset) };
		labelHolderPresset.Visible = false;
	}


	[Export] string testSay;
	[Export] Vector2 testPos;
	[Export] Color col;
	[Export] float duration;
	[Export] float size;

	[Export] Node2D labelHolderPresset;
	[Export] Label labelPresset;
	//[Export] bool testFIRST;

	public override void _Input(InputEvent @event)
	{

		if (@event is InputEventMouseButton == false) return;


		addText(testSay, GetGlobalMousePosition(), duration, size, col);
	}


	//struct textHoler { public Label text; public float duration; }
	Vector2 textOffset = new Vector2(-6, -10);// new Vector2(-16, -12);
	List<(Node2D, Label)> texts = new List<(Node2D, Label)>();
	(Node2D, Label) getText()
	{
		// if (testFIRST)
		// {
		// 	texts[0].Item1.Visible = true;
		// 	return texts[0];


		// }

		if (texts == null) texts = new List<(Node2D, Label)>();

		//int closed = 0;
		for (int i = 0; i < texts.Count; i++)
		{
			if (texts[i].Item1.Visible == false)
			{
				texts[i].Item1.Visible = true;
				return texts[i];
			}
			//else closed++;

		}

		//GD.Print("SPAWN new text" + closed);


		(Node2D, Label) item = (null, null);

		for (int i = 0; i < 5; i++)
		{
			var father = labelHolderPresset.Duplicate();  // new Node2D();

			AddChild(father);
			//if (labelHolderPresset.GetChild(0) is Label label == false)




			//label.HorizontalAlignment = HorizontalAlignment.Center;
			//label.VerticalAlignment = VerticalAlignment.Center;
			//label.SetAnchorsPreset(Control.LayoutPreset.Center);

			//father.AddChild(label);
			//label.Position = textOffset;
			//AddChild(father);


			item = ((Node2D)father, (Label)father.GetChild(0));


			texts.Add(item);
			father.Name = "poolable text "+texts.Count;

						item.Item1.Visible = false;

		}

		return item;


	}


	public static void displayDmgText(Entity entity, int amount)
	{
		// Color redLowAlpha = new Color(1, 0, 0, 0.5f);
		// battleEffectsC.inst.getMarker(entity.Parent.GlobalPosition
		// , color: redLowAlpha, lifetime: 2, message: amount.ToString());


		bool enemy = entity.Mask.Matches(new EntityMask(new EntityMasks[] { EntityMasks.Enemy }))
		|| entity is GoblinRanged || entity is GoblinWarrior;
		bool friendly = entity.Mask.Matches(new EntityMask(new EntityMasks[] { EntityMasks.Friendly }));




		const float offsetX = 25;
		const float offsetY = 30;
		const float duration = 0.15f;
		const float _size = 3f;
		const int rotation = -5	;

		Vector2 offset = Vector2.Zero;
		if (enemy) offset = new Vector2(offsetX, -offsetY);
		else if (friendly) offset = new Vector2(offsetX, -offsetY);

		//GD.Print("offset == ", offset);

		// GD.Print("DIR == ", entity.Direction);
		// GD.Print("POS == ", entity.Parent.GlobalPosition
		//  + offset );

		addText(amount.ToString(), entity.Parent.GlobalPosition + offset
		, duration, size: _size, color: Colors.Red, degrees: rotation);

	}

	public static void addText(string text, Vector2 position, float duration
	, float size = 1, Color color = default, int degrees = default )
	{

		//, bool outline = false

		if (inst == null) return;

		//GD.Print("SPAWN ");

		var label = inst.getText();
		//if (label == null) return;

		label.Item1.Position = position;
		label.Item1.Scale = new Vector2(size, size);
		label.Item1.RotationDegrees = degrees;

		label.Item2.Text = text;
		label.Item2.SelfModulate = color;
		label.Item2.AddThemeConstantOverride("outline_size", 5);

		///GD.Print("H == ", label.Item2.HorizontalAlignment.ToString());
		//GD.Print("V == ", label.Item2.VerticalAlignment.ToString());



		new battleEffectsC.invisibleLater(label.Item1, duration);
		new battleEffectsC.rotateLater(label.Item1, duration/2f , -7);
		new battleEffectsC.resizeLater(label.Item1, duration/2f , 0.8f);


	}







}
