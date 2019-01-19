using UnityEngine;
using System.Collections;

public class SpawnerScript : MonoBehaviour
{


	[SerializeField]
	private GameObject prefabToSpawn;


	public float spawnInterval, objectMinX, objectMaxX, objectY;

	[SerializeField]
	private GameObject[] objectSprites;

	public string c;

	public bool visited = false;

    private int index = 0;




	// Use this for initialization
	void Start () {
		GameController.GetInstance.WinEvent += OnPlayerRetry;
		InvokeRepeating ("spawnObject", 0.5f,3f);
	}



	private void spawnObject() {
		
		if(MultiStateBehaviour.mState == MultiStateBehaviour.State.Playing){
		float y = Random.Range(objectMinX, objectMaxX);
        index++;

		GameObject newObject = Instantiate (this.prefabToSpawn, this.transform.position + new Vector3(30,y,-30),Quaternion.identity);
        
        newObject.tag = tag;
        if (tag.Equals(Constants.TAG_LASER))
        {
            newObject.GetComponent<LaserScript>().velocity = newObject.GetComponent<LaserScript>().velocity + 0.2f*index;

            print(newObject.GetComponent<LaserScript>().velocity);
        }
		
		}
	}


	private void OnPlayerRetry(){
		
	//	InvokeRepeating ("spawnObject", 0.5f,3f);
	}
}
