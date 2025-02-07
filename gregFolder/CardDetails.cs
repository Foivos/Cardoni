namespace Cardoni;

using System;
using Godot;

public partial class CardDetails : Node2D
{
	static CardDetails inst;

	[Export]
	RichTextLabel myText;

	public override void _Ready()
	{
		inst = this;
	}

	public static void openDetails(Card card)
	{
		//todo set right or left depending on card index
		bool cardOnRight = card.Index > 1;

		inst.myText.Text = card.Description;
		inst.Visible = true;
	}

	public static void closeDetails()
	{
		inst.Visible = false;
	}



}
