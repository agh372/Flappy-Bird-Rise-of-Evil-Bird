using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	// Use this for initialization
	public void PlayOneShot (AudioClip clip) {
       GetComponent<AudioSource>().PlayOneShot(clip);
	}
	
}
