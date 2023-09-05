using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenGUI : MonoBehaviour {
	private string userID = "Enter name.# here";
	private string defaultMessage = "Enter name.# here";
	private string warningMessage = "Try entering your name.# again";
	private string TreatmentGroup;
	private bool Locked = true;

	private Texture2D unLock;
	private Texture2D Lock;
	private Texture2D currentLockPic;

	private Texture2D Record;
	private Texture2D Play;
	private Texture2D currentRecPlay;

	private bool Restart;


	// Use this for initialization
	void Start () {

		unLock = Resources.Load ("unLock") as Texture2D;
		Lock = Resources.Load ("Lock") as Texture2D;
		currentLockPic = Lock;

		Record = Resources.Load ("Rec") as Texture2D;
		Play = Resources.Load ("Play") as Texture2D;
		currentRecPlay = Record;
		PlayerPrefs.SetString ("Record", "true");


		if (!PlayerPrefs.HasKey ("TreatmentGroup") || PlayerPrefs.GetString ("TreatmentGroup") == "") {
			PlayerPrefs.SetString ("TreatmentGroup", "A");
			TreatmentGroup = "A";
		} else {
			TreatmentGroup = PlayerPrefs.GetString ("TreatmentGroup");
		}
		Debug.Log (!PlayerPrefs.HasKey("lastSceneLoaded")||PlayerPrefs.GetInt ("lastSceneLoaded") ==0);

		if (!PlayerPrefs.HasKey("lastSceneLoaded")||PlayerPrefs.GetInt ("lastSceneLoaded") ==0 ) {
			Restart = false;
			PlayerPrefs.SetInt ("lastSceneLoaded", 0);
		} else {
			Restart = true;
			userID = PlayerPrefs.GetString ("ID");
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnGUI(){
		float multiplier=1.06f;
		GUIStyle buttonSameSize = new GUIStyle("button");
		buttonSameSize.fontSize = 80;
		GUIStyle customButton = new GUIStyle("button");
		customButton.fontSize = (int)(2*(Screen.height/25.0f)*multiplier);
		GUIStyle customTextField = new GUIStyle("textField");
		customTextField.fontSize = (int)(2.2*(Screen.height/25.0f)*multiplier);
		customTextField.alignment = TextAnchor.MiddleCenter;


		userID = GUI.TextField (new Rect (Screen.width /8, Screen.height / 2 - (Screen.height/15.0f)*multiplier, (3*Screen.width/4), (2*Screen.height/15.0f)*multiplier), userID,customTextField);

		//var centeredStyle = GUI.skin.GetStyle("Label");
		//centeredStyle.alignment = TextAnchor.UpperCenter;
		//centeredStyle.fontSize = (int)(2*(Screen.height/25.0f)*multiplier);
		//GUI.Label (new Rect (3*Screen.width / 8,  Screen.height/4-2*(Screen.height/15.0f)*multiplier, Screen.width / 4, (Screen.height/15.0f)*multiplier), "Enter ID",centeredStyle);




		//GUI.backgroundColor = Color.green;
		if (GUI.Button (new Rect (3*Screen.width / 8, Screen.height/2+2*(Screen.height/15.0f)*multiplier, Screen.width / 4, 2*(Screen.height / 15.0f) * multiplier),"START",customButton)) {
			if(userID==defaultMessage|| userID ==warningMessage)
			{
				userID = warningMessage;
			}
			else{
				PlayerPrefs.SetString ("ID", userID.ToLower());
				SceneManager.LoadScene (1);}
		}

		if (Restart) { //only show for restart
			GUI.backgroundColor = Color.green;
			if (GUI.Button (new Rect (3 * Screen.width / 8, Screen.height / 2 + 4 * (Screen.height / 15.0f) * multiplier, Screen.width / 4, 2 * (Screen.height / 15.0f) * multiplier), "RESUME", customButton)) {
					PlayerPrefs.SetString ("ID", userID.ToLower ());
					SceneManager.LoadScene (PlayerPrefs.GetInt("lastSceneLoaded"));
			}

		}

		GUI.backgroundColor = Color.clear;
		customButton.fontSize = (int)(5*(Screen.height/25.0f)*multiplier);
		if (GUI.Button (new Rect (Screen.width /4, 0, Screen.width / 2, 5*(Screen.height / 15.0f) * multiplier),"Quiz "+TreatmentGroup, customButton)&&!Locked) {
			if (TreatmentGroup == "A") {
				TreatmentGroup = "B";
				PlayerPrefs.SetString("TreatmentGroup", TreatmentGroup);
			} else {
				TreatmentGroup="A";
				PlayerPrefs.SetString("TreatmentGroup", TreatmentGroup);
			}
		}

		Rect SwitchRect = new Rect (0, 0, (Screen.width / 10.0f), (Screen.width / 10.0f));
		if (GUI.Button (SwitchRect, " ")) {	
			if (currentLockPic == unLock) {
				currentLockPic=Lock;
				Locked = true;
			} else {
				currentLockPic=unLock;
				Locked = false;
				userID = defaultMessage;
				Restart = false;
			}
		}
		GUI.DrawTexture(SwitchRect, currentLockPic,ScaleMode.StretchToFill);

		Rect RecordRec = new Rect (9*Screen.width/10f, 0, (Screen.width / 10.0f), (Screen.width / 10.0f));
		if (GUI.Button (RecordRec, " ")&& !Locked) {	
			if (currentRecPlay == Record) {
				currentRecPlay=Play;
				PlayerPrefs.SetString ("Record", "false");
			} else {
				currentRecPlay=Record;
				PlayerPrefs.SetString ("Record", "true");
			}
		}
		GUI.DrawTexture(RecordRec, currentRecPlay,ScaleMode.StretchToFill);





	}




}
