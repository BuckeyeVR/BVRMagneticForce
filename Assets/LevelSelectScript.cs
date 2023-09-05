using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
//Some changes -> Scene Manager -JS
public class LevelSelectScript : MonoBehaviour {
	public Transform FakeGlowBox;
	private GameObject glow;
	public float startSize;
	public float endSize;
	public float shrinkRate;
	public float growRate;
	private float currentSize;
	private bool shouldShow = false;
	private bool isTransferingLevel = false;
	private Texture currentTexture = null;
	private bool IsUsingDualDisplay = false;
	private Transform displayController;

	// Use this for initialization
	void Start () {
		displayController = GameObject.Find ("DisplayController").transform;
		currentSize = startSize;
	}

	public float getSize(){
		return currentSize;
	}

	IEnumerator transferLevel(int i){
		yield return new WaitForSeconds (2);
		SceneManager.LoadScene(i);
	}
	void OnDisable() {
		if(glow!=null){
			Destroy (glow);
		}
		currentSize = startSize;
		transform.localScale = new Vector3(currentSize,1,currentSize);
	}

	void OnGUI(){
		if (shouldShow == true) {
			IsUsingDualDisplay = displayController.GetComponent<DisplayControllerScript>().getDisplayMode();
			if (IsUsingDualDisplay == false) {
				GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), currentTexture);
			} else {
				GUI.DrawTexture (new Rect (0, 0, Screen.width / 2, Screen.height), currentTexture);
				GUI.DrawTexture (new Rect (Screen.width / 2, 0, (Screen.width/2), Screen.height), currentTexture);
			}
		}
	}

	// Update is called once per frame
	void Update () {

		Vector3 fwd = transform.TransformDirection(Vector3.down);
		RaycastHit hit;
		if (Physics.Raycast(transform.position, fwd, out hit)) {
			if(hit.transform.GetComponent<LevelLoad>()){
				if(glow == null){
					glow = Instantiate(FakeGlowBox.gameObject,Vector3.zero,Quaternion.identity) as GameObject;
					glow.transform.parent = hit.transform;
					glow.transform.localPosition = Vector3.zero;
					glow.transform.localRotation = Quaternion.identity;
					glow.transform.localScale = new Vector3(10.5f,1,10.5f);
				}
				if(currentSize>endSize){
					currentSize-=shrinkRate;
				}
				else{
					if(isTransferingLevel==false){
						shouldShow = true;
						isTransferingLevel = true;
						currentTexture = hit.transform.GetComponent<LevelLoad>().getTexture();
						StartCoroutine (transferLevel(hit.transform.GetComponent<LevelLoad>().getLevel()));
					}
				}
			}
			else{
				if(currentSize<startSize){
					currentSize += growRate;
				}
				else{
					currentSize = startSize;
				}
				if(glow!=null){
					Destroy (glow);
				}
			}
		}
		else{
			if(currentSize<startSize){
				currentSize += growRate;
			}
			else{
				currentSize = startSize;
			}
			if(glow!=null){
				Destroy (glow);
			}
		}
		transform.localScale = new Vector3(currentSize,1,currentSize);
	}
}
