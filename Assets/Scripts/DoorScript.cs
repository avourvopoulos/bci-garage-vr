using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour {

	float doorInit;
	public static float max, min;

	public static bool doorOpen = false;
	public static float doorSpeed = 3f;

	public static bool isPlaying = false;

	public static float timer;
	bool timeout = true;

	// Use this for initialization
	void Awake () {
	
		doorInit = transform.position.y;
		max = doorInit + transform.localScale.y - 0.3f;
		min = doorInit;

		timer = float.Parse (SettingsScript.timerTimeGUI); 
		doorSpeed = float.Parse (SettingsScript.doorSpeedGUI); 

	}
	
	// Update is called once per frame
	void Update () {

		if(SettingsScript.vr)
		{
			//garage sound
			if((MoveHandle.getAction ()== "onmove" && !isPlaying) && SettingsScript.audioOn)
			{
				audio.Play();
				isPlaying=true;
			}
			if(MoveHandle.getAction ()== "halt" && isPlaying)
			{
				audio.Pause();
				isPlaying=false;
			}

			//update door speed realtime
			doorSpeed = float.Parse (SettingsScript.doorSpeedGUI); 
		}

		//roll up
//		if((Input.GetKey(KeyCode.UpArrow) || MoveHandle.getDirection ()== "up") &&(transform.position.y < max) )

		if(SettingsScript.training || SettingsScript.online )
		{
			if((MoveHandle.getAction ()== "onmove") &&(transform.position.y < max) )
			{
			transform.Translate(Vector3.up * Time.deltaTime * doorSpeed, Space.World);
				doorOpen = false;
			//	Debug.Log(Vector3.up.y * Time.deltaTime);
			}
//			else if((MoveHandle.getAction ()== "halt") &&(transform.position.y < max) )
//			{
//			transform.position= new Vector3 (transform.position.x, transform.position.y, transform.position.z);
//				doorOpen = true;
//				audio.Stop();
//			}
			else if((MoveHandle.getAction ()== "onmove") && (transform.position.y >= max) )
			{
				transform.position= new Vector3 (transform.position.x, max, transform.position.z);
				doorOpen = true;
				audio.Stop();
			}
//		else if((!SettingsScript.lbtn && !SettingsScript.rbtn) && timeout)
			else if(timeout)
			{
				timeout = false;
				transform.position = new Vector3 (transform.position.x, doorInit, transform.position.z);
				//	animation.Play();
				doorOpen = false;
				MoveHandle.lefthand = false;
				MoveHandle.righthand = false;
				MoveHandle.rot0 = 0;
				MoveHandle.rot1 = 0;
			}
			else 
			{
				transform.Translate(Vector3.zero, Space.World);

			}
		}

		if(SettingsScript.freemode)
		{
			if((MoveHandle.getRotation()== "fwd") && (transform.position.y < max) )
			{
				transform.Translate(Vector3.up * Time.deltaTime*3, Space.World);
				doorOpen = false;
				//	Debug.Log(Vector3.up.y * Time.deltaTime);
			}
			else if((MoveHandle.getRotation() == "bwd") && (transform.position.y >= max) )
			{
				transform.position= new Vector3 (transform.position.x, max, transform.position.z);
				doorOpen = true;
				audio.Stop();
			}
			else if(timeout)
			{
				timeout = false;
				transform.position = new Vector3 (transform.position.x, doorInit, transform.position.z);
				//	animation.Play();
				doorOpen = false;
				MoveHandle.lefthand = false;
				MoveHandle.righthand = false;
				MoveHandle.rot0 = 0;
				MoveHandle.rot1 = 0;
			}
			else 
			{
				transform.Translate(Vector3.zero, Space.World);
				
			}
		}

		//roll down
//		if((Input.GetKey(KeyCode.DownArrow) || MoveHandle.getDirection ()== "down") && (transform.position.y > min))
//		{
//			transform.Translate(Vector3.down * Time.deltaTime, Space.World);
//		//	Debug.Log(Vector3.down.y * Time.deltaTime);
//		}
//		else if((Input.GetKey(KeyCode.DownArrow) || MoveHandle.getDirection ()== "down") && (transform.position.y <= min) )
//		{
//			transform.position= new Vector3 (transform.position.x, min, transform.position.z);
//		}


	//	Debug.Log ("current: "+ transform.position.y +" , max: "+ max);

		if(transform.position.y >= max)
		{
			//close door after 2 seconds
			timer -= Time.deltaTime;
			if(timer <= 0 )
			{
				timeout = true;
				timer = float.Parse (SettingsScript.timerTimeGUI); 

				Scoring.score = 1 + Scoring.score;
			}


		}

		//close door
		if(Input.GetKey(KeyCode.X))
		{
			timeout = true;
			transform.position = new Vector3 (transform.position.x, doorInit, transform.position.z);
		//	animation.Play();
			doorOpen = false;
			MoveHandle.lefthand = false;
			MoveHandle.righthand = false;
			MoveHandle.rot0 = 0;
			MoveHandle.rot1 = 0;
		}

	}
}
