/*
-----------------------
UDP Object
-----------------------
Code pulled from here and modified
 [url]http://msdn.microsoft.com/de-de/library/bb979228.aspx#ID0E3BAC[/url]

Unity3D to MATLAB UDP communication 

Modified by: Sandra Fang 
2016
*/
using UnityEngine;
using System.Collections;

using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class ReadUDP : MonoBehaviour
{

	//Ports
	public int portLocal = 8000;
	public int portLocal1 = 8001;

	// Create necessary UdpClient objects
	UdpClient client;
	UdpClient client1;

	// Receiving Thread
	Thread receiveThread;
	private bool onReceive = true;
	public double dataChanged;
	public double timechanged;
	public double baselineTBR;

    // private ColorCtrl colorCtrl;

	// start from Unity3d
	public void Start ()
	{
		init ();
	}


	// Initialization code
	private void init ()
	{
		// Initialize (seen in comments window)
		// print ("UDP Object init()");
		
		// Create local client
		client = new UdpClient (portLocal);
		client1 = new UdpClient (portLocal1);

        // GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
        // if (gameControllerObject != null)
        // {
        //     colorCtrl = gameControllerObject.GetComponent <ColorCtrl>();
        // }
        // if (colorCtrl == null)
        // {
        //     Debug.Log("Cannot find 'ColorCtrl' script");
        // }

		// local endpoint define (where messages are received)
		// Create a new thread for reception of incoming messages
		receiveThread = new Thread (new ThreadStart (ReceiveData));
		receiveThread.IsBackground = true;
		receiveThread.Start ();
        print ("UDP is listening");
	}

	// Receive data, update packets received
	private  void ReceiveData ()
	{
		do 
		{
			// print("ReceiveData");
			try 
			{
				// print("In try");
				IPEndPoint IP8000 = new IPEndPoint (IPAddress.Any, portLocal);
				IPEndPoint IP8001 = new IPEndPoint (IPAddress.Any, portLocal1);
				// print("IP connect");
				byte[] data = client.Receive (ref IP8000);
				byte[] data1 = client1.Receive (ref IP8001);
				// print("After IP connect");
				// print("> " + BitConverter.ToString(data));
				print(">> " + BitConverter.ToDouble(data, 0));
				print("TT " + BitConverter.ToDouble(data1, 0));
				dataChanged = BitConverter.ToDouble(data, 0);
				timechanged = BitConverter.ToDouble(data1,0);
				// print("Data changed already");
			}
			catch (Exception err) 
			{
				print (err.ToString ());
				// OnDisable();
			}
		}while (onReceive);
	}

	//Prevent crashes - close clients and threads properly!
	void OnDisable ()
	{ 
		onReceive = false;
		if (receiveThread != null)
			receiveThread.Abort ();
		client.Close ();
		client1.Close ();			
		print("close all client");
	}
}