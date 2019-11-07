using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class timeController : MonoBehaviour
{
    public CanvasGroup settingCanvas;
    public CanvasGroup confirmBackCanvas;
    public CanvasGroup fixationCanvas;
    public CanvasGroup typeSelectCanvas;
    public CanvasGroup waitCanvas;
    public CanvasGroup sumScoreCanvas;
    public CanvasGroup instructionCanvas;
    public CanvasGroup countDownCanvas;
    public Dropdown modeDropdown;
    private int modeIndex;
    public static string modeName;
    public Dropdown difficultDropdown;
    public static string difficult;
    public static int difficultIndex;
    private List<string> difficultyOptions_withoutNF = new List<string> { "easy(no asteroid)", "easy (with sfx)", "easy (without sfx)", "hard (with sfx)", "hard (without sfx)"};
    private List<string> difficultyOptions_NFwithSlider = new List<string> { "-" };
    private List<string> difficultyOptions_NFwithMovingObj = new List<string> { "easy", "hard" };
    public Dropdown sequenceDropdown;
    private int sequenceIndex;
    public InputField FeedbackThresholdField;

    public Button startButton;
    public static bool isContinue;

    public Button backButton;
    public Button promptYes;
    public Button promptNo;
    //public Button multipleGameButton;
    //public Button multipleGameContinue;
    public Button multipleStartButton;
    public Button multipleBackButton;

    public static float timeStart = 0;

    public InputField timeSetInput;
    public static int timeSet;

    public Text timeCountText;

    public InputField timeFixationInput;
    public static int timeFixation; // second unit

    public Text totalScoreText;

    public Text countDownText;
    private int cntDwnNum;

    public Text waitText;

    // public static bool isConfirmExit;
    public static bool isTimeSet = false;
    public static bool isStart = false;
    public static bool isStartGame = false;
    public static bool isFinish = false;
    public static bool isOnSave = false;
    public static bool isSaving = false;
    public static bool isFixation = false;
    public static bool isFixing = false;
    public static bool isFixationSet = false;
    public static bool isCountDown = false;
    public static bool isCountDownSet = true;

    public GameObject[] hazards;
    public GameObject[] rewards;

    public static bool multiGame = false;
    public static int multiSceneCnt = 0;

    private int timeCntDwn = 5;
    private float timeStartCntDwn;

    private int[,] sequenceSet = { { 1, 2, 3, 4 }, { 3, 1, 2, 4 }, { 2, 3, 1, 4 } };
    private int sequenceCnt;

    // Use this for initialization
    void Start()
    {
        print("Time Start");
        Time.timeScale = 0;
        // isConfirmExit = false;
        timeStart = Time.time;
        startButton.onClick.AddListener(() => startOnClick());
        backButton.onClick.AddListener(() => onBack());
        promptYes.onClick.AddListener(onPromptYes);
        promptNo.onClick.AddListener(onPromptNo);

        modeDropdown.onValueChanged.AddListener(delegate {
            actionDropdownValueChangedMode(modeDropdown);
        });

        if(SceneManager.GetActiveScene().name != "Calibration")
        {
            difficultDropdown.onValueChanged.AddListener(delegate {
                actionDropdownValueChangedDifficult(difficultDropdown);
            });
            sequenceDropdown.onValueChanged.AddListener(delegate {
                actionDropdownValueChangedSequence(sequenceDropdown);
            });
            typeSelectCanvas.gameObject.SetActive(true);
            sumScoreCanvas.gameObject.SetActive(false);
            waitCanvas.gameObject.SetActive(false);
            instructionCanvas.gameObject.SetActive(false);
            settingCanvas.gameObject.SetActive(false);
        }
        else
        {
            settingCanvas.gameObject.SetActive(true);
        }
        
        confirmBackCanvas.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isContinue == true)
        {
            isSaving = true;
            isFinish = false;
            if (isTimeSet == true)
            {
                if (isFixation == true)
                {
                    timeCountText.text = "Time\n" + Mathf.Floor((Time.time - timeStart - timeFixation) / 60).ToString("00") + " : "
                    + Mathf.Floor((Time.time - timeStart - timeFixation) % 60).ToString("00");

                    if (isFixationSet == true)
                    {
                        timeSet = timeSet + (2 * timeFixation);
                        isFixationSet = false;
                    }

                    if (Time.time - timeStart < timeFixation | Time.time - timeStart > timeSet - timeFixation)
                    {
                        fixationCanvas.gameObject.SetActive(true);
                        isFixing = true;
                    }
                    else
                    {
                        fixationCanvas.gameObject.SetActive(false);
                        isFixing = false;
                    }

                    if (Time.time - timeStart > timeFixation & Time.time - timeStart < timeFixation + timeCntDwn)
                    {
                        if (!isCountDown)
                        {
                            timeStartCntDwn = Time.time - timeStart;
                            cntDwnNum = 5;
                            countDownCanvas.alpha = 1;
                            isCountDown = true;
                            //isCountDownSet = false;
                        }
                        if (Time.time - timeStart > timeStartCntDwn + 1)
                        {
                            timeStartCntDwn += 1;
                            cntDwnNum--;
                        }
                        countDownText.text = cntDwnNum.ToString();
                        if (cntDwnNum == 1)
                        {
                            isCountDownSet = false;
                        }
                    }
                    else
                    {
                        countDownCanvas.alpha = 0;
                        isCountDown = false;
                    }
                }
                else
                {
                    timeCountText.text = "Time\n" + Mathf.Floor((Time.time - timeStart) / 60).ToString("00") + " : "
                    + Mathf.Floor((Time.time - timeStart) % 60).ToString("00");

                    if (SceneManager.GetActiveScene().name != "Calibration")
                    {
                        if (Time.time - timeStart < timeCntDwn)
                        {
                            if (!isCountDown)
                            {
                                timeStartCntDwn = Time.time - timeStart;
                                cntDwnNum = 5;
                                countDownCanvas.alpha = 1;
                                isCountDown = true;
                                isCountDownSet = false;
                            }
                            if (Time.time - timeStart > timeStartCntDwn + 1)
                            {
                                timeStartCntDwn += 1;
                                cntDwnNum--;
                            }
                            countDownText.text = cntDwnNum.ToString();
                            if (cntDwnNum == 1)
                            {
                                isCountDownSet = false;
                            }
                        }
                        else
                        {
                            countDownCanvas.alpha = 0;
                            isCountDown = false;
                        }
                    }
                    else
                    {
                        fixationCanvas.gameObject.SetActive(true);
                    }
                }
                //print("time: " + Time.time + " | " + timeStart + " | " + timeSet);
                if (Time.time - timeStart > timeSet)
                {
                    Debug.Log("timeend");
                    if(multiGame == false)
                    {
                        onSetting();
                    }
                    else
                    {
                        onWaiting();
                    }
                    
                }
            }
            else
            {
                timeCountText.text = "Time\n" + Mathf.Floor((Time.time - timeStart) / 60).ToString("00") + " : "
                + Mathf.Floor((Time.time - timeStart) % 60).ToString("00");
            }

            if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
            {
                onSetting();
            }
        }
    }

    public void onSetting()
    {
        Debug.Log("onSetting");
        Time.timeScale = 0;
        isContinue = false;
        fixationCanvas.gameObject.SetActive(false);
        settingCanvas.gameObject.SetActive(true);
        isSaving = false;
        isFinish = true;
        isOnSave = true;
    }

    public void onWaiting()
    {
        Debug.Log("onWaiting");
        Time.timeScale = 0;
        isContinue = false;
        isSaving = false;
        isFinish = true;
        isOnSave = true;
        isFixing = false;

        fixationCanvas.gameObject.SetActive(false);
        totalScoreText.text = SpaceController.mindSpaceScore.ToString();
        waitCanvas.gameObject.SetActive(true);
        StartCoroutine(WaitForKeysToMultiGame());
        
    }

    public void ShowInstrctionCanvas()
    {
        instructionCanvas.gameObject.SetActive(true);
        StartCoroutine(WaitForKeysToMultiGame());
    }

    IEnumerator WaitForKeysToMultiGame()
    {
        /*while (!(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)))
        {
            
        }*/
        //while (!(Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow)))
        while (!(Input.GetKeyUp(KeyCode.Space)))
        {
            yield return null;
        }
        multiGameControl();
    }

    private void actionDropdownValueChangedMode(Dropdown actionTarget)
    {
        modeIndex = actionTarget.value;
        // print(modeIndex + ">>" + actionTarget.options[modeIndex].text);
        difficultDropdown.ClearOptions();
        switch (modeIndex)
        {
            case 0:
                difficultDropdown.AddOptions(difficultyOptions_withoutNF);
                FeedbackThresholdField.interactable = false;
                break;
            case 1:
                difficultDropdown.AddOptions(difficultyOptions_NFwithSlider);
                FeedbackThresholdField.interactable = true;
                break;
            case 2:
                difficultDropdown.AddOptions(difficultyOptions_NFwithMovingObj);
                FeedbackThresholdField.interactable = true;
                break;
        }
        
    }
    private void actionDropdownValueChangedDifficult(Dropdown actionTarget)
    {
        difficultIndex = actionTarget.value;
        // print(modeIndex + ">>" + actionTarget.options[modeIndex].text);
    }
    private void actionDropdownValueChangedSequence(Dropdown actionTarget)
    {
        sequenceIndex = actionTarget.value;
        // print(modeIndex + ">>" + actionTarget.options[modeIndex].text);
    }

    private void startOnClick()
    {
        multiGame = false;
        multiSceneCnt = 0;
        Time.timeScale = 1;
        timeStart = Time.time;

        if (timeSetInput.text == "")
        {
            //isTimeSet = false;
            isTimeSet = true;
            timeSet = 60 + timeCntDwn;
        }
        else
        {
            isTimeSet = true;
            timeSet = int.Parse(timeSetInput.text);
            if (SceneManager.GetActiveScene().name != "Calibration")
            {
                timeSet = timeSet + timeCntDwn;
            }
        }

        if (timeFixationInput.text == "")
        {
            isFixation = false;
            isFixationSet = false;
        }
        else
        {
            isFixation = true;
            isFixationSet = true;
            timeFixation = int.Parse(timeFixationInput.text);
        }

        if (SceneManager.GetActiveScene().name != "Calibration")
        {
            difficult = difficultDropdown.options[difficultIndex].text;
            print(difficultIndex);
        }
        modeName = modeDropdown.options[modeIndex].text;
        isContinue = true;
        settingCanvas.gameObject.SetActive(false);
        isStart = true;
        isStartGame = true;
        isCountDownSet = true;
    }


    public void onBack()
    {
        if (SceneManager.GetActiveScene().name != "Calibration")
        {
            typeSelectCanvas.gameObject.SetActive(false);
        }
        else
        {
            settingCanvas.gameObject.SetActive(false);
        }
        confirmBackCanvas.gameObject.SetActive(true);
    }

    public void onPromptYes()
    {
        Time.timeScale = 1;

        // if quit game without setting elapsed time
        // it still moves to scoreReport scene
        SceneManager.LoadScene("_MainMenu");
    }

    public void onPromptNo()
    {
        // if not exit, hide canvas
        //confirmQuit.SetActive(false); // obsolated
        if (SceneManager.GetActiveScene().name != "Calibration")
        {
            typeSelectCanvas.gameObject.SetActive(true);
        }
        else
        {
            settingCanvas.gameObject.SetActive(true);
        }
        confirmBackCanvas.gameObject.SetActive(false);
    }

    public void multiGameControl()
    {
        multiGame = true;
        waitCanvas.gameObject.SetActive(false);
        instructionCanvas.gameObject.SetActive(false);
        
        modeName = "without NF";
        timeSet =  360 + timeCntDwn; //300 + timeCntDwn
        timeFixation = 0; //120

        multiSceneCnt++;
        if (multiSceneCnt <= 4)
        {
            difficultIndex = sequenceSet[sequenceIndex, multiSceneCnt - 1];
            Time.timeScale = 1;
            timeStart = Time.time;
            isTimeSet = true;
            isFixation = false; //true
            isFixationSet = false; //true
            isContinue = true;
            isStart = true;
            isStartGame = true;
            isCountDownSet = true;
            if (multiSceneCnt < 4)
            {
                waitText.text = "If you are ready, please press space bar to continue.";
            }
            else
            {
                waitText.text = "Thank you for your participation.\nPlease press space bar to continue.";
            }
        }
        else
        {
            typeSelectCanvas.gameObject.SetActive(true);
            multiSceneCnt = 0;
        }

        Debug.Log(multiSceneCnt);
    }
}
