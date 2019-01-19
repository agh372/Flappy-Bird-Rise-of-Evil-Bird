using System.Collections;
using UnityEngine;
using System.Threading;

/// <summary>
/// Used to display the stars rewarded after the game
/// </summary>
public class WinStarsManager : MonoBehaviour
{
	public Animator[] starsAnimators;//stars animators references
	public GameController gameComponent;//Game component reference
	public ParticleSystem[] particleSystems;
	public GameObject[] stars;
    public GameObject[] childStars;

	// Use this for initialization
	void Start ()
	{
		GameObject gameOb = GameObject.Find ("Canvas");

		if (starsAnimators [0] == null) {
			starsAnimators [0] = GameObject.Find ("WinDialog/Stars/star1").GetComponent<Animator> ();
			particleSystems [0] = GameObject.Find ("WinDialog/Stars/star1/effect").GetComponent<ParticleSystem> ();
		}
		if (starsAnimators [1] == null) {
			starsAnimators [1] = GameObject.Find ("WinDialog/Stars/star2").GetComponent<Animator> ();
			particleSystems [1] = GameObject.Find ("WinDialog/Stars/star2/effect").GetComponent<ParticleSystem> ();
		}
		if (starsAnimators [2] == null) {
			starsAnimators [2] = GameObject.Find ("WinDialog/Stars/star3").GetComponent<Animator> ();
			particleSystems [2] = GameObject.Find ("WinDialog/Stars/star3/effect").GetComponent<ParticleSystem> ();
 
		}
	
		if (gameComponent == null) {
			//gameComponent = gameOb.GetComponent<GameController> ();
		}
	}


	//Prepare the stars to be animated
	public void PrepareStars (int starsCount)
	{
		StartCoroutine (PreparingStars (starsCount));
	}

	private IEnumerator PreparingStars (int starsCount)
	{
      
		yield return new WaitForSeconds(2.0f);
		float starDelay = 0.7f;//the delay time between stars

		if (starsCount == 1) {//One Star

            starsAnimators[0].enabled = true;
			starsAnimators [0].SetTrigger ("isRunning");
			particleSystems [0].Play ();
		} else if (starsCount == 2) {//Two Stars

            starsAnimators[0].enabled = true;
			starsAnimators [0].SetTrigger ("isRunning");
			particleSystems [0].Play ();
			yield return new WaitForSeconds (starDelay);
            starsAnimators[1].enabled = true;
			starsAnimators [1].SetTrigger ("isRunning");
			particleSystems [1].Play ();
		} else if (starsCount == 3) {//Three Stars

            starsAnimators[0].enabled = true;
			starsAnimators [0].SetTrigger ("isRunning");
			particleSystems [0].Play ();
			yield return new WaitForSeconds (starDelay);
			particleSystems [1].Play ();
            starsAnimators[1].enabled = true;
			starsAnimators [1].SetTrigger ("isRunning");
			yield return new WaitForSeconds (starDelay);
            starsAnimators[2].enabled = true;
			starsAnimators [2].SetTrigger ("isRunning");
			particleSystems [2].Play ();
		}
	}

    public void ResetStars()
    {
        starsAnimators[0].SetBool("isRunning",false);
        starsAnimators[1].SetBool("isRunning", false);
        starsAnimators[2].SetBool("isRunning", false);
        

    }

}