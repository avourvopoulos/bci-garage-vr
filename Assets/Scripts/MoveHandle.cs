using UnityEngine;
using System.Collections;

public class MoveHandle : MonoBehaviour {

	public static float rot0, rot1;

	public static bool lefthand, righthand = false;

	public GameObject leftarrow, rightarrow;

	public static float handleSpeed = 400;
	
	public Transform targetL;
	Quaternion initRotationL;

	public static float angle, angle0, angle1;

	// Use this for initialization
	void Start () {
	
	//	if(this.gameObject.name=="CCL")
			rot0 = rot1 = Mathf.Abs (GameObject.Find ("CCL").transform.rotation.x);
	//	if(this.gameObject.name=="CCR")
	//		rot0 = rot1 = Mathf.Abs (GameObject.Find ("CCR").transform.rotation.x);

		InvokeRepeating("Sampling", 1, 1F);//sample every second

		leftarrow.SetActive (false);
		rightarrow.SetActive (false);

		//init handle speed
		handleSpeed = float.Parse (SettingsScript.handleSpeedGUI); 
		initRotationL = GameObject.Find ("CCL").transform.rotation;
	}


	// Update is called once per frame
	void Update () {

		if (GameObject.Find ("palm") != null)
						targetL = GameObject.Find ("palm").transform;
				else
						targetL = targetL;


		//set handle speed realtime
		handleSpeed = float.Parse (SettingsScript.handleSpeedGUI); 

		if (SettingsScript.freemode)
		{
			if((GrabbingHand.closedLeft && HandCollision.grabbed))// && !DoorScript.doorOpen)
			{ 
				float xDiff = targetL.transform.position.z - GameObject.Find ("BoxL").transform.position.z;
				float yDiff = targetL.transform.position.y - GameObject.Find ("BoxL").transform.position.y;		 
				angle = (float)Mathf.Atan2(yDiff, xDiff) * 180.0f / (float)Mathf.PI;
				angle1 = Mathf.Abs (angle);
//				Debug.Log(angle);

				GameObject.Find ("CCL").transform.rotation = Quaternion.Euler(-angle + float.Parse(SettingsScript.angleOffset), initRotationL.eulerAngles.y, initRotationL.eulerAngles.z);

				rot1 = Mathf.Abs(GameObject.Find ("CCL").transform.rotation.x);
				lefthand = true;
				righthand = false;

			}
			else if((GrabbingHand.closedRight && HandCollision.grabbed))// && !DoorScript.doorOpen)
			{

				float xDiff = targetL.transform.position.z - GameObject.Find ("BoxR").transform.position.z;
				float yDiff = targetL.transform.position.y - GameObject.Find ("BoxR").transform.position.y;		 
				angle = (float)Mathf.Atan2(yDiff, xDiff) * 180.0f / (float)Mathf.PI;
				angle1 = Mathf.Abs (angle);
				GameObject.Find ("CCR").transform.rotation = Quaternion.Euler(-angle + float.Parse(SettingsScript.angleOffset), initRotationL.eulerAngles.y, initRotationL.eulerAngles.z);

				rot1 = Mathf.Abs(GameObject.Find ("CCR").transform.rotation.x);
				lefthand = false;
				righthand = true;
			}
			else 
			{
				rot0 = rot1 = 0;
			}

		}

		if (SettingsScript.training)
		{

			if((Input.GetKey(KeyCode.LeftArrow) || SettingsScript.lbtn) && !DoorScript.doorOpen)
			{
				GameObject.Find ("CCL").transform.Rotate(Vector3.right * Time.deltaTime *handleSpeed, Space.World);
				rot1 = Mathf.Abs(GameObject.Find ("CCL").transform.rotation.x);
				lefthand = true;
				righthand = false;
				leftarrow.SetActive (true);
				rightarrow.SetActive (false);
			}
			else if((Input.GetKey(KeyCode.RightArrow) || SettingsScript.rbtn) && !DoorScript.doorOpen)
			{
				GameObject.Find ("CCR").transform.Rotate(Vector3.right * Time.deltaTime *handleSpeed, Space.World);
				rot1 = Mathf.Abs(GameObject.Find ("CCR").transform.rotation.x);
				lefthand = false;
				righthand = true;
				leftarrow.SetActive (false);
				rightarrow.SetActive (true);
			}
			else 
			{
				rot0 = rot1 = 0;
				leftarrow.SetActive (false);
				rightarrow.SetActive (false);
			}
		}

		if (SettingsScript.online)
		{
			if(SettingsScript.lbtn)
			{
				leftarrow.SetActive (true);
				rightarrow.SetActive (false);
			}
			else if(SettingsScript.rbtn)
			{
				leftarrow.SetActive (false);
				rightarrow.SetActive (true);
			}
			else
			{
				leftarrow.SetActive (false);
				rightarrow.SetActive (false);
			}


			if(((SettingsScript.lbtn && SettingsScript.lda2)|| UDPReceive.lefthand) && !DoorScript.doorOpen)
			{
				GameObject.Find ("CCL").transform.Rotate(Vector3.right * Time.deltaTime *handleSpeed, Space.World);
				rot1 = Mathf.Abs(GameObject.Find ("CCL").transform.rotation.x);
				lefthand = true;
				righthand = false;
			}
			else if(((SettingsScript.lda1 && SettingsScript.rbtn)|| UDPReceive.righthand) && !DoorScript.doorOpen)
			{
				GameObject.Find ("CCR").transform.Rotate(Vector3.right * Time.deltaTime *handleSpeed, Space.World);
				rot1 = Mathf.Abs(GameObject.Find ("CCR").transform.rotation.x);
				lefthand = false;
				righthand = true;
			}
			else 
			{
				rot0 = rot1 = 0;
			}


		}
	
	//	Debug.Log (rot0+" : "+rot1);
	//	Debug.Log (getDirection());
	}

	void Sampling()
	{
		if(Input.GetKey(KeyCode.LeftArrow))
			rot0 = Mathf.Abs (GameObject.Find ("CCL").transform.rotation.x);
		if(Input.GetKey(KeyCode.RightArrow))
			rot0 = Mathf.Abs (GameObject.Find ("CCR").transform.rotation.x);

		angle0 = Mathf.Abs (angle);
	}

	public static string getDirection()
	{
		if(rot0<rot1)
			return "up";
		if(rot0>rot1)
			return "down";
		else
			return "";
	}

	public static string getRotation()
	{
		if(angle0<angle1)
			return "fwd";
		if(angle0>angle1)
			return "bwd";
		else
			return "";
	}

	public static string getAction()
	{
		if(rot0!=rot1)
			return "onmove";
		if(rot0==rot1)
			return "halt";
		else
			return "";
	}

}
