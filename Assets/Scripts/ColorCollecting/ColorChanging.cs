using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;               // ui objects

public class ColorChanging : MonoBehaviour {

	public Material matObject;
	public float Alpha = 1.0f;
	public float baseAlpha = 1.0f;
	// public KeyCode changeCol;
	private Read2UDP read2UDP;

	private static List<float> dataAvgChanged = new List<float>();
	private float dataAvg;
	public Text baselineSetText;
	public Text thresholdSetText;

	private bool isSaved;

	public static Dictionary<string, string> tempCalibation = new Dictionary<string, string>();
	private Color col;
	public float a,i=0.0f;

    public float baseline;
    public float threshold;

	// Use this for initialization
	void Start () {
        baseline = GameControl.currentBaselineAvg;
        threshold = GameControl.currentThresholdAvg;
        
        baselineSetText.text = baseline.ToString();
        thresholdSetText.text = threshold.ToString();

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
		// }
		// else
		// {	
		// 	if(timeController.isSetAvg)
		// 	{
		// 		dataAvg = dataAvgChanged.Average();
		// 		if(timeController.modeName == "Baseline")
		// 		{
		// 			baselineSetText.text = dataAvg.ToString();
		// 		}
		// 		else
		// 		{
		// 			thresholdSetText.text = dataAvg.ToString();
					// matObject.color = new Color(0f, 0f, 0f, 1-(baseAlpha-Alpha));
    


            //smooth color changing
            a=1-((baseline-Alpha)/threshold);
            if(a>=i)
            {
                while(a>i)
                {
                    i+=0.01f;
                    col=matObject.color;
                    col.a=(float)i;
                    matObject.color=col;
                }
                print("Intensity UP");
            }
            else if(a<i)
            {
                while(a<i)
                {
                    i-=0.01f;
                    col=matObject.color;
                    col.a=(float)i;
                    matObject.color=col;
                }
                print("Intensity DOWN");
            }
        }

        Read2UDP.tempData["average"] = dataAvg.ToString();

        dataAvgChanged.Clear();
        timeController.isSetAvg = false;	
    }
		// }
		// if(timeController.isConfirmExit)
		// {
		// 	print("saved specific");
		// 	timeController.isConfirmExit = false;
		// }
	// }
}
