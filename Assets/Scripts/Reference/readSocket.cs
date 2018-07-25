using UnityEngine;
using System.Collections;
// using System.Net;
using System.Net.Sockets;
// using System.Linq;
using System;
using System.IO;
// using System.Text;

public class readSocket : MonoBehaviour {
    // Use this for initialization
    TcpListener listener;
    String msg;
    private ColorCtrl colorCtrl;

    void Start () {
        listener = new TcpListener (55001);
        listener.Start ();
        print ("is listening");
        GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
        if (gameControllerObject != null)
        {
            colorCtrl = gameControllerObject.GetComponent <ColorCtrl>();
        }
        if (colorCtrl == null)
        {
            Debug.Log("Cannot find 'ColorCtrl' script");
        }
    }
    // Update is called once per frame
    void Update () {
        if (!listener.Pending ())
        {

        } 
        else 
        {
            print ("socket comes");
            TcpClient client = listener.AcceptTcpClient ();
            NetworkStream ns = client.GetStream ();
            StreamReader reader = new StreamReader (ns);
            msg = reader.ReadToEnd();
            Debug.Log(msg);
            float value = float.Parse(msg);
            print (msg);
            // colorCtrl.AdjustAlpha(value);
        }
    }
}