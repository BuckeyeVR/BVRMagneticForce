using UnityEngine;
using System.Collections;

public class Intro : MonoBehaviour {

	[SerializeField]
	public Transform DispControllerPrefab;
	[SerializeField]
	public Texture Logo;
	[SerializeField]
	private bool IsUsingDualDisplay;
	[SerializeField]
	private bool IsWEB;
	public float LengthOfLogo;
	private Texture currentTexture;
	private bool isSwitching = false;
	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (gameObject);
		currentTexture = Logo;
		StartCoroutine (switchLevel (LengthOfLogo));
	}

	void Awake() {
		Application.targetFrameRate = 60;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0) && IsWEB==false) {
			IsUsingDualDisplay = !IsUsingDualDisplay;
		}
	}

	void OnGUI(){
		if (isSwitching == false) {
			if (IsUsingDualDisplay == false) {
				GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), currentTexture);
			} else {
				GUI.DrawTexture (new Rect (0, 0, Screen.width / 2, Screen.height), currentTexture);
				GUI.DrawTexture (new Rect (Screen.width / 2, 0, (Screen.width/2), Screen.height), currentTexture);
			}
		}
	}


	public bool getIsUsingDualDisplay(){
		return IsUsingDualDisplay;
	}

	public bool getIsWeb(){
		return IsWEB;
	}

	//time to pass before switching to text in seconds.
	IEnumerator switchLevel(float timeToPass){
		yield return new WaitForSeconds(timeToPass);
		isSwitching = true;
		Instantiate (DispControllerPrefab, Vector3.zero, Quaternion.identity);
		GameObject dispC = GameObject.Find ("DisplayController(Clone)");
		dispC.transform.name = "DisplayController";
		DontDestroyOnLoad (dispC);
		Application.LoadLevel (1);
	}
}
