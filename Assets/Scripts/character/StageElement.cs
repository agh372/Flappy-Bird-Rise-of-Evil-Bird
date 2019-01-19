using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Stage element.
/// </summary>
public class StageElement : MonoBehaviour
{
		public bool isLocked;//is locked
		public int id;//id
		public int starLevel; //from 0 to 3
		public Sprite[] starLevelIcons;//star level icons
		public Sprite[] backgroundIcons;//background icons
       
		//set stars level
		public void SetStarsLevel (int level, SpriteRenderer sp)
		{
				if (!(level >= 0 && level < starLevelIcons.Length)) {
						return;
				}

				sp.sprite = starLevelIcons [level];
		}

	void Start(){

		GameController.GetInstance.WinEvent += OnPlayerWin;
	}


	void Update(){
		Vector2 contactPoint = Vector2.zero;
		if (Input.touchCount > 0)
			contactPoint = Input.touches[0].position;
		if (Input.GetMouseButtonDown(0))
			contactPoint = Input.mousePosition;
	}

		//set background icon
		public void SetBackgoundIcon (int index, SpriteRenderer sp)
		{
				if (!(index >= 0 && index < backgroundIcons.Length)) {
						return;
				}
				sp.sprite = backgroundIcons [index];
		}
		

	public void OnClicked(Button button)
	{
		print(button.tag);
	}
		
	private void OnPlayerWin(){
		
		if (GetComponent<StageElement> ().id == GameController.GetInstance.currentLevelId) {
			
			GameObject child = getChildGameObject (this.gameObject, "Content");
			GameObject star =  getChildGameObject(child, "Stars");
			star.GetComponent<SpriteRenderer> ().sprite = starLevelIcons [3];
		}

	}
		
	public GameObject getChildGameObject(GameObject fromGameObject, string withName) {
		Transform[] ts = fromGameObject.transform.GetComponentsInChildren<Transform>();
		foreach (Transform t in ts) if (t.gameObject.name == withName) return t.gameObject;
		return null;
	}
}