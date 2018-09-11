using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;               // ui objects

public class SpaceController : MonoBehaviour {
	
	public GameObject[] hazards;
	public GameObject[] rewards;
	public Vector3 SpawnValues;
	public int hazardCount; // 3
	public float spawnWait; // 1.5
	public float startWait; // 0.5
	public float waveWait;  // 0.8

	private int score;
	public Text scoreText;
	
	private int distance;
	public Text[] distanceTexts;
	public CanvasGroup distanceCanvas;

	public CanvasGroup slideCanvas;
	public Slider gaugeFill;
	public CanvasGroup centerSlideCanvas;

	public Material[] matObject;

	private Read2UDP read2UDP;
	private float Alpha;

	public float a;

    public float baseline;
    public float threshold;
	public int Fs = 256; float percentOver = 0.8f;
	private int numOverThreshold =0;

	public InputField feedbackThresholdInputField;
	public Dropdown difficultDropdown;
	private float feedbackThreshold;
	private string difficult;
	private int difficultIndex;
	public float timeDuration = 3;

	private float timeStart = 0;
	private bool isStart = false;

	private GameObject[] gameObjects;
	private static List<float> gameTime = new List<float>();
	private static List<int> gameTrigger = new List<int>();
	public int trigger = 0; 


	private static List<float> moveTime = new List<float>();
	private static List<int> moveTrigger = new List<int>();	
	private static List<float> moveEnd = new List<float>();

	private AudioSource audioSource;
	public AudioClip backgroundAudio;
	private static bool isPlayAudio = false;

	void Start ()
	{
		baseline = GameControl.currentBaselineAvg;
        threshold = GameControl.currentThresholdAvg;

        difficultDropdown.onValueChanged.AddListener(delegate {
            actionDropdownValueChanged(difficultDropdown);
        });

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

		audioSource = gameObject.GetComponent<AudioSource>();
		audioSource.clip = backgroundAudio;

		score = 0;
		distance = 350;

		centerSlideCanvas.alpha = 0;
		distanceCanvas.alpha = 0;
		slideCanvas.alpha = 0;
		AddDistance();
		UpdateScore ();
		// UpdateDistance ();
		StartCoroutine (SpawnWaves ());
	}

	void Update ()
	{
		if(timeController.isContinue)
		{
			if(timeController.isPlaying)
			{
				if( Input.GetButtonDown("Horizontal") ) {  moveTrigger.Add(1); moveTime.Add(read2UDP.timeTempChanged); }
				if( Input.GetButtonUp("Horizontal") ) 	{  moveEnd.Add(read2UDP.timeTempChanged); }

				if( Input.GetButtonDown("Vertical") ) 	{ moveTrigger.Add(2); moveTime.Add(read2UDP.timeTempChanged); }
				if( Input.GetButtonUp("Vertical") ) 	{ moveEnd.Add(read2UDP.timeTempChanged); }
				
				if(timeController.isStartGame)
				{
					if(!isPlayAudio)
					{
						audioSource.Play();  print("Play Background Audio");
						isPlayAudio = true;
					}
					
					difficult = difficultDropdown.options[difficultIndex].text;
					if(difficult == "easy")
					{
						hazardCount = 0;
					}
					else
					{
						hazardCount = 3;
					}

					if(feedbackThresholdInputField.text != "")
					{
						feedbackThreshold = float.Parse(feedbackThresholdInputField.text);
					}
					else
					{
						feedbackThreshold = 0.5f;
					}
					// print("Send to tempData");
					timeController.isStartGame = false; // comment out if not connect to G.Tec
					timeStart = timeController.timeStart;
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
					a = (Alpha-baseline)/(Mathf.Abs(threshold-baseline));
					// if(a < 0){a = 0;} else if (a > 1){a = 1;}
					
					if(Time.time - timeStart <= timeDuration)
					{
						if(a > feedbackThreshold)
						{
							numOverThreshold += 1;
						}
					}
					else
					{
						if( numOverThreshold >= percentOver*timeDuration*Fs)
						{
							RewardWaves ();
						}
						numOverThreshold = 0;
						timeStart = Time.time;
					}


					// 	if(isStart)
					// 	{
					// 		timeStart = Time.time;
					// 		isStart = false;
					// 	}

					// 	if(Time.time - timeStart >= timeDuration)
					// 	{
					// 		RewardWaves ();
					// 		isStart = true;
					// 	}
					// }	
					// else
					// {
					// 	isStart = true;
					// }	

					if(timeController.modeName == "NF with slider")
					{
						slideCanvas.alpha = 1;
						// currentGauge += (int)a;
						// currentGauge = Mathf.Clamp(currentGauge, 0, maxGauge);
						gaugeFill.value = a; //currentGauge / maxGauge;
					}
					else if(timeController.modeName == "NF with moving object")
					{
						if(difficult == "easy")
						{
							centerSlideCanvas.alpha = (1-a)/2;
						}
						else{
							for (int i = 0; i < matObject.Length; i++)
							{
								matObject[i].color = new Color(1f, 1f, 1f, a);
								matObject[i].SetFloat("_Metallic", a);
							}
						}

					}
				}
			}
			else
			{
				audioSource.Stop();
				isPlayAudio = false;
			}
		}
		else
		{
			if(timeController.isFinish)
			{
				Read2UDP.tempData["difficult"] = difficult;
				Read2UDP.tempData["feedbackthreshold"] = feedbackThreshold.ToString();
				Read2UDP.tempData["baseline"] = GameControl.currentBaselineAvg.ToString();
				Read2UDP.tempData["threshold"] = GameControl.currentThresholdAvg.ToString();
				Read2UDP.tempData["gametime"] = DataController.GameDataController.getAppendString(gameTime);
				Read2UDP.tempData["gametrigger"] = DataController.GameDataController.getAppendString(gameTrigger);
				Read2UDP.tempData["movetime"] = DataController.GameDataController.getAppendString(moveTime);
				Read2UDP.tempData["movetrigger"] = DataController.GameDataController.getAppendString(moveTrigger);
				Read2UDP.tempData["moveEnd"] = DataController.GameDataController.getAppendString(moveEnd);
				Read2UDP.tempData["score"] = score.ToString();

				gameTime.Clear();
				gameTrigger.Clear();
				timeController.isFinish = false;	
			}
			restartGame();
		}
	}

    private void actionDropdownValueChanged(Dropdown actionTarget){
        difficultIndex = actionTarget.value;
        // print(modeIndex + ">>" + actionTarget.options[modeIndex].text);
    }

	private void RewardWaves ()
	{
		GameObject reward = rewards [Random.Range (0, rewards.Length)];
		Vector3 spawnPosition = new Vector3 (Random.Range (-SpawnValues.x, SpawnValues.x), SpawnValues.y, SpawnValues.z);
		Quaternion spawnRotation = Quaternion.Euler(90, 0, 0);
		Instantiate (reward, spawnPosition, spawnRotation);
	}

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
		if(newScoreValue == 10){trigger = -1;} else {trigger = 1;}
		gameTrigger.Add(trigger);
		gameTime.Add(read2UDP.timeTempChanged);
		// print(">>" + (-1*newScoreValue) + " - " + read2UDP.timeTempChanged);
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
				RewardWaves ();
			}
		}
	}
	
	private void restartGame()
	{	
		score = 0;
		distance = 350;
		UpdateScore ();
		AddDistance();
		centerSlideCanvas.alpha = 0;
		distanceCanvas.alpha = 0;
		slideCanvas.alpha = 0;
		DestroyObjectswithTag("Asteroid");
		DestroyObjectswithTag("Reward");
	}

	private void DestroyObjectswithTag(string tag)
	{
		gameObjects =  GameObject.FindGameObjectsWithTag (tag);

		for(int i = 0 ; i < gameObjects.Length; i++)
			Destroy(gameObjects[i]);
	}
}
