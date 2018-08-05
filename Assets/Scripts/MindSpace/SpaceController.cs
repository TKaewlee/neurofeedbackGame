using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;               // ui objects

public class SpaceController : MonoBehaviour {
	
	public GameObject[] hazards;
	public GameObject[] rewards;
	public Vector3 SpawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;
	public float distanceCount;

	private int score;
	public Text scoreText;
	
	private int distance;
	public Text[] distanceTexts;
	public CanvasGroup distanceCanvas;

	public CanvasGroup slideCanvas;
	public Slider gaugeFill;

	public Material[] matObject;

	private Read2UDP read2UDP;
	private float Alpha = 1.0f;

	public float a;

    public float baseline;
    public float threshold;

	public InputField difficultInputField;
	public InputField offsetInputField;
	private float difficult;
	private float offset;

	void Start ()
	{
		baseline = GameControl.currentBaselineAvg;
        threshold = GameControl.currentThresholdAvg;
		
        GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("UDPReciever");
        if (gameControllerObject != null)
        {
            read2UDP = gameControllerObject.GetComponent <Read2UDP>();
        }
        if (read2UDP == null)
        {
            Debug.Log("Cannot find 'read2UDP' script");
        }

		for (int i = 0; i < matObject.Length; i++)
		{
			matObject[i].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
			matObject[i].SetFloat("_Metallic", 1.0f);
		}

		score = 0;
		distance = 350;
		
		UpdateScore ();
		// UpdateDistance ();
		StartCoroutine (SpawnWaves ());
		// StartCoroutine (RewardWaves ());
	}

	void Update ()
	{
		if(timeController.isContinue)
		{
			if(timeController.isStart)
			{
				if(difficultInputField.text != "")
				{
					difficult = float.Parse(difficultInputField.text);
				}
				else
				{
					difficult = 1;
				}

				if(offsetInputField.text != "")
				{
					offset = float.Parse(offsetInputField.text);
				}
				else
				{
					offset = 0;
				}
				Read2UDP.tempData["difficult"] = difficult.ToString();
				Read2UDP.tempData["offset"] = offset.ToString();
				Read2UDP.tempData["baseline"] = GameControl.currentBaselineAvg.ToString();
				Read2UDP.tempData["threshold"] = GameControl.currentThresholdAvg.ToString();
				// timeController.isStart = false;
			}


			if(timeController.modeName == "without NF")
			{
				distanceCanvas.alpha = 1;
				AddDistance();
			}
			else 
			{
				Alpha = read2UDP.dataTempChanged;
				// dataAvgChanged.Add(Alpha);
				a = (Alpha-baseline)/(difficult*(threshold-baseline)) + offset;				
				if(timeController.modeName == "NF with slider")
				{
					slideCanvas.alpha = 1;
					// currentGauge += (int)a;
					// currentGauge = Mathf.Clamp(currentGauge, 0, maxGauge);
					gaugeFill.value = a; //currentGauge / maxGauge;
				}
				else if(timeController.modeName == "NF with moving object")
				{
					for (int i = 0; i < matObject.Length; i++)
					{
						matObject[i].color = new Color(1f, 1f, 1f, a);
						matObject[i].SetFloat("_Metallic", a);
					}
				}
			}
		}
		else
		{
			score = 0;
			distance = 350;
			UpdateScore ();
			AddDistance();
			distanceCanvas.alpha = 0;
			slideCanvas.alpha = 0;
		}
	}

	// IEnumerator RewardWaves ()
	// {
	// 	// yield return new WaitForSeconds (startWait);
	// 	while((distance / 35) % 10 == 0)
	// 	{
	// 		// for (int i = 0; i < hazardCount; i++)
	// 		// {
	// 			// GameObject hazard = hazards [Random.Range (0, hazards.Length)];
	// 		// 	Vector3 spawnPosition = new Vector3 (Random.Range (-SpawnValues.x, SpawnValues.x), SpawnValues.y, SpawnValues.z);
	// 		// 	Quaternion spawnRotation = Quaternion.identity;
	// 		// 	Instantiate (hazard, spawnPosition, spawnRotation);
	// 		// 	yield return new WaitForSeconds (spawnWait);
	// 		// }
	// 		yield return new WaitForSeconds (waveWait);
	// 	}
	// }

	IEnumerator SpawnWaves ()
	{
		yield return new WaitForSeconds (startWait);
		while(true)
		{
			for (int i = 0; i < hazardCount; i++)
			{
				GameObject hazard = hazards [Random.Range (0, hazards.Length)];
				Vector3 spawnPosition = new Vector3 (Random.Range (-SpawnValues.x, SpawnValues.x), SpawnValues.y, SpawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);
		}
	}

	public void minusScore (int newScoreValue)
	{
		score -= newScoreValue;
		UpdateScore ();
	}

	void UpdateScore ()
	{
		scoreText.text = "Score: " + score;
	}

	private void AddDistance ()
	{
		distance += 1;
		if(distance % 35 == 0)
		{
			for (int i = 0; i < distanceTexts.Length; i++)
			{
				if(((distance / 35)-i) % 10 == 0)
				{
					distanceTexts[i].fontSize = 50;
				}
				else
				{
					distanceTexts[i].fontSize = 20;
				}
				distanceTexts[i].text = ((distance / 35)-i).ToString();
			}
			if((distance % 350) == 0)
			{
				minusScore(-10);
			}
		}
	}
}
