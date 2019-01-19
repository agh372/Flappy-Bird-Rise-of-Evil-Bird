using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderDistanceProgress : MonoBehaviour {

	// Use this for initialization

	public Slider slider;
	public float speed = 0.002f;
	float pos = 0;
	public GameObject dialogsComponent;

    public int max;


	private bool visited = false;
	void Awake() {
		GameController.GetInstance.RetryEvent += OnPlayerRetry;
        GameController.GetInstance.LevelClickEvent += OnPlayerRetry;
		GameController.GetInstance.OnContinueGameEvent += OnPlayerRetry;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (MultiStateBehaviour.IsBirdPlayingState ()) {
			pos += speed * Time.deltaTime;
			slider.value = pos;


			if (pos > max && !visited) {
				
				GameController.GetInstance.BirdWon ();
				visited = true;
			}
		}
}


	void OnPlayerRetry(){
		
		slider.value = 0;
		pos = 0;
		visited = false;

	}
}
