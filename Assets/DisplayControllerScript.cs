using UnityEngine;
using System.Collections;

public class DisplayControllerScript : MonoBehaviour {

	[SerializeField]
	private bool IsUsingWEB;
	private bool IsUsingDualDisplay;
	private float multiplier=1.06f;
	Transform [] currentActiveCamera;
	Transform [] charges;
	Transform currentCam; //current activeCam
	Transform dualCam; //vr view cam
	Transform dualCamSecondary; // controlsVR Settings
	Transform dualCamSelector; //level selector for dualcam
	Transform singleCamselector; //level selector for singlecam
	Transform singleCam; //non vr view cam
	private bool justLoaded = true;
	public string Message = "";
	public string headsUpMessage="";
	// Use this for initialization
	void Start () {
		Debug.Log ("I started_DispContr");
		//DontDestroyOnLoad (gameObject);
		if (GameObject.Find ("Main Camera") != null) {
			singleCam = GameObject.Find ("Main Camera").transform;
		}
		if (GameObject.Find ("Selector_2cam") != null) {
			dualCamSelector = GameObject.Find ("Selector_2cam").transform;
		}
		if (GameObject.Find ("Selector") != null) {
			singleCamselector = GameObject.Find ("Selector").transform;
		}
		if (GameObject.Find ("CardboardHead") != null) {
			dualCamSecondary = GameObject.Find ("Cardboard").transform;
			dualCam = GameObject.Find ("CardboardHead").transform.GetChild (0).transform; //we want to keep updating the position when the camera is disabled
		}
	}

	// Update is called once per frame
	void LateUpdate () {
		if (Input.GetMouseButtonDown (0) && IsUsingWEB==false) {
			//switchCam();
		}
		if (IsUsingWEB == true) {
			if (GameObject.Find ("CardboardHead")) {
				Destroy (GameObject.Find ("CardboardHead"));
				if (singleCam.GetComponent<CardboardHead> ()) {
					singleCam.GetComponent<CardboardHead> ().enabled = false;
				}
				singleCam.GetComponent<MouseOrbitImproved> ().enabled = true;
			}
			if (GameObject.Find ("Cardboard")) {
				if (singleCam.GetComponent<CardboardHead> ()) {
					singleCam.GetComponent<CardboardHead> ().enabled = false;
				}
				singleCam.GetComponent<MouseOrbitImproved> ().enabled = true;
				Destroy (GameObject.Find ("Cardboard"));
			}
			if (singleCam.GetComponent<MouseOrbitImproved> ().isActiveAndEnabled == false) {
				singleCam.GetComponent<MouseOrbitImproved> ().enabled = true;
			}
		} else {
			if (GameObject.Find ("GameObject"))
				Destroy (GameObject.Find ("GameObject"));
			}
	}

	//void OnLevelWasLoaded(int level) {
	void onValidate(){
		singleCam = GameObject.Find ("Main Camera").transform;
		if (GameObject.Find ("CardboardHead") != null) {
			dualCamSecondary = GameObject.Find ("Cardboard").transform;
			dualCam = GameObject.Find ("CardboardHead").transform.GetChild (0).transform; //we want to keep updating the position when the camera is disabled
			if(IsUsingWEB == true){
				Destroy(GameObject.Find ("Cardboard"));
				Destroy(GameObject.Find ("CardboardHead"));
			}
		}
		if(IsUsingWEB == true){
			if(singleCam.GetComponent<CardboardHead>()){
				singleCam.GetComponent<CardboardHead>().enabled=false;
			}
			singleCam.GetComponent<MouseOrbitImproved>().enabled=true;
		}
		if (justLoaded == true) {
			justLoaded = false;
			if(GameObject.Find ("IntroLogo_Settings")){
				IsUsingDualDisplay = GameObject.Find ("IntroLogo_Settings").GetComponent<Intro> ().getIsUsingDualDisplay ();
				IsUsingWEB = GameObject.Find ("IntroLogo_Settings").GetComponent<Intro> ().getIsWeb();
				Destroy (GameObject.Find ("IntroLogo_Settings"));
			}
		}
		if (IsUsingWEB == false) {
			switchCam ();
			switchCam ();
		}
	}

	public bool getDisplayMode(){
		return IsUsingDualDisplay;
	}

	public bool getWeb(){
		return IsUsingWEB;
	}

	void switchCam (){
		if (GameObject.Find ("Selector_2cam") != null) {
			dualCamSelector = GameObject.Find ("Selector_2cam").transform;
		}
		if (GameObject.Find ("Selector")!= null) {
			singleCamselector = GameObject.Find ("Selector").transform;
		}
		if (IsUsingDualDisplay == true) {
			//disabled the dualView make it one view
			if(singleCam == null){
				singleCam = GameObject.Find ("Main Camera").transform;
			}
			//if its still null it doesnt exist
			if(singleCam != null){
				if(currentCam!=null){
					if (dualCamSelector != null) {
						dualCamSelector.gameObject.SetActive(false);
					}
					dualCamSecondary.GetComponent<Cardboard>().setAlignmentMarker(false);
					dualCamSecondary.GetComponent<Cardboard>().setSettingsButton(false);
					dualCamSecondary.GetChild(0).gameObject.SetActive(false);
					dualCamSecondary.GetChild(1).gameObject.SetActive(false);
					currentCam.gameObject.SetActive(false);//currentCam.gameObject.SetActive(true);
				}
				if (singleCamselector != null) {
					singleCamselector.gameObject.SetActive(true);
				}
				currentCam = singleCam;
				currentCam.gameObject.SetActive(true);
			}
			else{
				IsUsingDualDisplay = !IsUsingDualDisplay;
			}

		} 
		else {
			//disabled the singleView make it dual view
			if(dualCam == null){
				if(GameObject.Find ("CardboardHead")!=null){
					dualCam = GameObject.Find ("CardboardHead").transform.GetChild (0).transform;
					dualCamSecondary = GameObject.Find ("Cardboard").transform;
				}
			}
			//if its still null it doesnt exist
			if(dualCam != null){
				if(currentCam != null){
					if (singleCamselector!= null) {
						singleCamselector.gameObject.SetActive(false);
					}
					currentCam.gameObject.SetActive(false);
				}
				if (dualCamSelector != null) {
					dualCamSelector.gameObject.SetActive(true);
				}
				dualCamSecondary.GetComponent<Cardboard>().setAlignmentMarker(true);
				dualCamSecondary.GetComponent<Cardboard>().setSettingsButton(true);
				currentCam = dualCam;
				currentCam.gameObject.SetActive(true);
				dualCamSecondary.GetChild(0).gameObject.SetActive(true);
				dualCamSecondary.GetChild(1).gameObject.SetActive(true);
			}
			else{
				IsUsingDualDisplay = !IsUsingDualDisplay;
			}
		}
		//if(charges!=null) {
		//	for (int i=0; i< charges.Length; i++) {
		//		if (charges [i] != null) {
		//			charges [i].GetComponent<LookAtCamera> ().newCam (currentCam);
		//		}
		//	}
		//}
		IsUsingDualDisplay = !IsUsingDualDisplay;
	}

	public void addChargeToArray (Transform charge){
		if (charges == null) {
			charges = new Transform[0];
		}
		Transform [] newArray = new Transform[charges.Length+1];
		for (int i=0; i< charges.Length; i++) {
			newArray[i] = charges[i];
		}
		newArray[charges.Length] = charge;
		charges = newArray;
		if(charges!=null) {
			for (int i=0; i< charges.Length; i++) {
				if (charges [i] != null) {
					charges [i].GetComponent<LookAtCamera> ().newCam (currentCam);
				}
			}
		}
	}

	void OnGUI(){
		if (Message != "") {
			if (IsUsingWEB) {
				var centeredStyle = GUI.skin.GetStyle ("Label");
				centeredStyle.alignment = TextAnchor.MiddleCenter;
				centeredStyle.normal.textColor = Color.white;
				centeredStyle.fontSize = (int)((Screen.height / 20.0f) * multiplier);
				//GUI.Label (new Rect (Screen.width / 8,  Screen.height/4-(Screen.height/15.0f)*multiplier, Screen.width / 4, (Screen.height/15.0f)*multiplier), "Scan QR Code",centeredStyle);
				GUI.Label (new Rect (Screen.width/10, 0, 4*Screen.width/5 , Screen.height), Message, centeredStyle);
			} else {
				var centeredStyle = GUI.skin.GetStyle ("Label");
				centeredStyle.alignment = TextAnchor.MiddleCenter;
				centeredStyle.normal.textColor = Color.white;
				centeredStyle.fontSize = (int)((Screen.height / 45.0f) * multiplier);
				//GUI.Label (new Rect (Screen.width / 8,  Screen.height/4-(Screen.height/15.0f)*multiplier, Screen.width / 4, (Screen.height/15.0f)*multiplier), "Scan QR Code",centeredStyle);
				//GUI.Label (new Rect (Screen.width / 8, 0,  Screen.width / 4, Screen.height), Message, centeredStyle);
				//GUI.Label (new Rect (5 * Screen.width / 8, 0, Screen.width / 4, Screen.height), Message, centeredStyle);
				GUI.Label (new Rect (3*Screen.width / 20f, 0,  Screen.width / 5, Screen.height), Message, centeredStyle);
				GUI.Label (new Rect (13 * Screen.width / 20f, 0, Screen.width / 5, Screen.height), Message, centeredStyle);
			}
		}

		if (headsUpMessage != "") {
			if (IsUsingWEB) {
				var centeredStyle = GUI.skin.GetStyle ("Label");
				centeredStyle.alignment = TextAnchor.MiddleCenter;
				centeredStyle.normal.textColor = Color.green;
				centeredStyle.fontSize = (int)((Screen.height / 15.0f) * multiplier);
				//GUI.Label (new Rect (Screen.width / 8,  Screen.height/4-(Screen.height/15.0f)*multiplier, Screen.width / 4, (Screen.height/15.0f)*multiplier), "Scan QR Code",centeredStyle);
				GUI.Label (new Rect (Screen.width/10, 0, 4*Screen.width/5 , Screen.height/2f), headsUpMessage, centeredStyle);
			} else {
				var centeredStyle = GUI.skin.GetStyle ("Label");
				centeredStyle.alignment = TextAnchor.MiddleCenter;
				centeredStyle.fontSize = (int)((Screen.height / 25.0f) * multiplier);
				centeredStyle.normal.textColor = Color.green;
				//GUI.Label (new Rect (Screen.width / 8,  Screen.height/4-(Screen.height/15.0f)*multiplier, Screen.width / 4, (Screen.height/15.0f)*multiplier), "Scan QR Code",centeredStyle);
				GUI.Label (new Rect (Screen.width / 8, 0,  Screen.width / 4, Screen.height/2f), headsUpMessage, centeredStyle);
				GUI.Label (new Rect (5 * Screen.width / 8, 0, Screen.width / 4, Screen.height/2f), headsUpMessage, centeredStyle);

			}
		}
	}

}
