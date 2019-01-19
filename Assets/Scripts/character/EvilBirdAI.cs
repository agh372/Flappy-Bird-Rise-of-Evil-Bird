using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Used to control the evil bird
/// </summary>
public class EvilBirdAI : MonoBehaviour {

	// Use this for initialization
	public Transform player;
	public int speed;
	float health = 30.0f;
	public GameObject fireBall;

	void Start () {
		InvokeRepeating ("SpawnFire", 0.5f,2f);
	}

	void Update () {
		Vector3 lookAt = player.position;

		Vector3 followXonly = new Vector3(
			transform.position.x,
			player.position.y,
			transform.position.z);

		transform.position = Vector3.MoveTowards(
			transform.position,
			followXonly,
			speed * Time.deltaTime);
	}

	public void SpawnFire(){
		Instantiate (fireBall,transform.position,Quaternion.Euler(0f,180f,0f));
		Instantiate (fireBall,transform.position,Quaternion.Euler(0f,180f,5f));
		Instantiate (fireBall,transform.position,Quaternion.Euler(0f,180f,-5f));
	}


	void OnTriggerEnter2D(Collider2D col)
	{

		if (col.gameObject.tag == "FireBall")
		{
				health -= 3f;
				if (health == 0) {
					Destroy (gameObject);
				}

		}
	}



}
