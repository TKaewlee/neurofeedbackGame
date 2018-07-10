using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorCtrl : MonoBehaviour {

	public Material matObject;
	public float Alpha = 1.0f;
	public KeyCode changeCol;
	private Read2UDP read2UDP;
	
	// Use this for initialization
	void Start () {
		matObject.color = Color.black;	

        GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
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
		Alpha = (float)read2UDP.dataChanged;
		matObject.color = new Color(0f, 0f, 0f, Alpha);
	}

	// public void AdjustAlpha (float newAlpha) {
	// 	// Alpha = newAlpha;
		
	// }
}
