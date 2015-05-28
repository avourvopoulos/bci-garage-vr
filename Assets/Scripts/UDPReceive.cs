using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

public class UDPReceive : MonoBehaviour {
	
 // receiving Thread
    Thread receiveThread; 
 // udpclient object
    UdpClient client; 
	

	string datatype;
	string device;
	string arrow;
	string transformationtype;
	float udpx, udpy, udpz, udpw;
 
    // public
    int port; // define > init
	private string portField = "1204";
	public static bool isConnected=false;

	public static bool lefthand = false;
	public static bool righthand = false;
	int data;
		
    void Start()
    {	
		port = int.Parse(portField);			
		init();		
		isConnected = true;
    }
	

    public void init()
    {
        // Local endpoint define (where messages are received).
        // Create a new thread to receive incoming messages.
        receiveThread = new Thread(new ThreadStart(ReceiveData));
        receiveThread.IsBackground = true;	
        receiveThread.Start();
    }
 

    // receive thread 
    public void ReceiveData() 
    {	
		port = int.Parse(portField);
		
		client = new UdpClient(port);
		print("Started UDP - port: " +port);
		
        while (isConnected)
        {
            try 
            { 
              	 // receive Bytes from 127.0.0.1
					IPEndPoint IP = new IPEndPoint(IPAddress.Any, 0);
					
        	        byte[] udpdata = client.Receive(ref IP);
					//client.EnableBroadcast = true;

                //  UTF8 encoding in the text format.
					string data = Encoding.UTF8.GetString(udpdata);	
				
			
					//PROTOCOL
					if(data!=String.Empty)
					{
						TranslateData(data);
					}
				
            }//try
            catch (Exception err) 
            {
                print(err.ToString());
            }
			
        }//while true		
	 
    }//ReceiveData
	
	
	void TranslateData(string n_data)
	{
				//	[$]<data  type> , [$$]<device> , [$$$]<joint> , <transformation> , <param_1> , <param_2> , .... , <param_N>
		
				// Decompose incoming data based on the protocol rules
//		if (n_data.Contains ("[$]")) {
//						string[] separators = {"[$]","[$$]","[$$$]",",",";"," "};      
//						string[] words = n_data.Split (separators, StringSplitOptions.RemoveEmptyEntries);		 	         					
//						datatype = words [0];
//						device = words [1];
//						arrow = words [2];
//						transformationtype = words [3];
//
//						//button, openvibe, leftarrow/rightarrow, event, 0/1
//						if (arrow == "leftarrow" && words [4] == "1") {
//								//words[4]//value
//								SettingsScript.lbtn = true;
//								SettingsScript.rbtn = false;
//							UnityEngine.Debug.Log("leftarrow");
//						} else if (arrow == "rightarrow" && words [4] == "1") {
//	
//								SettingsScript.lbtn = false;
//								SettingsScript.rbtn = true;
//								UnityEngine.Debug.Log("rightarrow");
//						} else {
//
//								SettingsScript.lbtn = false;
//								SettingsScript.rbtn = false;
//						}
//				} else {

						bool isNumeric = int.TryParse (n_data, out data);

						//		if(isNumeric)
						//		{
						if (double.Parse (n_data) < 0) {
								lefthand = true;
								righthand = false;
						} else if (double.Parse (n_data) > 0) {
								lefthand = false;
								righthand = true;
						} else {
								lefthand = false;
								righthand = false;
						}
						//}
//				}
		
//		UnityEngine.Debug.Log ("RAW: "+n_data);
//
//		if(lefthand)
//			UnityEngine.Debug.Log ("left");
//		else if(righthand)
//			UnityEngine.Debug.Log ("right");
//		else
//			UnityEngine.Debug.Log ("---");


	}//end of TranslateData()
	

	
	void OnDisable() 
	{ 
		if(isConnected)
    	{
			receiveThread.Abort();
			client.Close();
			isConnected = false;
			receiveThread.Abort();
			client.Close();
			print("Stop UDP");
		}
	} 	
	
	void OnApplicationQuit () 
	{
		if(isConnected)
	    {
		  receiveThread.Abort();
          client.Close();
			isConnected = false;
			receiveThread.Abort();
			client.Close();
			print("Stop UDP");
		}
    }
	

}