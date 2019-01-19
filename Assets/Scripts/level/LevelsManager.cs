using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameController.GetInstance.NewGameEvent += OnNewGameOrMenu;
        GameController.GetInstance.LevelClickEvent += OnNewLevelMenu;
	}

	/// <summary>
	/// Raises this event after new game is clicked.
	/// </summary>
    public void OnNewGameOrMenu()
    {
        GetComponent<Animator>().SetBool("SlideStage", true);

    }

	/// <summary>
	/// Moves the stage selection back to default
	/// </summary>
    public void OnNewLevelMenu()
    {
        GetComponent<Animator>().SetBool("SlideStage", false);
    }


}
