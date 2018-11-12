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
    public Dropdown modeDropdown;
    private int modeIndex;
    public static string modeName;
    public Dropdown difficultDropdown;
    public static string difficult;
    private int difficultIndex;

    public Button startButton;
    public static bool isContinue;

    public Button backButton;
    public Button promptYes;
    public Button promptNo;
    public Button multipleGameButton;
    public Button multipleGameContinue;

    public static float timeStart = 0;

    public InputField timeSetInput;
    public static int timeSet;

    public Text timeCountText;

    public InputField timeFixationInput;
    public static int timeFixation; // second unit

    public Text totalScoreText;

    // public static bool isConfirmExit;
    public static bool isTimeSet = false;
    public static bool isStart = false;
    public static bool isStartGame = false;
    public static bool isFinish = false;
    public static bool isOnSave = true;
    public static bool isSaving = false;
    public static bool isFixation = false;
    public static bool isFixing = false;
    private static bool isFixationSet = false;

    public GameObject[] hazards;
    public GameObject[] rewards;

    public static bool multiGame = false;
    public static int multiSceneCnt = 0;

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
            multipleGameButton.onClick.AddListener(() => multiGameControl());

            difficultDropdown.onValueChanged.AddListener(delegate {
                actionDropdownValueChangedDifficult(difficultDropdown);
            });
        }

        typeSelectCanvas.gameObject.SetActive(true);
        settingCanvas.gameObject.SetActive(false);
        sumScoreCanvas.gameObject.SetActive(false);
        confirmBackCanvas.gameObject.SetActive(false);
        waitCanvas.gameObject.SetActive(false);
        //settingCanvas. = false;

        /*
        settingCanvas.alpha = 0;
        settingCanvas.interactable = false;
        settingCanvas.blocksRaycasts = false;
        sumScoreCanvas.alpha = 0;
        sumScoreCanvas.interactable = false;
        sumScoreCanvas.blocksRaycasts = false;
        */
    }

    // Update is called once per frame
    void Update()
    {
        //print(isContinue+" "+isTimeSet+" "+isFixation+" "+isFixationSet+" "+isFixing);
        if (isContinue == true)
        {
            isSaving = true;
            isFinish = false;
            if (isTimeSet == true)
            {
                //Debug.Log("continue + timeset");
                // timeTimeText.text = Mathf.Floor(Time.time / 60).ToString("00") + " : "
                // + Mathf.Floor(Time.time % 60).ToString("00"); 

                // timeCountText.text = Mathf.Floor((Time.time - timeStart - timeFixation) / 60).ToString("00") + " : "
                // + Mathf.Floor((Time.time - timeStart - timeFixation) % 60).ToString("00");

                // timeDownText.text = Mathf.Floor(timeSet / 60).ToString("00") + " : "
                // + Mathf.Floor(timeSet % 60).ToString("00");  


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
                        if(isFixing == false)
                        {
                            fixationCanvas.alpha = 1;
                            isFixing = true;
                        }
                    }
                    else
                    {
                        fixationCanvas.alpha = 0;
                        isFixing = false;
                    }
                }
                else
                {
                    timeCountText.text = "Time\n" + Mathf.Floor((Time.time - timeStart) / 60).ToString("00") + " : "
                    + Mathf.Floor((Time.time - timeStart) % 60).ToString("00");
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
                // isPlaying = true;
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
        fixationCanvas.alpha = 0;
        settingCanvas.gameObject.SetActive(true);
        /*
        settingCanvas.alpha = 1;
        settingCanvas.interactable = true;
        settingCanvas.blocksRaycasts = true;
        */
        isSaving = false;
        isFinish = true;
        isOnSave = true;
        // isPlaying = false;
        //isFixationSet = true;
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
        //isFixationSet = true;

        fixationCanvas.alpha = 0;
        totalScoreText.text = SpaceController.mindSpaceScore.ToString();
        waitCanvas.gameObject.SetActive(true);
        //multipleGameContinue.onClick.AddListener(() => multiGameControl());
    }

    private void actionDropdownValueChangedMode(Dropdown actionTarget)
    {
        modeIndex = actionTarget.value;
        // print(modeIndex + ">>" + actionTarget.options[modeIndex].text);
    }
    private void actionDropdownValueChangedDifficult(Dropdown actionTarget)
    {
        difficultIndex = actionTarget.value;
        // print(modeIndex + ">>" + actionTarget.options[modeIndex].text);
    }

    // private bool isCalScene()
    // {
    //     // string[] scenesList = new string[] {"spaceShooter", "cognitiveRun", "tetris", "gdrive", "gmath", "gmatch", "ordering", "grouping", "matching"};
    //     // foreach (string i in scenesList)
    //     // {
    //     if(SceneManager.GetActiveScene().name == "Calibration")
    //     {
    //         return true;
    //     }
    //     // }
    //     return false;
    // }

    private void startOnClick()
    {
        multiGame = false;
        multiSceneCnt = 0;
        Time.timeScale = 1;
        timeStart = Time.time;
        // timeStartText.text = Mathf.Floor(timeStart / 60).ToString("00") + " : " 
        //     + Mathf.Floor(timeStart % 60).ToString("00");

        if (timeSetInput.text == "")
        {
            isTimeSet = false;
        }
        else
        {
            isTimeSet = true;
            timeSet = int.Parse(timeSetInput.text);
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
        }
        modeName = modeDropdown.options[modeIndex].text;
        isContinue = true;
        settingCanvas.gameObject.SetActive(false);
        isStart = true;
        isStartGame = true;
    }


    public void onBack()
    {
        typeSelectCanvas.gameObject.SetActive(false);
        confirmBackCanvas.gameObject.SetActive(true);
        /*
        typeSelectCanvas.alpha = 0;
        typeSelectCanvas.interactable = false;
        typeSelectCanvas.blocksRaycasts = false;
        confirmBackCanvas.alpha = 1;
        confirmBackCanvas.interactable = true;
        confirmBackCanvas.blocksRaycasts = true; // blocksRaycasts if true is interactable false is not
        */
    }

    public void onPromptYes()
    {
        Time.timeScale = 1;
        // isConfirmExit = true;

        // if quit game without setting elapsed time
        // it still moves to scoreReport scene
        SceneManager.LoadScene("_MainMenu");
    }

    public void onPromptNo()
    {
        // if not exit, hide canvas
        //confirmQuit.SetActive(false); // obsolated

        typeSelectCanvas.gameObject.SetActive(true);
        confirmBackCanvas.gameObject.SetActive(false);

        /*
        typeSelectCanvas.alpha = 1;
        typeSelectCanvas.interactable = true;
        typeSelectCanvas.blocksRaycasts = true;

        confirmBackCanvas.alpha = 0;
        confirmBackCanvas.interactable = false;
        confirmBackCanvas.blocksRaycasts = false;
        */
    }

    public void multiGameControl()
    {
        multiGame = true;
        waitCanvas.gameObject.SetActive(false);
        //multipleGameContinue.onClick.RemoveListener(() => multiGameControl());

        modeName = "without NF";
        difficult = "hard";
        timeSet = 15;
        timeFixation = 1;
        multiSceneCnt++;
        if (multiSceneCnt <= 3)
        {
            Time.timeScale = 1;
            timeStart = Time.time;
            isTimeSet = true;
            isFixation = true;
            isFixationSet = true;
            isContinue = true;
            //settingCanvas.gameObject.SetActive(false);
            isStart = true;
            isStartGame = true;
        }
        
        /*
        switch (multiSceneCnt)
        {
            case 0:
                multiSceneCnt = 1;
                break;
            case 1:
                multiSceneCnt = 2;
                break;
            case 2:
                multiSceneCnt = 3;
                //print("Level3: " + timeSet + " | " + timeFixation);
                break;
            case 3:
                multiSceneCnt = 4;
                break;
            default:
                //Do nothing
                break;
        }*/
        
        Debug.Log(multiSceneCnt);
        //waitCanvas.gameObject.SetActive(false);
        print(timeStart);
        print(isContinue+" "+isFinish+isFixation+isFixationSet+isFixing+isOnSave+isSaving+isStart+isStartGame+isTimeSet);
    }

}
