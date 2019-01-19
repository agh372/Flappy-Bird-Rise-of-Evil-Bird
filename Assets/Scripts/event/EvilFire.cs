using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Used by evil bird to shoot fire
/// </summary>
public class EvilFire : MonoBehaviour {

	float maxSpeed = 8f;
	float health = 30.0f;
	// Use this for initialization
	void Start () {
	//	GetComponent<SpriteRenderer>().color = Color.black;
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
		if (transform.position.x < Camera.main.transform.position.x - (width/2)) {
			Destroy (gameObject);
		}
	}


	void OnTriggerEnter2D(Collider2D col)
	{

		if (col.gameObject.tag == "FireBall")
		{
			health -= 5f;
		//	if (health == 0) {
		//		Destroy (col.gameObject);
		//	}

		}
	}

}
