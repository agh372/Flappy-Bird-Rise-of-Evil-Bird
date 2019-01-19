using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TweenAnimations  {

	/// <summary>
	/// Animation to move down
	/// </summary>
	/// <param name="gameObject">Game object.</param>
	/// <param name="y">The y coordinate.</param>
	/// <param name="time">Time.</param>
	public static void MoveDown(GameObject gameObject,float y,float time){
        
        iTween.MoveBy(gameObject, iTween.Hash(
            "y", y,
            "time", time
        ));
	}
	
}
