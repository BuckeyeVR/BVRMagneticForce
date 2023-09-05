using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneAdvancer : MonoBehaviour {
	public bool IsUsingWEB;
	public float timeRemainingInScene = 20.0f; //custimize time (s) for each level if desired
	public bool lastScene = false;
	public bool onTouchSwitch = false;
	public float DoubleClickBuffer = 1.0f; 
	// Use this for initialization
	void Start () {
		if (lastScene) {
			PlayerPrefs.SetInt ("lastSceneLoaded", 0);
		}
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Backspace)&& DoubleClickBuffer < 0) {
			int lastScene = SceneManager.GetActiveScene ().buildIndex - 1;
			PlayerPrefs.SetInt ("lastSceneLoaded", lastScene);//added
			SceneManager.LoadScene (lastScene);
		} else {
			if (onTouchSwitch) {
				DoubleClickBuffer -= Time.deltaTime;
				if (IsUsingWEB&& DoubleClickBuffer < 0) {
					if (Input.GetKey (KeyCode.Space)) {
						if (lastScene) {
							SceneManager.LoadScene (0);
						} else {
							int nextScene = SceneManager.GetActiveScene ().buildIndex + 1;
							PlayerPrefs.SetInt ("lastSceneLoaded", nextScene);//added
							SceneManager.LoadScene (nextScene);
						}
					}

				} else {
					if (Input.GetMouseButtonDown (0) && DoubleClickBuffer < 0) {
						if (lastScene) {
							SceneManager.LoadScene (0);
						} else {
							int nextScene = SceneManager.GetActiveScene ().buildIndex + 1;
							PlayerPrefs.SetInt ("lastSceneLoaded", nextScene);//added
							SceneManager.LoadScene (nextScene);
						}
					}
				}
			} else {
			
				timeRemainingInScene -= Time.deltaTime;
				if (timeRemainingInScene <= 0) {
					if (lastScene) {
						SceneManager.LoadScene (0);
					} else {
						int nextScene = SceneManager.GetActiveScene ().buildIndex + 1;
						PlayerPrefs.SetInt ("lastSceneLoaded", nextScene);//added
						SceneManager.LoadScene (nextScene);
					}
				}
			}
		}
	}
		
}
