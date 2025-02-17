namespace Cardoni;

using System;
using Godot;

public partial class EntityParent : Area2D
{
	[Export]
	public TextureProgressBar HealthBar;

	[Export]
	public Sprite2D Sprite;

	[Export] public Sprite2D Weapon;
	//[Export] public enemyMove EnemyMove;



	public void setShordPosition(Entity entity)
	{
		if (Weapon == null) return;

		bool lookingDown = entity.FacingDirection == 1;

		// if (enytity.Direction != 0) lookingDown = enytity.Direction == 1;
		// else if (enytity is AttackingEntity atacker && atacker.Target != null)
		// {
		// 	lookingDown = atacker.Target.Parent.Sprite.GlobalPosition.Y
		// > enytity.Parent.Sprite.GlobalPosition.Y;
		// }
		// else GD.PushError("NEED BETTER WAY OF KNOWING DIRECTION");



		if (lookingDown) Weapon.RotationDegrees = -135;
		else Weapon.RotationDegrees = -45;

		Vector2 shordPosition = new Vector2(23, 23);
		if (!lookingDown) shordPosition.Y *= -1;
		Weapon.Position = shordPosition;
	}




	public void processSideMove()
	{

		if (moveThen > Time.GetTicksMsec()) return;
		moveThen = Time.GetTicksMsec() + moveCooldown * 1000;

		//GD.Print("processSideMove");

		if (side != 1) side = 1;
		else side = -1;


		Sprite.Offset = new Vector2(moveDistance, 0) * side;
		RotationDegrees = moveRotation * side;

		Weapon.Offset = new Vector2(weaponMove, 0) * side;


	}

	int side = 1;
	float weaponMove = 0.5f;
	float moveDistance = 0.3f;
	float moveRotation = 1;
	float moveCooldown = 0.3f;
	double moveThen;




}
