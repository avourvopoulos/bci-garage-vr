using UnityEngine;
using System.Collections;

public class HandFollowScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		//male hands
		if(this.gameObject.name=="LeftHandMale")
			transform.position = new Vector3 (GameObject.Find ("CL").transform.position.x, GameObject.Find ("CL").transform.position.y-3, GameObject.Find ("CL").transform.position.z);
		if(this.gameObject.name=="RightHandMale")
			transform.position = new Vector3 (GameObject.Find ("CR").transform.position.x+2.7f, GameObject.Find ("CR").transform.position.y-0.2f, GameObject.Find ("CR").transform.position.z+5);
	
		//female hands
		if(this.gameObject.name=="LeftHandFemale")
			transform.position = new Vector3 (GameObject.Find ("CL").transform.position.x+1.71f, GameObject.Find ("CL").transform.position.y+1.74f, GameObject.Find ("CL").transform.position.z-5.70f);
		if( this.gameObject.name=="RightHandFemale")
			transform.position = new Vector3 (GameObject.Find ("CR").transform.position.x+1.75f, GameObject.Find ("CR").transform.position.y-1.6f, GameObject.Find ("CR").transform.position.z+5.2f);

	}
}
