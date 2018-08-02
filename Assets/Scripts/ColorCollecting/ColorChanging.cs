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
	public float a,i;

    public float baseline;
    public float threshold;
	public GameObject fallBlack;public Material blackMat;
	public GameObject fallBlue;public Material blueMat;
	public GameObject fallCyan;public Material cyanMat;
	public GameObject fallGreen;public Material greenMat;
	public GameObject fallGrey;public Material greyMat;
	public GameObject fallMagenta;public Material magentaMat;
	public GameObject fallOrange;public Material orangeMat;
	public GameObject fallSky;public Material skyMat;
	public GameObject fallRed;public Material redMat;
	public GameObject fallWhite;public Material whiteMat;
	public GameObject fallYellow;public Material yellowMat;
	public float hardFactor,helpFactor;

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

		i=0.2f; // initial smooth color changing

		hardFactor=1;
		helpFactor=0;
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
            a=((Alpha-baseline)/(hardFactor*(threshold-baseline)))+helpFactor;
            if(a>i)
            {
                while(a>i)
                {
                    i+=0.01f;
                    col=blackMat.color;    col.a=(float)i;     blackMat.color=col;
					col=blueMat.color;     col.a=(float)i;     blueMat.color=col;
					col=cyanMat.color;     col.a=(float)i;     cyanMat.color=col;
					col=greenMat.color;    col.a=(float)i;     greenMat.color=col;
					col=greyMat.color;     col.a=(float)i;     greyMat.color=col;
					col=magentaMat.color;  col.a=(float)i;     magentaMat.color=col;
					col=orangeMat.color;   col.a=(float)i;     orangeMat.color=col;
					col=skyMat.color;      col.a=(float)i;     skyMat.color=col;
					col=redMat.color;      col.a=(float)i;     redMat.color=col;
					col=whiteMat.color;    col.a=(float)i;     whiteMat.color=col;
					col=yellowMat.color;   col.a=(float)i;     yellowMat.color=col;
                }
                print("Intensity UP");
            }
            else if(a<i)
            {
                while(a<i)
                {
                    i-=0.01f;
                    col=blackMat.color;    col.a=(float)i;     blackMat.color=col;
					col=blueMat.color;     col.a=(float)i;     blueMat.color=col;
					col=cyanMat.color;     col.a=(float)i;     cyanMat.color=col;
					col=greenMat.color;    col.a=(float)i;     greenMat.color=col;
					col=greyMat.color;     col.a=(float)i;     greyMat.color=col;
					col=magentaMat.color;  col.a=(float)i;     magentaMat.color=col;
					col=orangeMat.color;   col.a=(float)i;     orangeMat.color=col;
					col=skyMat.color;      col.a=(float)i;     skyMat.color=col;
					col=redMat.color;      col.a=(float)i;     redMat.color=col;
					col=whiteMat.color;    col.a=(float)i;     whiteMat.color=col;
					col=yellowMat.color;   col.a=(float)i;     yellowMat.color=col;
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
