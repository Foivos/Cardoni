using Godot;
using Cardoni;

using System;


[GlobalClass]
public partial class EntityActivePush : EntityActive
{




	[Export]
	public int MovePush { get; set; } = -60;





	public EntityActivePush() { }

	public override void Activate(Entity entity)
	{
	
		entity.Y -= MovePush * entity.FacingDirection;
	SpecialState.pushShadowTrail(entity);//MUST BE AFTER //, MovePush

	}



}



