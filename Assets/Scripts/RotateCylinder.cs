using UnityEngine;
using System.Collections;

public class RotateCylinder : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		//roll up
		if((Input.GetKey(KeyCode.UpArrow) || MoveHandle.getDirection ()== "up")&&(GameObject.Find("Door").transform.position.y < DoorScript.max) )
		{
			transform.Rotate(Vector3.left * Time.deltaTime*50, Space.World);
		}

		//roll down
		if((Input.GetKey(KeyCode.DownArrow) || MoveHandle.getDirection ()== "down") &&(GameObject.Find("Door").transform.position.y > DoorScript.min))
		{
			transform.Rotate(Vector3.right * Time.deltaTime*50, Space.World);
		}

	
	}
}
