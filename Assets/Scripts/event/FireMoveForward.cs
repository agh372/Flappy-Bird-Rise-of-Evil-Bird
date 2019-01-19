using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Used to shoot fire from the bird
/// </summary>
public class FireMoveForward : MonoBehaviour {

	float maxSpeed = 5f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = transform.position;
		Vector3 velocity = new Vector3 (maxSpeed * Time.deltaTime, 0, 0);
		pos += transform.rotation * velocity;
		transform.position = pos;
		Camera cam = Camera.main;
		float height = 2f * cam.orthographicSize;
		float width = height * cam.aspect;
		if (transform.position.x > Camera.main.transform.position.x + (width/2)) {
			Destroy (gameObject);
		}
	}


	void OnTriggerEnter2D (Collider2D col)
	{
		
		if (col.gameObject.tag == Constants.TAG_LASER) {
			GameController.GetInstance.BirdScored (col.gameObject, 20);
			Destroy (col.gameObject);

		} else if (col.gameObject.tag == "EvilFireBall") {
			Destroy (col.gameObject);

		}

	}
}
