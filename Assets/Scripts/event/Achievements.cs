using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Achievements : MonoBehaviour {


	void Awake () {
		///registering to an event
		GameController.GetInstance.WinEvent += OnPlayerWin;
        GameController.GetInstance.deathEvent += OnPlayerDead;
        GameController.GetInstance.OnGameOverRetry += OnGameOverRetry;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //gets invoked when the death ofthe bird occurs
	public void OnPlayerWin()
    {
		DisplayScores ();
    }


    public void OnPlayerDead()
    {
        if(GetComponent<Achievements>().tag == "DeathGUI")
        TweenAnimations.MoveDown(GetComponent<Achievements>().gameObject,-20,3f);
    }


    public void OnGameOverRetry()
    {
        if (GetComponent<Achievements>().tag == "DeathGUI")
            TweenAnimations.MoveDown(GetComponent<Achievements>().gameObject, 20, 3f);

    }


	private void DisplayScores(){
		if (!(GetComponent<Achievements> ().tag == "DeathGUI")) {
			GetComponentsInChildren<Text> () [0].text = Constants.SCORE + ScoreManagerScript.Score;
			ScoreManagerScript.SetHighScore (ScoreManagerScript.Score);
			GetComponentsInChildren<Text> () [1].text = "High Score:  " + PlayerPrefs.GetInt ("highscore");
		}
		GameController.GetInstance.WinEvent -= OnPlayerWin;
	}
}
