using UnityEngine;
using System;
using System.Collections;
using System.Diagnostics;
using UIVA;

public class UIVA_OpenVibe : MonoBehaviour {
	
	UIVA_Client uiva;
	
	Process uivaProcess;
	
	double anlg = 0.0;//analog
	bool lbtn = false; //left button
	bool rbtn = false; //right button
	
	bool server = false;
	public static bool started = false;

	
	// Use this for initialization
	void Start () 
	{
	//	uiva = new UIVA_Client("localhost");
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if(started)
		{
			GetDeviceData();
		}

	}
	
	
	void OnGUI()
	{
		if(SettingsScript.settingsMenu)
		{

			GUI.BeginGroup (new Rect (150, Screen.height-250, Screen.width-250, 150));
			GUI.Box(new Rect(0, 0, Screen.width-250, 150), "Network");
			GUI.color = Color.grey;
			GUI.Label(new Rect(60, 03, 200, 25), "openvibe-vrpn");
			GUI.color = Color.white;
			
			//display analog
			GUI.Label(new Rect(15, 30, 200, 25), "Analog: " + anlg.ToString("0.000000"));
			//display button
			if (lbtn)
				GUI.Label(new Rect(15, 60, 100, 25), "Button: " + "Left");
			else if (rbtn)
				GUI.Label(new Rect(15, 60, 100, 25), "Button: " + "Right");
			else
				GUI.Label(new Rect(15, 60, 100, 25), "Button: " + " ");	
			
			
			GUI.enabled = !started;				
			//Start sending UDP
			if (GUI.Button (new Rect (10, 90, 90, 30), "Start")) 
			{
				server=true;
				started = true;
				uivaServer();
				
			}
			GUI.enabled = true;	
			
			GUI.enabled = started;		
			//Stop sending UDP
			if (GUI.Button (new Rect (100, 90, 90, 30), "Stop")) 
			{		
				started = false;
				server=false;
				killUivaServer();			
			}
			GUI.enabled = true;		
		
			GUI.EndGroup ();//end network group

			
		}
	}

	
	
	void GetDeviceData()
	{	
       uiva.GetOpenVibeData(out anlg, out lbtn, out rbtn);
	//UnityEngine.Debug.Log("Analog: "+ anlg + " Button: "+lbtn);
		
	}
	

	
	void uivaServer()
	{	
		uivaProcess = new Process();
		uivaProcess.StartInfo.FileName = "UIVA_Server.exe";
//		uivaProcess.StartInfo.UseShellExecute = false;
		uivaProcess.StartInfo.CreateNoWindow = true;		
		uivaProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
		uivaProcess.Start();
		
		uiva = new UIVA_Client("localhost");
		print("Launching UIVA Server");
		
	}
	
	void killUivaServer()
	{
		uiva.Disconnect();
 		uivaProcess.CloseMainWindow();
		print("Killing UIVA Server");
	}
	
	
	void PossibleQuit()
	{
		if(Input.GetKeyDown("escape"))
		{
			uiva.Disconnect();
		}
	}
	
	
	void OnApplicationQuit() 
		{
			if(server)
			{
	         killUivaServer();
			}
		}
	
	
}
