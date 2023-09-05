using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdvanceSelectGroup : MonoBehaviour {
	public float DoubleClickBuffer = 2.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		DoubleClickBuffer -= Time.deltaTime;
		if (Input.GetMouseButtonDown (0) && DoubleClickBuffer < 0) {
			if (PlayerPrefs.GetString ("TreatmentGroup") == "A") {
				SceneManager.LoadScene (2);
			} else {
				SceneManager.LoadScene (6);
			}
		}
	}
}
