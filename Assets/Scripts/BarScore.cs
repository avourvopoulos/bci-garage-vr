using UnityEngine;
using System.Collections;

public class BarScore : MonoBehaviour {

	public GameObject leftbar, rightbar;
	
	Vector3 initPosL, initPosR;
	Vector3 initScaleL, initScaleR;

	bool isPlaying = false;

	// Use this for initialization
	void Start () 
	{
		leftbar.SetActive (true);
		rightbar.SetActive (true);

		initPosL = leftbar.transform.position;
		initScaleL = leftbar.transform.localScale;
		initPosR = rightbar.transform.position;
		initScaleR = rightbar.transform.localScale;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!SettingsScript.vr && SettingsScript.audioOn)
		{
			//beep sound
			if((SettingsScript.lbtn || SettingsScript.rbtn) && !isPlaying)
			{
				audio.Play();
				isPlaying=true;
			}
			else if((!SettingsScript.lbtn && !SettingsScript.rbtn))
			{
				audio.Stop(); 
				isPlaying=false;
			}

		}


		if(Input.GetKey(KeyCode.Q) || (SettingsScript.lbtn && SettingsScript.lda2))
		{
			leftbar.transform.localScale = new Vector3(leftbar.transform.localScale.x + 0.8f, leftbar.transform.localScale.y, leftbar.transform.localScale.z);
			leftbar.transform.position = new Vector3(leftbar.transform.position.x + 0.4f, leftbar.transform.position.y, leftbar.transform.position.z);
		}
		else if (SettingsScript.lbtn && SettingsScript.lda1)
		{
			//bar stable
			leftbar.transform.localScale = new Vector3(leftbar.transform.localScale.x , leftbar.transform.localScale.y, leftbar.transform.localScale.z);
		}
		else if (!SettingsScript.lbtn && !SettingsScript.rbtn)
		{
			//bar reset
			leftbar.transform.localScale = initScaleL;
			leftbar.transform.position = initPosL;
		}

		//right bar
		if(Input.GetKey(KeyCode.W) || (SettingsScript.rbtn && SettingsScript.lda1))
		{
			rightbar.transform.localScale = new Vector3(rightbar.transform.localScale.x - 0.8f, rightbar.transform.localScale.y, rightbar.transform.localScale.z);
			rightbar.transform.position = new Vector3(rightbar.transform.position.x - 0.4f, rightbar.transform.position.y, rightbar.transform.position.z);
		}
		else if(SettingsScript.rbtn && !SettingsScript.lda2)
		{
			//bar stable
			rightbar.transform.localScale = new Vector3(rightbar.transform.localScale.x , rightbar.transform.localScale.y, rightbar.transform.localScale.z);
		}
		else if (!SettingsScript.lbtn && !SettingsScript.rbtn)
		{
			//bar reset
			rightbar.transform.localScale = initScaleL;
			rightbar.transform.position = initPosR;
		}

	}
}
