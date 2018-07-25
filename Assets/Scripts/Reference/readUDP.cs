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

public class readUDP : MonoBehaviour
{

	//local host
	// public string IP = "192.168.15.49";

	//Ports
	public int portLocal = 8000;
	// public int portRemote = 25001;

	// Create necessary UdpClient objects
	UdpClient client;
	IPEndPoint remoteEndPoint;

	// Receive Number
	public uint numberReceived;

	// Receiving Thread
	Thread receiveThread;
	// Message to be sent
	// string strMessageSend = "";
	
	private bool onRecieve = true;

	// info strings
	// public string lastReceivedUDPPacket = "";
	// public string allReceivedUDPPackets = "";
	// clear this from time to time!

    private ColorCtrl colorCtrl;

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

		// Create remote endpoint (to Matlab) 
		// remoteEndPoint = new IPEndPoint (IPAddress.Parse (IP), portRemote);
		
		// Create local client
		client = new UdpClient (portLocal);

        GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
        if (gameControllerObject != null)
        {
            colorCtrl = gameControllerObject.GetComponent <ColorCtrl>();
        }
        if (colorCtrl == null)
        {
            Debug.Log("Cannot find 'ColorCtrl' script");
        }
		// 	//IPEndPoint object will allow us to read datagrams sent from any source.
		// 	IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);

		// local endpoint define (where messages are received)
		// Create a new thread for reception of incoming messages
		receiveThread = new Thread (
			new ThreadStart (ReceiveData));
		receiveThread.IsBackground = true;
		receiveThread.Start ();
        print ("UDP is listening");		
	}

	// Receive data, update packets received
	private  void ReceiveData ()
	{

		do {
			try {
				IPEndPoint anyIP = new IPEndPoint (IPAddress.Any, 0);
				// print("data");
				byte[] data = client.Receive (ref anyIP);
				print("> " + BitConverter.ToString(data));
				print(">> " + BitConverter.ToDouble(data, 0));
				// colorCtrl.AdjustAlpha((float)BitConverter.ToDouble(data, 0));
                // string text = Encoding.Default.GetString (data);
				// numberReceived = UInt32.Parse(text);
				// print (">> " + Convert.ToByte(numberReceived));
				// lastReceivedUDPPacket = text;
				// allReceivedUDPPackets = allReceivedUDPPackets + text;
			} catch (Exception err) {
				print (err.ToString ());
			}
		} while (onRecieve);
	}

	// Send data
	private void sendData (string message)
	{
		try {
			byte[] data = Encoding.UTF8.GetBytes (message);
			client.Send (data, data.Length, remoteEndPoint);
			
		} catch (Exception err) {
			print (err.ToString ());
		}
	}

	// getLatestUDPPacket, clears all previous packets
	// public string getLatestUDPPacket ()
	// {
	// 	allReceivedUDPPackets = "";
	// 	return lastReceivedUDPPacket;
	// }

	//Prevent crashes - close clients and threads properly!
	void OnDisable ()
	{ 
		onRecieve = false;
		if (receiveThread != null)
			receiveThread.Abort (); 
		client.Close ();
		print("All closed");
	}

}