using UnityEngine; using System.Collections;


/// <summary>
/// Button pushed during level stages
/// </summary>
public class ButtonPushed : MonoBehaviour {

	public GameObject SendMessageTo;
	public string CallMethod;
	// Use this for initialization
	void Start () {
        
	}

	// Update is called once per frame
	void Update () {

	}
	public void Pushed()
	{
		
		SendMessageTo.SendMessage(CallMethod, gameObject.name);
	}


    void Message(string name)
    {

        print(name);
        GameController.GetInstance.OnLevelClick(name);
    }

    void OnMouseDown()
    {
        
        transform.localScale -= new Vector3(0.05f, 0.05f, 0f);
        //or
        transform.GetComponent<SpriteRenderer>().color += new Color(40, 40, 40);
    }

    void OnMouseUp()
    {
        transform.localScale += new Vector3(0.05f, 0.05f, 0f);
        //or
        transform.GetComponent<SpriteRenderer>().color -= new Color(40, 40, 40);

        
    }
} 