using UnityEngine;
using System.Collections;

public class CollissionScript : MonoBehaviour {

	public static bool grabbed = false;
	Vector3 init;

	// Use this for initialization
	void Start () {

		init = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{

	}

	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.name.Contains("palm"))
		{
			grabbed = true;
		}
	}


	void OnCollisionStay (Collision col)
	{

		if(col.gameObject.name.Contains("palm"))
		{
		//	print("hand touches: "+ gameObject.name);
			gameObject.transform.position = GameObject.Find("palm").transform.position;
		}
		else{
			gameObject.transform.position = init;
		}
	}

	void OnCollisionExit (Collision col)
	{	
		if(col.gameObject.name.Contains("palm"))
		{
			grabbed = false;
		}
	}


//	bool inBorders()
//	{
//		if (gameObject.transform.position.z < GameObject.Find ("Border1").transform.position.z 
//						&& gameObject.transform.position.z > GameObject.Find ("Border2").transform.position.z
//						&& gameObject.transform.position.y < GameObject.Find ("Border3").transform.position.y
//						&& gameObject.transform.position.y > GameObject.Find ("Border4").transform.position.y)
//						return true;
//				else 
//						return false;
//
//	}


}
