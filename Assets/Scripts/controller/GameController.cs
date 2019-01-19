using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;


public class GameController: MonoBehaviour {


 	public delegate void EventsDelegate();
	public event EventsDelegate deathEvent;
	public event EventsDelegate WinEvent;
	public event EventsDelegate RetryEvent;
	public event EventsDelegate NewGameEvent;
	public event EventsDelegate LevelClickEvent;
	public event EventsDelegate OnGameOverRetry;
	public event EventsDelegate OnContinueGameEvent;

    public GameObject mBirdObj;
    public GameObject mTreesObj;
    public GameObject mFloorObj;
    public GameObject mMountainsObj;
    public GameObject mLaserObj;
    public GameObject mCoinObj;
	public GameObject mEvilBirdObj;

	public GameObject dialogsComponent;
	public GameObject StagesDialog;


	public GameObject coinSpawner;
	public GameObject laserSpawner;
    
	private static GameController instance;
	public AudioClip FlyAudioClip, DeathAudioClip, ScoredAudioClip,ApplauseClip;
	public float mVelocityPerJump = 3;
	public bool isGameOver = false;

    public int currentLevelId = 1;


 public enum GameState {
  Intro,
  Playing,
  Dead
 }


 public static GameController GetInstance {

  get {
   if (instance == null) {
    instance = FindObjectOfType < GameController > ();
   }
   return instance;
  }
 }

 // Use this for initialization
 void Start() {

 }

 void Update() {


 }

 public void BirdDied() {
		
  mBirdObj.GetComponent < Animator > ().enabled = false;
  ResetSpeed (mTreesObj, 0, 0);
  ResetSpeed (mMountainsObj, 0, 0);
  ResetSpeed (mFloorObj, 0, 0);
  ResetSpeed (mCoinObj, 10, 0);
  isGameOver = true;
  DestroyAllObjects(Constants.TAG_LASER);
  DestroyAllObjects(Constants.TAG_COIN);
  GetComponent<AudioManager>().PlayOneShot(DeathAudioClip);

		if (ScoreManagerScript.SetHighScore (ScoreManagerScript.Score)) {
			GetComponent<AudioManager>().PlayOneShot(ApplauseClip);
		}
  
     //Invoke the method from Achievements.cs to display the high score and other achievements
  if (deathEvent != null) {
   deathEvent();

  }
dialogsComponent.SetActive (true);
 }

	/// <summary>
	/// On Bird Winning
	/// </summary>
	public void BirdWon(){
		if (WinEvent != null) {
			WinEvent();
		}

		TweenAnimations.MoveDown (dialogsComponent,-20,3f);
		GetComponent<WinStarsManager>().PrepareStars (3);
        mBirdObj.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
	}



	/// <summary>
	/// Retries the game.
	/// </summary>
	public void RetryGame(){
		if (RetryEvent != null) {
			RetryEvent();
		}
	
		SpawnLasersAndCoins ();
		ScoreManagerScript.Score = 0;
		TweenAnimations.MoveDown (dialogsComponent,20,3f);
        mBirdObj.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        GetComponent<WinStarsManager>().ResetStars();

	}

	/// <summary>
	/// Brings the level selection menu from results.
	/// </summary>
    public void BringLevelSelectionMenuFromResults()
    {
		
        if (LevelClickEvent != null)
        {
          //  LevelClickEvent();
        }
        ScoreManagerScript.Score = 0;
        TweenAnimations.MoveDown(dialogsComponent, 20, 3f);
        mBirdObj.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        GetComponent<WinStarsManager>().ResetStars();
        ShowStages();
       
    }

	/// <summary>
	/// On state of the Bird in intro.
	/// </summary>
	/// <param name="rigidBody">Rigid body.</param>
	/// <param name="forceAppliedIntroScene">Force applied intro scene.</param>
 public void BirdIntroState(Rigidbody2D rigidBody, float forceAppliedIntroScene)
 {
     if (rigidBody != null)
     {
         GetComponent<BirdMovement>().MoveStraightLine(rigidBody, forceAppliedIntroScene);
     }

 }


	/// <summary>
	/// Called when bird wants to fly
	/// </summary>
	/// <param name="rigidBody">Rigid body.</param>
 public void BirdFly(Rigidbody2D rigidBody) {
     
  GetComponent<AudioManager>().PlayOneShot(FlyAudioClip);
  if (rigidBody != null)
  {
      GetComponent<BirdMovement>().JumpOnClick(rigidBody, mVelocityPerJump);
  }

 }

	public void ShowStages(){
		if (NewGameEvent != null) {
			NewGameEvent();
		}
	}

	/// <summary>
	/// On Clicking Retry after game over
	/// </summary>
    public void GameOverRety(){
        if (OnGameOverRetry != null)
        {
            OnGameOverRetry();
        }
        if (RetryEvent != null)
        {
            RetryEvent();
        }
		SpawnLasersAndCoins ();
        ScoreManagerScript.Score = 0;
        mBirdObj.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        GetComponent<WinStarsManager>().ResetStars();
		LevelModel levelData = JsonRetrieval.ReadFile (currentLevelId);
		mFloorObj.GetComponent < ScrollingScript > ().speed = new Vector2((float)levelData.FloorSpeed, 0);
		mMountainsObj.GetComponent < ScrollingScript > ().speed = new Vector2((float)levelData.SkySpeed, 0);
		mTreesObj.GetComponent < ScrollingScript > ().speed = new Vector2((float)levelData.TreeSpeed, 0);
		Vector3 temp = new Vector3 (-5.13f, 0.2581109f, 4.669603f);
		mBirdObj.transform.position = temp;
    }


	/// <summary>
	/// When Bird scores
	/// </summary>
	/// <param name="obj">Object.</param>
	/// <param name="points">Points.</param>
	public void BirdScored(GameObject obj,int points) {
	  GetComponent<AudioManager>().PlayOneShot(ScoredAudioClip);
		ScoreManagerScript.incrementScore(points);
  		DestroyObject(obj);
 }

	/// <summary>
	/// Raises the continue after game window event.
	/// </summary>
	public void OnContinueAfterGameWin(){
		if (OnContinueGameEvent != null)
		{
			OnContinueGameEvent();
		}
		currentLevelId++;
		UpdateObjects ();
		TweenAnimations.MoveDown (dialogsComponent,20,3f);
		GetComponent<WinStarsManager>().ResetStars();
		SpawnLasersAndCoins ();

		LevelModel gameData =  JsonRetrieval.ReadFile (currentLevelId);
		if (gameData.IsBossEnabled) {
			mEvilBirdObj.SetActive (true);

		}
		UpdateObjects ();
	}



	private void UpdateObjects(){

		LevelModel levelData = JsonRetrieval.ReadFile (currentLevelId);
		mFloorObj.GetComponent < ScrollingScript > ().speed = new Vector2((float)levelData.FloorSpeed, 0);
		mMountainsObj.GetComponent < ScrollingScript > ().speed = new Vector2((float)levelData.SkySpeed, 0);
		mTreesObj.GetComponent < ScrollingScript > ().speed = new Vector2((float)levelData.TreeSpeed, 0);
		Vector3 temp = new Vector3 (-5.13f, 0.2581109f, 4.669603f);
		mBirdObj.transform.position = temp;
	}



 public void RestartGame() {
  UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
		ScoreManagerScript.Score = 0;
 }

    /// <summary>
    /// Returns the current score
    /// </summary>
    /// <returns></returns>
    public int GetScore(){
        return ScoreManagerScript.Score;
    }


    /// <summary>
    /// Destroys all objects with particular tag
    /// </summary>
    /// <param name="tag"></param>
	public void DestroyAllObjects(string tag) {
  GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(tag);
  for (var i = 0; i < gameObjects.Length; i++) {
   Destroy(gameObjects[i]);
  }
 }

	void ResetSpeed(GameObject gameObject,float x,float y){
		gameObject.GetComponent < ScrollingScript > ().speed = new Vector2(x, y);
	}


    public void OnLevelClick(string level)
    {
		if (LevelClickEvent != null)
		{
			  LevelClickEvent();
		}
		LevelModel gameData =  JsonRetrieval.ReadFile (getLevelIdFromString(level));
		SpawnLasersAndCoins ();
		if (gameData.IsBossEnabled) {
			mEvilBirdObj.SetActive (true);
		}
		mCoinObj.GetComponent<ScrollingScript> ().speed = new Vector2(gameData.CoinVelocity,0);
		mLaserObj.GetComponent<LaserScript> ().velocity = gameData.LaserVelocity;
        StagesDialog.GetComponent<Animator>().enabled = false;
        if (LevelClickEvent != null)
        {
            LevelClickEvent();
        }
        TweenAnimations.MoveDown(StagesDialog, 20.0f, 3f);
        StagesDialog.GetComponent<Animator>().enabled = true;
    }


	private int getLevelIdFromString(string level){
		if (level.Equals ("FirstLevel")) {
			return 1;
		}else if(level.Equals ("SecondLevel")){
			return 2;
		}else if(level.Equals ("ThirdLevel")){
			return 3;
		}
		return 3;
	}


	private void SpawnLasersAndCoins(){
		GameObject clone = Instantiate(coinSpawner, new Vector3(5.76f, -2.52f, 35.61f), Quaternion.identity) as GameObject;
		GameObject laserClone = Instantiate(laserSpawner, new Vector3(5.00f, 3.64f, 34.10f), Quaternion.identity) as GameObject;

	}
}