﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class ColorCtrl : MonoBehaviour {

	public Material matObject;
	public float Alpha = 1.0f;
	public float baseAlpha = 1.0f;
	public KeyCode changeCol;
	private Read2UDP read2UDP;

	private static List<float> dataAvgChanged = new List<float>();
	private

	// Use this for initialization
	void Start () {
		matObject.color = Color.black;	

        GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("UDPReciever");
        if (gameControllerObject != null)
        {
            read2UDP = gameControllerObject.GetComponent <Read2UDP>();
        }
        if (read2UDP == null)
        {
            Debug.Log("Cannot find 'read2UDP' script");
        }
	}
	
	// Update is called once per frame
	void Update () {
		Alpha = read2UDP.dataTempChanged;
		// dataAvgChanged.Add(Alpha);
		// if(timeController.isOnSave)
		// {
		// 	print(dataAvgChanged.Average());
		// }
		matObject.color = new Color(0f, 0f, 0f, 1-(baseAlpha-Alpha));
	}

	// public void AdjustAlpha (float newAlpha) {
	// 	// Alpha = newAlpha;
		
	// }
}
