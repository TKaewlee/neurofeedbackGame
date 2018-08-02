using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
public class barGague : MonoBehaviour {
	
	public Transform gaugeBar;
	public Slider gaugeFill;
	public float currentGauge;
	public float maxGauge;
		private Read2UDP read2UDP;
	public float Alpha = 1.0f;
	public float baseAlpha = 1.0f;
	private static List<float> dataAvgChanged = new List<float>();
	private float dataAvg;
	private bool isSaved;
	public static Dictionary<string, string> tempCalibation = new Dictionary<string, string>();
	public float a,i=0.0f;

    public float baseline;
    public float threshold;

	// Use this for initialization
	void Start () {
		baseline = GameControl.currentBaselineAvg;
        threshold = GameControl.currentThresholdAvg;
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
            a=1-((baseline-Alpha)/threshold);
        }
        Read2UDP.tempData["average"] = dataAvg.ToString();
        dataAvgChanged.Clear();
        timeController.isSetAvg = false;
		controlGauge();
	}

	public void ChangeGauge(int amount){
		currentGauge += amount;
		currentGauge = Mathf.Clamp(currentGauge, 0, maxGauge);

		gaugeFill.value = currentGauge / maxGauge;
	}
	private void controlGauge(){
		//float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal");
		float controlThrow = a;
		ChangeGauge((int)controlThrow);
	}
}
