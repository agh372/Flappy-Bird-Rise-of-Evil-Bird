using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///Involves all types of bird movement Bird movement.
/// </summary>
public class BirdMovement : MonoBehaviour {

	/// <summary>
	/// Moves the straight line.
	/// </summary>
	/// <param name="rigidBody">Rigid body.</param>
	/// <param name="forceAppliedIntroScene">Force applied intro scene.</param>
    public void MoveStraightLine(Rigidbody2D rigidBody, float forceAppliedIntroScene)
    {
		//print("Intro");
        rigidBody.AddForce(new Vector2(0, forceAppliedIntroScene));
	}
	
	/// <summary>
	/// Jumps the on click.
	/// </summary>
	/// <param name="rigidBody">Rigid body.</param>
	/// <param name="VelocityPerJump">Velocity per jump.</param>
    public void JumpOnClick(Rigidbody2D rigidBody, float VelocityPerJump)
    {
        rigidBody.velocity = new Vector2(0, VelocityPerJump);
        rigidBody.AddForce(new Vector2(0, 100));
	}
}
