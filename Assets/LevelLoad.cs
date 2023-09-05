using UnityEngine;
using System.Collections;

public class LevelLoad : MonoBehaviour {

	public int levelToLoad;
	public Texture levelPicture;
	public Texture introTextPicture;
	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Renderer> ().material.mainTexture = levelPicture;
	}


	public Texture getTexture(){
		return introTextPicture;
	}

	public int getLevel(){
		return levelToLoad;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
