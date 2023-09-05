// Copyright 2014 Google Inc. All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using UnityEngine;
using System.Collections;//added
using System.IO; //added
using System;
/// @ingroup Scripts
/// This script provides head tracking support for a camera.
///
/// Attach this script to any game object that should match the user's head motion.
/// By default, it continuously updates the local transform to Cardboard.HeadView.
/// A target object may be specified to provide an alternate reference frame for the motion.
///
/// This script will typically be attached directly to a Camera object, or to its
/// parent if you need to offset the camera from the origin.
/// Alternatively it can be inserted as a child of the Camera and parent of the
/// CardboardEye camera.  Do this if you already have steering logic driving the
/// mono Camera and wish to have the user's head motion be relative to that.  Note
/// that in the latter setup, head tracking is visible only when VR Mode is enabled.
///
/// In some cases you may need two instances of CardboardHead, referring to two
/// different targets (one of which may be the parent), in order to split where
/// the rotation is applied from where the positional offset is applied.  Use the
/// #trackRotation and #trackPosition properties in this case.
public class CardboardHead : MonoBehaviour {
  private bool shouldContinue = true;
  /// Determines whether to apply the user's head rotation to this gameobject's
  /// orientation.  True means to update the gameobject's orientation with the
  /// user's head rotation, and false means don't modify the gameobject's orientation.
  public bool trackRotation = true;

  /// Determines whether to apply ther user's head offset to this gameobject's
  /// position.  True means to update the gameobject's position with the user's head offset,
  /// and false means don't modify the gameobject's position.
  public bool trackPosition = true;

  /// The user's head motion will be applied in this object's reference frame
  /// instead of the head object's parent.  A good use case is for head-based
  /// steering.  Normally, turning the parent object (i.e. the body or vehicle)
  /// towards the direction the user is looking would carry the head along with it,
  /// thus creating a positive feedback loop.  Use an external target object as a
  /// fixed point of reference for the direction the user is looking.  Often, the
  /// grandparent or higher ancestor is a suitable target.
  public Transform target;


  /// Determines whether the head tracking is applied during `LateUpdate()` or
  /// `Update()`.  The default is false, which means it is applied during `LateUpdate()`
  /// to reduce latency.
  ///
  /// However, some scripts may need to use the camera's direction to affect the gameplay,
  /// e.g by casting rays or steering a vehicle, during the `LateUpdate()` phase.
  /// This can cause an annoying jitter because Unity, during this `LateUpdate()`
  /// phase, will update the head object first on some frames but second on others.
  /// If this is the case for your game, try switching the head to apply head tracking
  /// during `Update()` by setting this to true.
  public bool updateEarly = false;
	public bool trackUser = false; //added
	public string fileNameBase= "";
	private bool recording; //added
	public float maxRecTime = 90; //added max time (s) for record. 
  /// Returns a ray based on the heads position and forward direction, after making
  /// sure the transform is up to date.  Use to raycast into the scene to determine
  /// objects that the user is looking at.
  public Ray Gaze {
    get {
      UpdateHead();
      return new Ray(transform.position, transform.forward);
    }
  }

  private bool updated;
	private int counter = 0;//added
	private float currentTime=0;

  void Update() {
    updated = false;  // OK to recompute head pose.
    if (updateEarly) {
      UpdateHead();
    }
  }

  // Normally, update head pose now.
  void LateUpdate() {
    UpdateHead();
  }

  void Start(){

		if (PlayerPrefs.GetString ("Record") == "true") { //check for play vs record
			recording = true;
		} else {
			recording = false;
		}

		if (GameObject.Find ("IntroLogo_Settings")) {
			if(GameObject.Find ("IntroLogo_Settings").GetComponent<Intro>().getIsWeb()==true){
				shouldContinue=false;
			}
		}
		if (GameObject.Find ("DisplayController(Clone)")) {
			if(GameObject.Find ("DisplayController(Clone)").GetComponent<DisplayControllerScript>().getWeb()==true){
				shouldContinue=false;
			}
		}
		if (GameObject.Find ("DisplayController")) {
			if(GameObject.Find ("DisplayController").GetComponent<DisplayControllerScript>().getWeb()==true){
				shouldContinue=false;
			}
		}
		if (shouldContinue == false) {
			Destroy(this);
		}
	}

  // Compute new head pose.
  private void UpdateHead() {
    if (updated) {  // Only one update per frame, please.
      return;
    }
		currentTime+= Time.deltaTime;

    updated = true;
    Cardboard.SDK.UpdateState();

    if (trackRotation) {
      var rot = Cardboard.SDK.HeadPose.Orientation;
      if (target == null) {
        transform.localRotation = rot;
      } else {
        transform.rotation = target.rotation * rot;
      }
    }

    if (trackPosition) {
      Vector3 pos = Cardboard.SDK.HeadPose.Position;
      if (target == null) {
        transform.localPosition = pos;
      } else {
        transform.position = target.position + target.rotation * pos;
      }
    }

		if (trackUser &&recording && currentTime < maxRecTime)//added
		{
			//if (counter % 2 == 0) //not sure why not saving every, but seems to work --JS
			//{
				//Debug.Log (transform.localRotation);
				//Debug.Log (transform.rotation);
				//Debug.Log (transform.rotation.eulerAngles);
				//Debug.Log (transform.localEulerAngles);

				//StartCoroutine(PostData(transform.position,transform.rotation));
			saveDataToFile(transform.localEulerAngles,currentTime);
				//counter = 1;
			//}
			//else
			//{
				//counter = 0;
			//}
		}
  }

	private void saveDataToFile(Vector3 eulerRot, float time)
	{

		try{
			string filename =  Application.persistentDataPath+ "/" + fileNameBase +"_"+PlayerPrefs.GetString("TreatmentGroup")+"_"+PlayerPrefs.GetString("ID")+ ".txt";
		if (!System.IO.File.Exists (filename)) {
			System.IO.File.WriteAllText (filename, "");
		}

		StreamWriter w = File.AppendText(filename);
		w.Write(time.ToString()+ eulerRot.ToString()+"\n");
		w.Close();
		}		catch (Exception e) {
			//Debug.Log(filename);
		}

	}

/*	private static string getPath()
	{
		#if UNITY_EDITOR
		return Application.dataPath;
		#elif UNITY_ANDROID
		return Application.persistentDataPath;
		#else
		return (Application.dataPath;
		#endif
	}
*/

	IEnumerator PostData(Vector3 pos, Quaternion rot)
	{
		WWWForm form = new WWWForm();
		form.AddField("pos", pos.ToString());
		form.AddField("rot", rot.ToString());
		form.AddField("fileName", "blah");//this could be a random string or it could be the users name maybe

		WWW download = new WWW("http://www.server.com/saveMove.php", form);
		yield return download;
		//THIS IS WHERE HAVING A SERVER IS NECESSARY
		//THIS IS THE PHP CODE TO SAVE POS + ROT to a file
		/*
         * <?php
                $f = fopen($_POST['fileName'].".txt",'a');
                flock($f, LOCK_EX);
				fwrite($f, $_POST['pos']."|||".$_POST['rot']);
				flock($f, LOCK_UN);
				fclose($f);
            ?>
         */

	}


}
