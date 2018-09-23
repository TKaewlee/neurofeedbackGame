using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;               // ui objects

public class ColorCtrl : MonoBehaviour {

	public Material matObject;
	public float Alpha = 1.0f, a;
	private float baseAlpha = 10.0f;
	// public KeyCode changeCol;
	private Read2UDP read2UDP;

	private static List<float> dataAvgChanged = new List<float>();
	private float dataAvg;
	public Text baselineSetText;
	public InputField baselineInputField;
	public Text thresholdSetText;
	public InputField thresholdInputField;

	private bool isSaved;

	// Use this for initialization
	void Start () {
		matObject.color = new Color(0f, 0f, 0f, 255f);
		//matObject.color = Color.black;	
		isSaved = false;
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
		if(timeController.isSaving)
		{
			Alpha = read2UDP.dataTempChanged;
			dataAvgChanged.Add(Alpha);
			if(timeController.modeName == "Threshold")
			{
				// a = (Alpha-baseline)/(difficult*(threshold-baseline)) + offset;
				a = 1-((Alpha - baseAlpha)/10);
				if(a < 0){a = 0;} else if (a > 1){a = 1;}
				matObject.color = new Color(0f, 0f, 0f, a);
			}
		}
		else
		{	
			if(timeController.isFinish)
			{
				dataAvg = dataAvgChanged.Average();
				if(timeController.modeName == "Baseline")
				{
					if(baselineInputField.text != "")
					{
						baselineSetText.text = "Baseline     : Manaully";
						GameControl.currentBaselineAvg = float.Parse(baselineInputField.text);
					}
					else
					{
						baselineSetText.text = "Baseline     : " + dataAvg.ToString();
						GameControl.currentBaselineAvg = dataAvg;
					}
					
				}
				else
				{
					if(thresholdInputField.text != "")
					{
						thresholdSetText.text = "Threshold : Manaully";
						GameControl.currentThresholdAvg = float.Parse(thresholdInputField.text);
					}
					else
					{
						thresholdSetText.text = "Threshold : " + dataAvg.ToString();
						GameControl.currentThresholdAvg = dataAvg;
					}
				}

				Read2UDP.tempData["average"] = dataAvg.ToString();
				
				dataAvgChanged.Clear();
				timeController.isFinish = false;	
			}
		}
	}
}
