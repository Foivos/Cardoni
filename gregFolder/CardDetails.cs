namespace Cardoni;

using Godot;
using System;

public partial class CardDetails : Node2D
{


	static CardDetails inst;


	[Export] RichTextLabel myText;



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
