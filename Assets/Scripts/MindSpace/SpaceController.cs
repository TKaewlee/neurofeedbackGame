using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;               // ui objects

public class SpaceController : MonoBehaviour
{

    public GameObject[] hazards;
    public int hazardsSpeed;
    public GameObject[] rewards;
    public int rewardsSpeed;
    public Vector3 SpawnValues;
    public int configHazardCount;
    private int hazardCount; // 3
    public float spawnWait; // 1.5
    public float startWait; // 0.5
    public float waveWait;  // 0.8

    public static int mindSpaceScore = 0;
    public Text scoreText;

    private int distance;
    public Text[] distanceTexts;
    public CanvasGroup distanceCanvas;

    public CanvasGroup slideCanvas;
    public Slider gaugeFill;
    public CanvasGroup centerSlideCanvas;

    public Material[] matObject;

    private Read2UDP read2UDP;
    private float data;

    public float a;

    private float baseline;
    private float threshold;

    public int Fs = 256;
    public float percentOver = 0.01f;
    private int numOverThreshold = 0;

    public InputField feedbackThresholdInputField;
    private float feedbackThreshold;
    public float timeDuration = 1;

    private float timeStart = 0;

    private GameObject[] gameObjects;
    private static List<float> gameTime = new List<float>();
    private static List<int> gameTrigger = new List<int>();
    public int trigger = 0;

    private static List<float> asteroidAppearTime = new List<float>();
    private static List<float> rewardAppearTime = new List<float>();

    private static List<float> moveTime = new List<float>();
    private static List<int> moveTrigger = new List<int>();
    private static List<float> moveEnd = new List<float>();

    private AudioSource audioSource;
    public AudioClip backgroundAudio;
    private static bool isPlayAudio;

    private int countObject;
    private int tmpHazardsSpeed;
    private int tmpRewardsSpeed;

    void Start()
    {
        baseline = GameControl.currentBaselineAvg;
        threshold = GameControl.currentThresholdAvg;

        GameObject gameControllerObject = GameObject.FindGameObjectWithTag("UDPReciever");
        if (gameControllerObject != null)
        {
            read2UDP = gameControllerObject.GetComponent<Read2UDP>();
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
        isPlayAudio = false;

        mindSpaceScore = 0;
        distance = 350;

        centerSlideCanvas.alpha = 0;
        distanceCanvas.alpha = 0;
        slideCanvas.alpha = 0;
        AddDistance();
        UpdateScore();
        StartCoroutine(SpawnWaves());
    }

    void Update()
    {
        //print(timeController.isContinue+" "+timeController.isFixing+" "+timeController.isStartGame);
        //if ((timeController.isFixation == false || (timeController.isFixation == true && timeController.isFixationSet == false)) && timeController.isCountDownSet == false)
        if (timeController.isCountDownSet == false)
        {
            if (timeController.isContinue == true)
            {
                if (timeController.isFixing == true || timeController.isCountDown == true)
                {
                    if (isPlayAudio == true)
                    {
                        audioSource.Stop(); print("Stop Background Audio");
                        isPlayAudio = false;
                    }
                    hazardCount = 0;
                    DestroyObjectswithTag("Asteroid");
                    DestroyObjectswithTag("Reward");
                }
                else
                {
                    if (Input.GetButtonDown("Horizontal")) { moveTrigger.Add(1); moveTime.Add(read2UDP.timeTempChanged); }
                    if (Input.GetButtonUp("Horizontal")) { moveEnd.Add(read2UDP.timeTempChanged); }

                    if (Input.GetButtonDown("Vertical")) { moveTrigger.Add(2); moveTime.Add(read2UDP.timeTempChanged); }
                    if (Input.GetButtonUp("Vertical")) { moveEnd.Add(read2UDP.timeTempChanged); }

                    if (timeController.isStartGame == true)
                    {
                        if (isPlayAudio == false)
                        {
                            if (timeController.modeName == "without NF" && (timeController.difficultIndex == 2 || timeController.difficultIndex == 4)) //hard (without sound)
                            {
                                AudioListener.volume = 0;
                            }
                            else
                            {
                                audioSource.Play(); print("Play Background Audio");
                                isPlayAudio = true;
                                AudioListener.volume = 1;
                            }
                        }
                        

                        if (timeController.difficultIndex == 0) //easy (no asteroid)
                        {
                            hazardCount = 0;

                            for (countObject = 0; countObject < rewards.Length; countObject++)
                            {
                                rewards[countObject].GetComponent<RewardMover>().speed = -3;
                            }
                        }
                        else
                        {
                            hazardCount = configHazardCount;
                        
                            switch(timeController.difficultIndex)
                            {
                                case 1:
                                case 2:
                                    tmpHazardsSpeed = -5;
                                    tmpRewardsSpeed = -5;
                                    break;
                                case 3:
                                case 4:
                                    tmpHazardsSpeed = -15;
                                    tmpRewardsSpeed = -15;
                                    break;
                                default:
                                    tmpHazardsSpeed = hazardsSpeed;
                                    tmpRewardsSpeed = rewardsSpeed;
                                    break;
                            }
                            print(tmpHazardsSpeed);
                            for(countObject = 0; countObject < hazards.Length; countObject++)
                            {
                                hazards[countObject].GetComponent<Mover>().speed = tmpHazardsSpeed;
                            }

                            for (countObject = 0; countObject < rewards.Length; countObject++)
                            {
                                rewards[countObject].GetComponent<RewardMover>().speed = tmpRewardsSpeed;
                            }
                        
                        }

                        if (feedbackThresholdInputField.text != "")
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


                    if (timeController.modeName == "without NF")
                    {
                        distanceCanvas.alpha = 1;
                        AddDistance();
                    }
                    else
                    {
                        data = read2UDP.dataTempChanged;
                        // dataAvgChanged.Add(Alpha);
                        a = (data - baseline) / (Mathf.Abs(threshold - baseline));
                        if (a < 0) { a = 0; } else if (a > 1) { a = 1; }

                        //print(">> " + numOverThreshold + " - " + percentOver * timeDuration * Fs);
                        if (Time.time - timeStart <= timeDuration)
                        {
                            if (a > feedbackThreshold)
                            {
                                numOverThreshold += 1;
                            }
                        }
                        else
                        {
                            if (numOverThreshold >= percentOver * timeDuration * Fs)
                            {
                                RewardWaves();
                            }
                            numOverThreshold = 0;
                            timeStart = Time.time;
                        }

                        if (timeController.modeName == "NF with slider")
                        {
                            slideCanvas.alpha = 1;
                            // currentGauge += (int)a;
                            // currentGauge = Mathf.Clamp(currentGauge, 0, maxGauge);
                            gaugeFill.value = a; //currentGauge / maxGauge;
                        }
                        else if (timeController.modeName == "NF with moving object")
                        {
                            if (timeController.difficult == "easy")
                            {
                                centerSlideCanvas.alpha = 0.8f * (1 - a);
                            }
                            else
                            {
                                for (int i = 0; i < matObject.Length; i++)
                                {
                                    matObject[i].color = new Color(1f, 1f, 1f, a);
                                    matObject[i].SetFloat("_Metallic", a);
                                }
                            }

                        }
                    }
                }
            }
            else
            {
                if (timeController.isFinish == true)
                {
                    if (isPlayAudio == true)
                    {
                        audioSource.Stop(); print("Stop Background Audio");
                        isPlayAudio = false;
                    }
                    Read2UDP.tempData["difficult"] = timeController.difficult;
                    Read2UDP.tempData["feedbackthreshold"] = feedbackThreshold.ToString();
                    Read2UDP.tempData["baseline"] = GameControl.currentBaselineAvg.ToString();
                    Read2UDP.tempData["threshold"] = GameControl.currentThresholdAvg.ToString();
                    Read2UDP.tempData["gametime"] = DataController.GameDataController.getAppendString(gameTime);
                    Read2UDP.tempData["gametrigger"] = DataController.GameDataController.getAppendString(gameTrigger);
                    Read2UDP.tempData["movetime"] = DataController.GameDataController.getAppendString(moveTime);
                    Read2UDP.tempData["movetrigger"] = DataController.GameDataController.getAppendString(moveTrigger);
                    Read2UDP.tempData["moveEnd"] = DataController.GameDataController.getAppendString(moveEnd);
                    Read2UDP.tempData["asteroidappeartime"] = DataController.GameDataController.getAppendString(asteroidAppearTime);
                    Read2UDP.tempData["rewardappeartime"] = DataController.GameDataController.getAppendString(rewardAppearTime);
                    Read2UDP.tempData["score"] = mindSpaceScore.ToString();

                    gameTime.Clear();
                    gameTrigger.Clear();
                    moveTime.Clear();
                    moveTrigger.Clear();
                    moveEnd.Clear();
                    asteroidAppearTime.Clear();
                    rewardAppearTime.Clear();
                    timeController.isFinish = false;
                }
                RestartGame();
            }
        }
    }

    /*private void actionDropdownValueChanged(Dropdown actionTarget)
    {
        timeController.difficultIndex = actionTarget.value;
        // print(modeIndex + ">>" + actionTarget.options[modeIndex].text);
    }*/

    private void RewardWaves()
    {
        GameObject reward = rewards[Random.Range(0, rewards.Length)];
        Vector3 spawnPosition = new Vector3(Random.Range(-SpawnValues.x, SpawnValues.x), SpawnValues.y, SpawnValues.z);
        Quaternion spawnRotation = Quaternion.Euler(90, 0, 0);
        Instantiate(reward, spawnPosition, spawnRotation);
        rewardAppearTime.Add(read2UDP.timeTempChanged);
        //print("Reward time: " + read2UDP.timeTempChanged);
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-SpawnValues.x, SpawnValues.x), SpawnValues.y, SpawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                asteroidAppearTime.Add(read2UDP.timeTempChanged);
                //print("Asteriod time: " + Time.time);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
        }
    }

    public void MinusScore(int newScoreValue)
    {
        mindSpaceScore -= newScoreValue;
        UpdateScore();
        if (newScoreValue == 10)
        {
            trigger = -1;
            //print("Asteroid hit time: " + Time.time);
        }
        else
        {
            trigger = 1;
        }
        gameTrigger.Add(trigger);
        gameTime.Add(read2UDP.timeTempChanged);
        //print(">>" + (-1*newScoreValue) + " - " + read2UDP.timeTempChanged);
    }

    void UpdateScore()
    {
        scoreText.text = mindSpaceScore.ToString();
    }

    private void AddDistance()
    {
        distance += 1;
        if (distance % 35 == 0)
        {
            for (int i = 0; i < distanceTexts.Length; i++)
            {
                if (((distance / 35) - i) % 10 == 0)
                {
                    distanceTexts[i].fontSize = 50;
                }
                else
                {
                    distanceTexts[i].fontSize = 20;
                }
                distanceTexts[i].text = ((distance / 35) - i).ToString();
            }
            if ((distance % 350) == 0)
            {
                RewardWaves();
            }
        }
    }

    private void RestartGame()
    {
        for (int i = 0; i < matObject.Length; i++)
        {
            matObject[i].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            matObject[i].SetFloat("_Metallic", 1.0f);
        }

        mindSpaceScore = 0;
        distance = 350;
        UpdateScore();
        AddDistance();
        centerSlideCanvas.alpha = 0;
        distanceCanvas.alpha = 0;
        slideCanvas.alpha = 0;
        DestroyObjectswithTag("Asteroid");
        DestroyObjectswithTag("Reward");
    }

    private void DestroyObjectswithTag(string tag)
    {
        gameObjects = GameObject.FindGameObjectsWithTag(tag);

        for (int i = 0; i < gameObjects.Length; i++)
            Destroy(gameObjects[i]);
    }
}
