using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
///This toggles the turning off and on animation of the lasers following the 
/// </summary>
public class LaserScript : MonoBehaviour {

	public Sprite laserOnSprite;    
	public Sprite laserOffSprite; 
	public float interval = 0.5f;    
	public float rotationSpeed = 30.0f;

	public float velocity = 2.0f;


	public float maxSpeed=60.0f;
	public float acceleration=1.0f;
	//3
	private bool isLaserOn = true;    
	private float timeUntilNextToggle;
	// Use this for initialization
	void Start () {
		timeUntilNextToggle = interval;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//1
		timeUntilNextToggle -= Time.fixedDeltaTime;

		
		if (timeUntilNextToggle <= 0) {

			isLaserOn = !isLaserOn;
			foreach(BoxCollider2D c in GetComponents<BoxCollider2D> ()) {
                c.enabled = true;

			}
			SpriteRenderer spriteRenderer = (SpriteRenderer)GetComponent<Renderer>();
			if (isLaserOn)
				spriteRenderer.sprite = laserOnSprite;
			else
				spriteRenderer.sprite = laserOnSprite;

			//6
			timeUntilNextToggle = interval;
		}

		transform.Translate (velocity*Vector3.left * Time.deltaTime, Space.World);
		transform.Rotate (Vector3.forward, 300.0f * Time.deltaTime);
		
        if (GetComponent<SpriteRenderer>().transform.position.x < Camera.main.transform.position.x - (Camera.main.transform.position.x / 2) - 20)
        {
            DestroyObject(GetComponent<SpriteRenderer>().gameObject);
        }
}
}
