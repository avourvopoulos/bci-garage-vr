using UnityEngine;
using System.Collections;

public class HandSelectionScript : MonoBehaviour {

	public GameObject maleHand, femaleHand;
	bool male = true;
	bool female = false;

	// Use this for initialization
	void Start () 
	{
		male = true;
		female = false;
		maleHand.SetActive(true);
		femaleHand.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () 
	{
//		if(male)
//		{
//		//	female = false;
//			maleHand.SetActive(true);
//			femaleHand.SetActive(false);
//		}
//		if(female)
//		{
//		//	male = false;
//			maleHand.SetActive(false);
//			femaleHand.SetActive(true);
//		}

	}

	void OnGUI() 
	{
		if(SettingsScript.settingsMenu && SettingsScript.vr)
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
	}
}
