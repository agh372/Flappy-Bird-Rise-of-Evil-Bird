using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Used to pause or resume the game
/// </summary>
public class PauseManager : MonoBehaviour {

	public Sprite playSprite;
	public Sprite pauseSprite;
	private bool isPaused;

    private static PauseManager instance;

    public static PauseManager GetInstance
    {

        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PauseManager>();

            }

            return instance;
        }
    }

	void Start () {
		isPaused = false;
	}

	public void PauseOrResume(){
		Selectable button = gameObject.GetComponent<Button>();
		isPaused = !isPaused;
		if(isPaused){
			Time.timeScale = 0;
			AudioListener.pause = true;
			button.image.sprite = pauseSprite;

		}else if(!isPaused){
			Time.timeScale = 1;
			AudioListener.pause = false;
		
			button.image.sprite = playSprite;
		}
	}

    public bool IsPausedState()
    {
        return isPaused;
    }
}
