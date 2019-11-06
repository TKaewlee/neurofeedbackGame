using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;               // ui objects

public class ColorCtrl : MonoBehaviour {

	public Material matObject;
    public float Beta = 1.0f;
    public float Theta = 1.0f;
    public float a;
	//private float baseAlpha = 10.0f;
	// public KeyCode changeCol;
	private Read2UDP read2UDP;

	private static List<float> dataAvgChanged = new List<float>();
	private static List<float> betaDataAvgChanged = new List<float>();
    private static List<float> thetaDataAvgChanged = new List<float>();
    private float betaDataAvg;
    private float thetaDataAvg;
    private float tbrDataAvg;
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
            Beta = read2UDP.betaDataTempChanged;
            Theta = read2UDP.thetaDataTempChanged;
            betaDataAvgChanged.Add(Beta);
            thetaDataAvgChanged.Add(Theta);
			if(timeController.modeName == "Threshold")
			{
				// a = (Alpha-baseline)/(difficult*(threshold-baseline)) + offset;
				/*a = 1-((Alpha - baseAlpha)/10);
				if(a < 0){a = 0;} else if (a > 1){a = 1;}
				matObject.color = new Color(0f, 0f, 0f, a);*/
			}
		}
		else
		{	
			if(timeController.isFinish)
			{
                betaDataAvg = betaDataAvgChanged.Average();
                thetaDataAvg = thetaDataAvgChanged.Average();
                tbrDataAvg = thetaDataAvg / thetaDataAvg;
				if(timeController.modeName == "Baseline")
				{
					if(baselineInputField.text != "")
					{
						baselineSetText.text = "Baseline     : Manaully";
						GameControl.currentBaselineAvg = float.Parse(baselineInputField.text);
					}
					else
					{
						baselineSetText.text = "Baseline     : " + tbrDataAvg.ToString();
						GameControl.currentBaselineAvg = tbrDataAvg;
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
						thresholdSetText.text = "Threshold : " + tbrDataAvg.ToString();
						GameControl.currentThresholdAvg = tbrDataAvg;
					}
				}

				Read2UDP.tempData["average"] = tbrDataAvg.ToString();

                betaDataAvgChanged.Clear();
                thetaDataAvgChanged.Clear();
                timeController.isFinish = false;	
			}
		}
	}
}
