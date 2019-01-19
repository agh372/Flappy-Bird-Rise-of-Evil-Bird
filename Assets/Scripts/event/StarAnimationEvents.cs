using UnityEngine;

/// <summary>
/// Animation events.
/// [put your animation events here]
/// </summary>
public class StarAnimationEvents : MonoBehaviour
{
	public WinStarsManager winStarManagerComp;
	public GameObject exitDialog;
	private static int starNumber = 0;

	// Use this for initialization
	void Start ()
	{
		starNumber = 0;
	}
		
	private void WinDialogEvent ()
	{
		winStarManagerComp.PrepareStars (2);
	}

	private void CameraShakingStart ()
	{
		Camera.main.GetComponent<Animator> ().SetBool ("isRunning", true);
		starNumber++;
	}

	private void CameraShakingEnd ()
	{
		Camera.main.GetComponent<Animator> ().SetBool ("isRunning", false);
	}

}
