using UnityEngine;
using System.Collections;

public class HandCollision : MonoBehaviour {

	public static bool grabbed = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if(GrabbingHand.closedLeft && grabbed)
		{
		//	Debug.Log("Turn Left!");
		}
		if(GrabbingHand.closedRight && grabbed)
		{
		//	Debug.Log("Turn Right!");
		}
	
	}


	void OnCollisionEnter (Collision col)
	{

		if(col.gameObject.name.Contains("bone") || col.gameObject.name.Contains("palm"))
		{
			grabbed = true;
		//	Debug.Log (gameObject.name + " hand collision");
		}
		else
		{
			grabbed = false;
		}
	}

	void OnCollisionExit (Collision col)
	{

		if(col.gameObject.name.Contains("bone") || col.gameObject.name.Contains("palm"))
		{
			grabbed = false;
		
		}
	}

}
