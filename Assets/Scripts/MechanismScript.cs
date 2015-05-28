using UnityEngine;
using System.Collections;

public class MechanismScript : MonoBehaviour {

	public GameObject MechL;
	public GameObject MechR;

	// Use this for initialization
	void Awake () 
	{
		MechL.gameObject.SetActive (false);
		MechR.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {

		if (MoveHandle.lefthand)
		{
			MechL.gameObject.SetActive (true);
			MechR.gameObject.SetActive (false);
		}
		else if (MoveHandle.righthand)
		{
			MechR.gameObject.SetActive (true);
			MechL.gameObject.SetActive (false);
		}
		else{
			MechL.gameObject.SetActive (false);
			MechR.gameObject.SetActive (false);
		}


		if (SettingsScript.lbtn)
		{
			MechL.gameObject.SetActive (true);
			MechR.gameObject.SetActive (false);
		}
		else if (SettingsScript.rbtn)
		{
			MechR.gameObject.SetActive (true);
			MechL.gameObject.SetActive (false);
		}
		else{
			MechL.gameObject.SetActive (false);
			MechR.gameObject.SetActive (false);
		}

		if (SettingsScript.freemode)
		{
			if (SettingsScript.lbtn)
			{
				MechL.gameObject.SetActive (true);
				MechR.gameObject.SetActive (false);
			}
			else if (SettingsScript.rbtn)
			{
				MechR.gameObject.SetActive (true);
				MechL.gameObject.SetActive (false);
			}
			else{
				MechL.gameObject.SetActive (false);
				MechR.gameObject.SetActive (false);
			}
		}
		
	}
}
