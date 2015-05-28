using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class Scoring : MonoBehaviour {

	public GUIStyle txtstyle;

	double counttime = 0;
	public static int score = 0;

	TextWriter file;
	public static string timestamp;
	static string date = String.Empty;
	string filepath = String.Empty;


	void Awake()
	{
		date = DateTime.Now.ToString("hh:mm dd-MM-yyyy"); // get date
		
	}

	// Update is called once per frame
	void Update () 
	{
		counttime += Time.deltaTime;
		timestamp = DateTime.UtcNow.Minute.ToString("00")+DateTime.UtcNow.Second.ToString("00")+DateTime.Now.Millisecond.ToString("0000"); //time in min:sec:usec

	//	reset values
		if(Input.GetKey(KeyCode.R))
		{
			score = 0;
			counttime = 0;
		}

	}

	void OnGUI() {
		GUI.Label(new Rect(10, 10, 200, 20), score+" / "+counttime.ToString("0"), txtstyle);
	}


	
	void writeFile()
	{
		string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "/DataLog/";

		if(!Directory.Exists(path))
		{
			System.IO.Directory.CreateDirectory(path);
		}
		filepath = path + "data_"+ DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";//save on Desktop
		file = new StreamWriter(filepath, true);
		file.Write(score+" / "+counttime.ToString("0"));
		file.WriteLine("");
		file.Close();	
		Debug.Log(filepath+" logged!");
	}

//	void OnDisable()
//	{
//		writeFile ();
//	}
	void OnApplicationQuit () 
	{
		writeFile ();
	}

}