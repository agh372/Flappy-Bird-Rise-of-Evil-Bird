using UnityEngine;
using System.Collections;
using System.Threading;

/// <summary>
///Main Menu manager.
/// </summary>
public class MenuManager : MonoBehaviour 
{

    public GameObject menu; 

    public void GoTo(int scene)
    {
        TweenAnimations.MoveDown(menu,20.0f,6.0f);
		MultiStateBehaviour.NewGameClicked();
    }



    public void Quit()
    {
        Application.Quit();
    }


    public void ToggleVisible(Animator anim)
    {
        if (anim.GetBool("isDisplayed"))
        {
            anim.SetBool("isDisplayed", false);
        }
        else
        {
            anim.SetBool("isDisplayed", true);
        }
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
