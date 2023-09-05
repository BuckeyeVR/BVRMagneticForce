using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class playbackTrack : MonoBehaviour {
    public string URL = "";
	public string fileNameBase= "";
    private bool playData = false;
    private bool doneDownload = false;
    private int counter = 0;
    private int itter = 0;
    List<Quaternion> rots = new List<Quaternion>();
    List<Vector3> poss = new List<Vector3>();
	List<Vector3> eulerRot = new List<Vector3>();
    // Use this for initialization
    void Start () {
       // StartCoroutine(startDownload());
		if (PlayerPrefs.GetString ("Record") == "true") 
		{playData = false;}
		else{playData=true;}

		if (playData) {
			GameObject.Find ("CardboardHead").GetComponent <CardboardHead>().enabled = false; 

		}
		readFromFile();

    }
	
	// Update is called once per frame
	void Update ()

	{
		if (doneDownload == true && playData == true && itter < eulerRot.Count) {
			//if (counter % 2 == 0) {
				//transform.rotation = rots [itter];

			transform.localEulerAngles = eulerRot [itter];

				//transform.localRotation=rots [itter];
				//Debug.Log ("Rotated");
				itter++;
				//counter = 1;
			//} else {
				//counter = 0;
			//}
		}
	}

	private static string getPath()
	{
		#if UNITY_EDITOR
		return Application.dataPath;
		#elif UNITY_ANDROID
		return Application.persistentDataPath;
		#else
		return Application.dataPath;
		#endif
		}
	public void readFromFile()
	{
		//Debug.Log ("StartRead");
		string filename = getPath()+ "/" + fileNameBase +"_"+PlayerPrefs.GetString("TreatmentGroup")+"_"+PlayerPrefs.GetString("ID")+ ".txt"; //Maybe read from cardboard head/share in future
		Debug.Log(filename);
		string fullText = System.IO.File.ReadAllText(filename);

		while (fullText!=null && fullText.Contains("\n"))
		{
			string line = fullText.Substring(0, fullText.IndexOf("\n"));

			//string linePos = line.Substring(1, line.IndexOf("|||")-1);//may not be -1, might be -0



		//	if (line.Contains("("))
		//	{
		//		line = line.Substring(line.IndexOf("(") + 1);
		//	}
		//	if (line.Contains(")"))
		//	{
		//		line = line.Substring(0, line.IndexOf(")") - 1);
		//	}
		//	string[] sArray = line.Split(',');
		//	Quaternion resultRot = new Quaternion(float.Parse(sArray[0]), float.Parse(sArray[1]), float.Parse(sArray[2]),float.Parse(sArray[3]));
		//	rots.Add(resultRot);
		//	Debug.Log (resultRot);
			//line = line.Substring(line.IndexOf("|||")+3);//might be plus 2 rather than 3


			//line = linePos;

			if (line.Contains("("))
			{
				line= line.Substring(line.IndexOf("(")+1);
			}
			if (line.Contains(")"))
			{
				line = line.Substring(0,line.IndexOf(")")-1);
			}
			string[] sArray = line.Split(',');
			Vector3 resultEuler = new Vector3(float.Parse(sArray[0]),float.Parse(sArray[1]),float.Parse(sArray[2]));
			eulerRot.Add (resultEuler);
			//poss.Add(resultPos);

			//Debug.Log(resultPos);
			fullText = fullText.Substring(fullText.IndexOf("\n") + 1);
		}
		doneDownload = true;
		Debug.Log ("FinishedRead");

	}
    IEnumerator startDownload()
    {
        //PHP file would look something like this
        /*
         * <?php
                echo file_get_contents ("somedata.txt");
           ?>
         * 
         */
        WWW download = new WWW(URL);

        yield return download;
        string fullText = download.text;
        while (fullText!=null && fullText.Contains("|||"))
        {
            string line = fullText.Substring(0, fullText.IndexOf("\n"));
            string linePos = line.Substring(0, line.IndexOf("|||")-1);//may not be -1, might be -0

            line = line.Substring(line.IndexOf("|||")+3);//might be plus 2 rather than 3
            if (linePos.Contains("("))
            {
                linePos = linePos.Substring(linePos.IndexOf("(")+1);
            }
            if (linePos.Contains(")"))
            {
                linePos = linePos.Substring(0,linePos.IndexOf(")")-1);
            }
            string[] sArray = linePos.Split(',');
            Vector3 resultPos = new Vector3(float.Parse(sArray[0]),float.Parse(sArray[1]),float.Parse(sArray[2]));
            poss.Add(resultPos);
            if (line.Contains("("))
            {
                line = line.Substring(line.IndexOf("(") + 1);
            }
            if (line.Contains(")"))
            {
                line = line.Substring(0, line.IndexOf(")") - 1);
            }
            sArray = linePos.Split(',');
            Quaternion resultRot = new Quaternion(float.Parse(sArray[0]), float.Parse(sArray[1]), float.Parse(sArray[2]),float.Parse(sArray[3]));
            rots.Add(resultRot);
            fullText = fullText.Substring(fullText.IndexOf("\n") + 1);
        }
        doneDownload = true;
    }

}
