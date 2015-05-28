﻿using UnityEngine;
using System;
using System.Collections;
using System.Diagnostics;
using UIVA;

public class SettingsScript : MonoBehaviour {

	public GameObject leapmotion;
	public GameObject hands;

	public GameObject maleHand, femaleHand;
	bool male = true;
	bool female = false;
	
	public Texture arrow;

	public static bool settingsMenu = false;

	public GUIStyle settingsStyle;

	public static bool audioOn = false;
	public static bool vr = true;
	public static bool freemode, training, online = false;

	public static string timerTimeGUI = "2";
	public static string doorSpeedGUI = "3";
	public static string handleSpeedGUI = "400";
	public static string velocityThreshold = "5";
	public static string angleOffset = "90";

	//UIVA -------------------------------------
	UIVA_Client uiva;
	Process uivaProcess;
	double anlg = 0.0;//analog
	double anlg2 = 0.0;//analog
	public static bool lbtn = false; //left button
	public static bool rbtn = false; //right button
	double thres = 0; //threshold value
	public string threshold = "0.1";
	public static bool lda1 = false;//right
	public static bool lda2 = false;//left
	bool server = false;
	public static bool started = false;
	public float timer;
	float sec;
	bool timeout = false;
	//--------------------------------------------

//	void Awake()
//	{
//		timerTimeGUI = "2";
//		doorSpeedGUI = "3";
//		handleSpeedGUI = "400";
//	}

	// Use this for initialization
	void Start () 
	{
	//	uiva = new UIVA_Client("localhost");

		settingsMenu = true;
		training = true;
		leapmotion.SetActive(false);

		//hand selection
		male = true;
		female = false;
		maleHand.SetActive(true);
		femaleHand.SetActive(false);

		//UIVA
		threshold = "0.1";
		sec = 0.5f;

		angleOffset = "90";
	}
	
	// Update is called once per frame
	void Update () 
	{
		//pause/resume game
		if(settingsMenu)
		{Time.timeScale = 0;}
		else{Time.timeScale = 1;}

		//exit with escape
		if (Input.GetKey (KeyCode.Escape)) 
		{
			Application.Quit();
		}

		//enter/exit settings menu
		if(Input.GetKey(KeyCode.S) && !settingsMenu)
		{
			settingsMenu = true;
		}
		else if(Input.GetKey(KeyCode.Z) && settingsMenu)
		{
			settingsMenu = false;
		}

		//UIVA
		if(started)
		{
			GetDeviceData();			
			LDAOutput();//lda two classes-to-button


			//set analog value to 0 every 0.5 seconds
			timer -= Time.deltaTime;
			if(timer <= 0)
			{
				anlg2 = anlg;
				timer = sec;
			}
			if(anlg.Equals(anlg2))
			{
				timeout = true;
			}
			else{
				timeout = false;
			}	

		}


	}

	void LDAOutput()
	{
		//get threshold value from guibox
		thres = double.Parse(threshold);
		
		if (anlg > 0 + thres && !timeout) // right hand
		{
			lda2 = false;
			lda1 = true;
		}
		else if (anlg < 0 - thres && !timeout) // left hand
		{
			lda1 = false;
			lda2 = true;
		}
		else
		{
			lda1 = false;
			lda2 = false;
		}
	}
	

	void GetDeviceData()
	{
		try
		{            
			uiva.GetOpenVibeData(out anlg, out lbtn, out rbtn);	
		}	
		catch (Exception e)
		{
		//	UnityEngine.Debug.Log("Exception caught: "+ e);
		}
			
		//UnityEngine.Debug.Log("Analog: "+ anlg + " Button: "+lbtn);
	}

	void OnGUI() 
	{
		if(settingsMenu)
		{

			GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "Settings", settingsStyle);

			//================================================================================
			//GUI.Box(new Rect(20, 40, Screen.width-40, 250), "Experimental Pipeline");
			//================================================================================

			//arrows
			GUI.Label(new Rect(Screen.width/2-300, 140, arrow.width/2, arrow.height/2), arrow);//next to mode
			GUI.Label(new Rect(Screen.width/2-60, 140, arrow.width/2, arrow.height/2), arrow);//next to environment
			if(vr)
				GUI.Label(new Rect(Screen.width/2 + 180, 140, arrow.width/2-20, arrow.height/2), arrow);//next to view

			//audio settings

			//Modes
			GUI.BeginGroup(new Rect(Screen.width/2-420, 120, 120, 120));
			GUI.Box(new Rect(0, 0, 120, 120), "Mode");
			GUI.enabled = true; GUI.enabled = true; GUI.enabled = true; 
			GUI.enabled = !freemode && (training || online);
			if (GUI.Button(new Rect(20, 30, 80, 20), "Free Mode"))
			{
				freemode = true;
				training = false;
				online = false;
				leapmotion.SetActive(true);
				hands.SetActive(false);
			}
			GUI.enabled = true;			
			GUI.enabled = !training && (freemode || online);
			if (GUI.Button(new Rect(20, 60, 80, 20), "Training"))
			{
				training = true;
				online = false;
				freemode = false;
				leapmotion.SetActive(false);
				hands.SetActive(true);
			}
			GUI.enabled = true;	
			GUI.enabled = !online && (freemode || training);
			if (GUI.Button(new Rect(20, 90, 80, 20), "Online"))
			{
				online = true;
				freemode = false;
				training = false;
				leapmotion.SetActive(false);
				hands.SetActive(true);
			}
			GUI.enabled = true;	
			GUI.EndGroup();
			//================================================================================

			//VR-GUI
			GUI.BeginGroup(new Rect(Screen.width/2-180, 120, 120, 90));
			GUI.Box(new Rect(0, 0, 120, 90), "Environment");
			GUI.enabled = true; GUI.enabled = true; GUI.enabled = true; 
			GUI.enabled = !vr;
			if (GUI.Button(new Rect(35, 30, 50, 20), "VR"))
			{
				vr = true;
			}
			GUI.enabled = true;	

			if(!freemode)//dont draw on freemode - leapmotions
			{
			GUI.enabled = vr;
			if (GUI.Button(new Rect(35, 60, 50, 20), "2D"))
			{
				vr = false;
			}
			GUI.enabled = true;	
			}

			GUI.EndGroup();
			//================================================================================

			GUI.BeginGroup(new Rect(20, Screen.height-210, Screen.width-40, 180));
			GUI.Box(new Rect(0, 0, Screen.width-40, 180), "General Settings");

			audioOn = GUI.Toggle(new Rect(50, 50, 80, 20), audioOn, "Audio On");
			//ToDo: audio volume

			//timer in sec
			GUI.Label(new Rect(90, 100, 200, 20), "Door Timeout (sec)");
			timerTimeGUI = GUI.TextField(new Rect(50, 100, 30, 20), timerTimeGUI, 25);		
			//door speed
			GUI.Label(new Rect(90, 120, 200, 20), "Door Speed");
			doorSpeedGUI = GUI.TextField(new Rect(50, 120, 30, 20), doorSpeedGUI, 25);
			//handle speed
			GUI.Label(new Rect(90, 140, 200, 20), "Handle Speed");
			handleSpeedGUI = GUI.TextField(new Rect(50, 140, 30, 20), handleSpeedGUI, 25);


			//isHeadMounted
			//mirror Z


			//handle velocity threshold
			GUI.Label(new Rect(290, 140, 200, 20), "Velocity Threshold");
			velocityThreshold = GUI.TextField(new Rect(250, 140, 30, 20), velocityThreshold, 25);

			//angle offset
			GUI.Label(new Rect(290, 100, 200, 20), "Angle Offset");
			angleOffset = GUI.TextField(new Rect(250, 100, 30, 20), angleOffset, 25);

			GUI.EndGroup();

			//================================================================================

			if(SettingsScript.vr)
			{
				
				GUI.BeginGroup(new Rect(Screen.width/2 + 300, 120, 120, 90));
				GUI.Box(new Rect(0, 0, 120, 90), "Hand Model");
				//			male = GUI.Toggle(new Rect(10, 30, 80, 20), male, "Male");
				//			female = GUI.Toggle(new Rect(10, 50, 80, 20), female, "Female");
				
				GUI.enabled = !male;
				if (GUI.Button(new Rect(35, 30, 50, 20), "Male"))
				{
					male = true;
					female = false;
					maleHand.SetActive(true);
					femaleHand.SetActive(false);
				}
				GUI.enabled = true;
				
				GUI.enabled = male;
				if (GUI.Button(new Rect(30, 60, 60, 20), "Female"))
				{
					male = false;
					female = true;
					maleHand.SetActive(false);
					femaleHand.SetActive(true);
				}
				GUI.enabled = true;			
				
				GUI.EndGroup();			
			}

			//================================================================================

			GUI.BeginGroup (new Rect (Screen.width/2 + 180, Screen.height-190, 190, 150));
			
			GUI.color = Color.white;
			GUI.Label(new Rect(60, 03, 200, 25), "openvibe-vrpn");
			GUI.color = Color.white;
			
			//display analog
			GUI.Label(new Rect(15, 30, 150, 25), "Analog: " + anlg.ToString("0.000"));
			//display button
			if (lbtn)
				GUI.Label(new Rect(15, 60, 100, 25), "Button: " + "Left");
			else if (rbtn)
				GUI.Label(new Rect(15, 60, 100, 25), "Button: " + "Right");
			else
				GUI.Label(new Rect(15, 60, 200, 25), "Button: " + " "+lbtn+":"+rbtn);	
			
			//display lda outcome
			if (lda2)
				GUI.Label(new Rect(140, 30, 50, 25), "<");
			else if (lda1)
				GUI.Label(new Rect(140, 30, 50, 25), ">");
			else
				GUI.Label(new Rect(140, 30, 50, 25), "-");
			//threshold
			threshold = GUI.TextField(new Rect(160, 30, 25, 20), threshold, 25);
			
			
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

			//================================================================================


			if (GUI.Button(new Rect(Screen.width/2-40, Screen.height-280, 90, 30), "Play!"))
			{
				settingsMenu = false;
			}
			GUI.color = Color.black;
			GUI.Label(new Rect(10, Screen.height-25, 200, 20), "Press 'Z' to resume, 'S' to pause ");

		}
	}


	void uivaServer()
	{	
		uivaProcess = new Process();
		uivaProcess.StartInfo.FileName = "UIVA_Server.exe";
		//		uivaProcess.StartInfo.UseShellExecute = false;
		uivaProcess.StartInfo.CreateNoWindow = true;		
		uivaProcess.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
		uivaProcess.Start();
		
		uiva = new UIVA_Client("localhost");
		print("Launching UIVA Server");
		
	}
	
	void killUivaServer()
	{
//		uivaProcess.CloseMainWindow();
		uivaProcess.Kill();
		print("Killing UIVA Server");
		uiva.Disconnect();
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
