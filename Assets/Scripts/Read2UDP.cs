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
using System.Collections.Generic;
using System.Linq;

public class Read2UDP : MonoBehaviour
{

	//Ports
	public int portLocal = 8000; //Beta
	public int portLocal1 = 8001; //Time
    public int portLocal2 = 8002; //Theta

	// Create necessary UdpClient objects
	UdpClient client;
	UdpClient client1;
    UdpClient client2;

	// Receiving Thread
	Thread receiveThread;
	private bool onRecieve = true;

	private float timeshift;
	public float betaDataTempChanged;
	public float timeTempChanged;
    public float thetaDataTempChanged;
	private static List<float> betaDataChanged = new List<float>();
	private static List<float> timeChanged = new List<float>();
    private static List<float> thetaDataChanged = new List<float>();
	public static Dictionary<string, string> tempData = new Dictionary<string, string>();
    


    // start from Unity3d
    public void Start ()
	{
        print("Read2UDP start");
		Init ();
	}


	// Initialization code
	private void Init ()
	{
		// Initialize (seen in comments window)
		// print ("UDP Object init()");
		
		// Create local client
		client = new UdpClient (portLocal);
		client1 = new UdpClient (portLocal1);
        client2 = new UdpClient(portLocal2);

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
				IPEndPoint IP8000 = new IPEndPoint(IPAddress.Any, portLocal);
				IPEndPoint IP8001 = new IPEndPoint(IPAddress.Any, portLocal1);
                IPEndPoint IP8002 = new IPEndPoint(IPAddress.Any, portLocal2);
                byte[] data = client.Receive(ref IP8000);
				byte[] data1 = client1.Receive(ref IP8001);
                byte[] data2 = client2.Receive(ref IP8002);
                betaDataTempChanged = (float)BitConverter.ToDouble(data, 0);
				timeTempChanged = (float)BitConverter.ToDouble(data1, 0);
                thetaDataTempChanged = (float)BitConverter.ToDouble(data2, 0);
                



                if (timeController.isStart == true && timeController.isFixationSet == false)
				{
					tempData["date"] = DateTime.Now.ToString();
					if(timeController.isTimeSet == true)
					{
						tempData["timeset"] = timeController.timeSet.ToString();
					}
					else
					{
						tempData["timeset"] = "None";
					}
					if(timeController.isFixation == true)
					{
						tempData["timefixation"] = timeController.timeFixation.ToString();
					}
					else
					{
						tempData["timefixation"] = "None";
					}					
					tempData["start"] = timeTempChanged.ToString("f2");
					timeController.isStart = false;
				}
				
				if (timeTempChanged >= timeshift)
				{
                    timeshift = timeTempChanged + 0.02f; //0.02f: 1s receives 50 data, if deleted, 1s receives 256data
					if (timeController.isSaving)
					{
						timeChanged.Add(timeTempChanged);
						betaDataChanged.Add(betaDataTempChanged);
                        thetaDataChanged.Add(thetaDataTempChanged);
                    }
				}
                
                if (timeController.isOnSave == true)
				{
					if(timeController.isFinish == false)
					{
                        tempData["stop"] = timeTempChanged.ToString("f2");
                        //tempData["time"] = DataController.GameDataController.getAppendString(timeChanged); //save data
                        //tempData["beta"] = DataController.GameDataController.getAppendString(betaDataChanged);
                        //tempData["theta"] = DataController.GameDataController.getAppendString(thetaDataChanged);

                        print(">> Start Saving");
						DataController.GameDataController.updateGamePath();
						DataController.GameDataController.getData();
						DataController.GameDataController.writeData();
						DataController.GameDataController.clearData();

						timeChanged.Clear();
						betaDataChanged.Clear();
                        thetaDataChanged.Clear();
                        timeController.isOnSave = false;
						tempData.Clear();
					}
				}
			}
            catch (Exception err) {
				print (err.ToString ());	
			}
		}while (onRecieve);
	}


	//Prevent crashes - close clients and threads properly!
	void OnDisable ()
	{ 
		onRecieve = false;
		if (receiveThread != null)
			receiveThread.Abort ();
		client.Close ();
		client1.Close ();
        client2.Close();
		print("close all");
	}
}