using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

	public GameObject simpleCamera;
	public GameObject oculusCamera;
	public GameObject simpleCamera2D;
	public GameObject oculusCamera2D;

	bool mainCam = true;
	bool oculusCam = false;
	bool mainCam2d = false;
	bool oculusCam2d = false;

	// Use this for initialization
	void Start () 
	{
		oculusCam = false;
		mainCam = true;
		mainCam2d = false;
		oculusCam2d = false;
		oculusCamera.SetActive(false);
		simpleCamera.SetActive(true);
		oculusCamera2D.SetActive(false);
		simpleCamera2D.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI() 
	{
		if(SettingsScript.settingsMenu)
		{

			//on//off cameras
			GUI.BeginGroup(new Rect(Screen.width/2 + 60, 120, 120, 90));
			GUI.Box(new Rect(0, 0, 120, 90), "View Control");
			
			GUI.enabled = mainCam || mainCam2d;
			if (GUI.Button(new Rect(30, 30, 60, 20), "Oculus"))
			{
				if(SettingsScript.vr)
				{
					oculusCam = true;
					mainCam = false;
					mainCam2d = false;
					oculusCam2d = false;
					oculusCamera.SetActive(true);
					simpleCamera.SetActive(false);
					oculusCamera2D.SetActive(false);
					simpleCamera2D.SetActive(false);
				}
				else
				{
					oculusCam = false;
					mainCam = false;
					mainCam2d = false;
					oculusCam2d = true;
					oculusCamera.SetActive(false);
					simpleCamera.SetActive(false);
					oculusCamera2D.SetActive(true);
					simpleCamera2D.SetActive(false);
				}
			}
			GUI.enabled = true;
			
			GUI.enabled = oculusCam || oculusCam2d;
			if (GUI.Button(new Rect(10, 60, 100, 20), "Main Camera"))
			{
				if(SettingsScript.vr)
				{
					oculusCam = false;
					mainCam = true;
					mainCam2d = false;
					oculusCam2d = false;
					oculusCamera.SetActive(false);
					simpleCamera.SetActive(true);
					oculusCamera2D.SetActive(false);
					simpleCamera2D.SetActive(false);
				}
				else
				{
					oculusCam = false;
					mainCam = false;
					mainCam2d = true;
					oculusCam2d = false;
					oculusCamera.SetActive(false);
					simpleCamera.SetActive(false);
					oculusCamera2D.SetActive(false);
					simpleCamera2D.SetActive(true);
				}
			}
			GUI.enabled = true;		
			
			GUI.EndGroup();


		}
	}

}
