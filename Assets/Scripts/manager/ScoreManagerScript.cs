using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManagerScript : MonoBehaviour {

	public static int Score = 0;

	private int highScore;

	private string previousScore = "";

	// Use this for initialization
	void Start () {
		GetComponent<Text> ().text = "Score: " + ScoreManagerScript.Score;
	}

	public static void incrementScore(int incrementValue) {
		Score += incrementValue;

	}

	void Update(){
		if (!previousScore.Equals (Score)) {
			GetComponent<Text> ().text = "Score: " + ScoreManagerScript.Score;    
			previousScore = GetComponent<Text> ().text;
		}
	}


	public int getScore() {
		return ScoreManagerScript.Score;
	}


	public static bool SetHighScore(int highScore){
		
		if (highScore >= PlayerPrefs.GetInt("highscore")) {
			PlayerPrefs.SetInt ("highscore", highScore);
            return true;
		}
        return false;
	}
		
   
}
