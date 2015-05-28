using UnityEngine;
using System.Collections;

public class LookTarget : MonoBehaviour {

	public Transform target;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{

		if (GameObject.Find ("palm") != null)
			target = GameObject.Find ("palm").transform;
		else
			target = this.transform;

		//
		Vector3 targetPostition = target.position;
//		targetPostition.x = 0.0f;
//		targetPostition.y = 0.0f;
//		targetPostition.z = 0.0f;
		this.transform.LookAt(targetPostition);

	
	}
}
