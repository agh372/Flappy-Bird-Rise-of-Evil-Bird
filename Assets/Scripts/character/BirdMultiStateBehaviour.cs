using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
///Bird has different behaviours for different states,{Intro, Playing, Dead} 
///To avoid using complicated switching logic in Update loop, 
///I have used using Coroutines. That way I can keep the related behaviour in one class/instance, 
///rather than juggling a collection of them every time you want a different state.
///A coroutine with a yield return null loop executes once each frame very similar to the Update message, 
///and by switching between multiple coroutines I can get the desired state change behaviour.
/// </summary>

public class MultiStateBehaviour : MonoBehaviour
{
    [System.Serializable]
    public enum State
    {
        Intro,
        Playing,
		Win,
        Dead
    }

    public float RotateUpSpeed = 1, RotateDownSpeed = 1;
    Vector3 birdRotation = Vector3.zero;
    public Collider2D[] restartButtonGameCollider;
    public Collider2D newGameButtonGameCollider;
    public Collider2D levelSelectionButton;
	public Collider2D continueButton;
	public Collider2D backToMenu;



	public GameObject fireBall;


    private static bool IsNewGameClicked = false;
    Coroutine _activeState;

    public static State mState;

    private Rigidbody2D rigidBody;
    private float forceAppliedIntroScene;

    void Start()
    {
        GameController.GetInstance.RetryEvent += OnPlayerRetry;
		GameController.GetInstance.WinEvent += OnPlayerWin;
        GameController.GetInstance.LevelClickEvent += OnNewLevelMenu;
        rigidBody = GetComponent<Rigidbody2D>();
        ChangeState(State.Intro);
        if(rigidBody!=null)
        forceAppliedIntroScene = rigidBody.mass * 6000 * 0.02f;
    }


    FlappyYAxisTravelState flappyYAxisTravelState;

    enum FlappyYAxisTravelState
    {
        GoingUp,
        GoingDown
    }

    public  void ChangeState(State destinationState)
    {

        // Optionally, you can check if you're already in destinationState
        // and early-out rather than restarting the state.
        mState = destinationState;
        IEnumerator state = null;

       
        switch (destinationState)
        {
            case State.Intro:
                state = IntroState();
                break;
            case State.Playing:
        
                state = PlayState();
                break;
            case State.Dead:
                state = DeadState();
                break;
		case State.Win:
			state = WinState();
			break;
        }
        
        // Abort the currently-running state.
        if (_activeState != null)
        {
          
            StopCoroutine(_activeState);
        }
       
        _activeState =  StartCoroutine(state);
    }

    /// <summary>
    /// Represents the state of bird in Intro state
    /// </summary>
    /// <returns></returns>
    IEnumerator IntroState()
    {
         
        while (true)
        {
            if (IsNewGameClicked && mState == State.Intro)
            {
			
				GameController.GetInstance.ShowStages ();
            }
            yield
            return null;
        }
    }
    /// <summary>
    ///  Represents the state of bird in Play state
    /// </summary>
    /// <returns></returns>
    IEnumerator PlayState()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;    
        IsNewGameClicked = false;
       
        while (true)
        {
            
            if (WasTouchedOrClicked()&&IsBirdPlayingState())
            {
                BoostOnYAxis();
            }


            yield
            return null;
        }
    }

    /// <summary>
    /// Represents the state of bird in Dead state
    /// </summary>
    /// <returns></returns>
    IEnumerator DeadState()
    {
        
        while (true)
        {

            Vector2 contactPoint = Vector2.zero;

            if (Input.touchCount > 0)
                contactPoint = Input.touches[0].position;
            if (Input.GetMouseButtonDown(0))
                contactPoint = Input.mousePosition;

            //  If restart button is clicked after game over
			if (restartButtonGameCollider [0] == Physics2D.OverlapPoint (Camera.main.ScreenToWorldPoint (contactPoint))) {
				ChangeState (State.Playing);
				BoostOnYAxis ();
				GameController.GetInstance.RetryGame ();
			} else if (restartButtonGameCollider [1] == Physics2D.OverlapPoint (Camera.main.ScreenToWorldPoint (contactPoint))) {

				ChangeState (State.Playing);
				BoostOnYAxis ();
				GameController.GetInstance.GameOverRety ();
			} else if (backToMenu == Physics2D.OverlapPoint (Camera.main.ScreenToWorldPoint (contactPoint))) {
				GameController.GetInstance.RestartGame ();
			}
            yield
            return null;
        }
    }

	/// <summary>
	///Represents the state of bird in Win state
	/// </summary>
	/// <returns>The state.</returns>
	IEnumerator WinState()
	{
       
		while (true)
		{
			Vector2 contactPoint = Vector2.zero;

			if (Input.touchCount > 0)
				contactPoint = Input.touches[0].position;
			if (Input.GetMouseButtonDown(0))
				contactPoint = Input.mousePosition;
     
			//  If restart button is clicked after game over
			if (restartButtonGameCollider[0] == Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(contactPoint)) )
			{
                
				ChangeState(State.Playing);
				BoostOnYAxis();
				GameController.GetInstance.RetryGame ();
            }
            else if (levelSelectionButton == Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(contactPoint)))
            {
				ChangeState(State.Intro);
                GameController.GetInstance.BringLevelSelectionMenuFromResults();
			}else if(continueButton == Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(contactPoint) )){
				ChangeState (State.Playing);
				GameController.GetInstance.OnContinueAfterGameWin ();
			}

			yield
			return null;
		}
	}
	/// <summary>
	/// Raises the player window event.
	/// </summary>
	public void OnPlayerWin(){
		GameController.GetInstance.DestroyAllObjects(Constants.TAG_LASER);
		GameController.GetInstance.DestroyAllObjects(Constants.TAG_COIN);
		ChangeState (State.Win);
	}



    void FixedUpdate()
    {

        if (mState == State.Intro)
        {

            if (rigidBody.velocity.y < -1)
            {
                GameController.GetInstance.BirdIntroState(rigidBody, forceAppliedIntroScene);
              //  rigidBody.AddForce(new Vector2(0, forceAppliedIntroScene)); //Adds force upwards to neutralize the gravitational force
            }
        }
        else if (mState == State.Playing || mState == State.Dead)
        {
            FixFlappyRotation(); //To rotate the flappy bird
        }
    }


    /// <summary>
    /// Method to check if screen is touched(phone) or clicked(PC)
    /// </summary>
    /// <returns></returns>
    bool WasTouchedOrClicked()
    {
        if (Input.GetButtonUp("Jump") || Input.GetMouseButtonDown(0) ||
         (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended))
        {

            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Boosts the bird on y axis won to avoid obstacles
    /// </summary>
    void BoostOnYAxis()
    {
       
		if (mState != State.Dead && !PauseManager.GetInstance.IsPausedState())
        {
            GameController.GetInstance.BirdFly(rigidBody);
        }

    }


    /// <summary>
    /// when the flappy goes up, it'll rotate up to 45 degrees. when it falls, rotation will be -90 degrees min
    /// </summary>
    private void FixFlappyRotation()
    {
        if (rigidBody.velocity.y > 0) flappyYAxisTravelState = FlappyYAxisTravelState.GoingUp;
        else flappyYAxisTravelState = FlappyYAxisTravelState.GoingDown;

        float degreesToAdd = 0;

        switch (flappyYAxisTravelState)
        {
            case FlappyYAxisTravelState.GoingUp:
                degreesToAdd = 6 * RotateUpSpeed;

                break;
            case FlappyYAxisTravelState.GoingDown:
                degreesToAdd = -3 * RotateDownSpeed;
                break;
            default:
                break;
        }

        birdRotation = new Vector3(0, 0, Mathf.Clamp(birdRotation.z + degreesToAdd, -90, 45));
        transform.eulerAngles = birdRotation;

    }



    /// <summary>
    /// checks for collision with Coins or Lasers
    /// </summary>
    /// <param name="col"></param>
    void OnTriggerEnter2D(Collider2D col)
    {
        if (mState == State.Playing)
        {
			if (col.gameObject.tag == Constants.TAG_COIN) {

				GameController.GetInstance.BirdScored (col.gameObject, 50);
			} else if (col.gameObject.tag == Constants.TAG_LASER) {
				
				ChangeState (State.Dead);
				GameController.GetInstance.BirdDied ();
			} 
        }
    }

    /// <summary>
    /// checks for collision with Floor
    /// </summary>
    /// <param name="col"></param>
    void OnCollisionEnter2D(Collision2D col)
    {
        if ((mState == State.Playing))
        {
            if (col.gameObject.tag == "Floor")
            {
               ChangeState(State.Dead);
              GameController.GetInstance.BirdDied();
            }
        }
    }

    /// <summary>
    /// flag to check if New Game is clicked
    /// </summary>
    public static void NewGameClicked()
    {
        IsNewGameClicked = true;
    }


	/// <summary>
	/// Raises the player retry event.
	/// </summary>
    public void OnPlayerRetry()
    {
        ChangeState(State.Playing);


    }

	/// <summary>
	/// Fires the ball.
	/// </summary>
	public void FireBall(){
			Instantiate (fireBall,transform.position,Quaternion.identity);
	}

	/// <summary>
	/// Raises the new level menu event.
	/// </summary>
    public void OnNewLevelMenu()
    {
        ChangeState(State.Playing);
        BoostOnYAxis();

    }

	/// <summary>
	/// Determines if is bird playing state.
	/// </summary>
	/// <returns><c>true</c> if is bird playing state; otherwise, <c>false</c>.</returns>
	public static bool IsBirdPlayingState(){
		return MultiStateBehaviour.mState == MultiStateBehaviour.State.Playing;
	}

}