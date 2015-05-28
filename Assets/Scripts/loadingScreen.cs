using UnityEngine;
using System.Collections;

public class loadingScreen : MonoBehaviour {

	public GUIStyle loadstyle;

	// Use this for initialization
	void Start () 
	{
		StartCoroutine(loadlevel());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator loadlevel() {
		yield return new WaitForSeconds(1);
		Application.LoadLevel(1);
	}

	void OnGUI() 
	{	
		GUI.Label(new Rect(Screen.width/2-160, Screen.height/2-130, 353, 263), "", loadstyle);
	}

}
