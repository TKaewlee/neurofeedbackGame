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

    public static float timeStart = 0;

    public InputField timeSetInput;
    public static int timeSet;

    public Text timeCountText;

    public InputField timeFixationInput;
    public static int timeFixation; // second unit

    // public static bool isConfirmExit;
    public static bool isTimeSet = false;
    public static bool isStart = false;
    public static bool isStartGame = false;
    public static bool isFinish = false;
    public static bool isOnSave = false;
    public static bool isSaving = false;
    public static bool isFixation;
    public static bool isFixing = false;
    private static bool isFixationSet = true;

    public GameObject[] hazards;
    public GameObject[] rewards;

    private bool multiGame = false;

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
        multipleGameButton.onClick.AddListener(() => multiGameControl());

        modeDropdown.onValueChanged.AddListener(delegate {
            actionDropdownValueChangedMode(modeDropdown);
        });
        difficultDropdown.onValueChanged.AddListener(delegate {
            actionDropdownValueChangedDifficult(difficultDropdown);
        });

        settingCanvas.alpha = 1;
        settingCanvas.interactable = true;
        settingCanvas.blocksRaycasts = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isContinue)
        {
            isSaving = true;
            isFinish = false;
            if (isTimeSet)
            {
                // timeTimeText.text = Mathf.Floor(Time.time / 60).ToString("00") + " : "
                // + Mathf.Floor(Time.time % 60).ToString("00"); 

                // timeCountText.text = Mathf.Floor((Time.time - timeStart - timeFixation) / 60).ToString("00") + " : "
                // + Mathf.Floor((Time.time - timeStart - timeFixation) % 60).ToString("00");

                // timeDownText.text = Mathf.Floor(timeSet / 60).ToString("00") + " : "
                // + Mathf.Floor(timeSet % 60).ToString("00");  


                if (isFixation)
                {
                    timeCountText.text = "Time\n" + Mathf.Floor((Time.time - timeStart - timeFixation) / 60).ToString("00") + " : "
                    + Mathf.Floor((Time.time - timeStart - timeFixation) % 60).ToString("00");

                    if (isFixationSet)
                    {
                        timeSet = timeSet + 2 * timeFixation;
                        isFixationSet = false;
                    }

                    if (Time.time - timeStart < timeFixation | Time.time - timeStart > timeSet - timeFixation)
                    {
                        fixationCanvas.alpha = 1;
                        isFixing = true;
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
                    /*sumScoreCanvas.alpha = 1;
                    sumScoreCanvas.interactable = true;
                    sumScoreCanvas.blocksRaycasts = true;*/
                    onSetting();
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
        Time.timeScale = 0;
        isContinue = !isContinue;
        fixationCanvas.alpha = 0;
        settingCanvas.alpha = 1;
        settingCanvas.interactable = true;
        settingCanvas.blocksRaycasts = true;
        isSaving = false;
        isFinish = true;
        isOnSave = true;
        // isPlaying = false;
        isFixationSet = true;
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
        }
        else
        {
            isFixation = true;
            timeFixation = int.Parse(timeFixationInput.text);
        }

        difficult = difficultDropdown.options[difficultIndex].text;
        modeName = modeDropdown.options[modeIndex].text;
        isContinue = !isContinue;
        settingCanvas.alpha = 0;
        settingCanvas.interactable = false;
        settingCanvas.blocksRaycasts = false;
        isStart = true;
        isStartGame = true;

        multiGame = false;
    }


    public void onBack()
    {
        typeSelectCanvas.alpha = 0;
        typeSelectCanvas.interactable = false;
        typeSelectCanvas.blocksRaycasts = false;
        confirmBackCanvas.alpha = 1;
        confirmBackCanvas.interactable = true;
        confirmBackCanvas.blocksRaycasts = true; // blocksRaycasts if true is interactable false is not
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
        typeSelectCanvas.alpha = 1;
        typeSelectCanvas.interactable = true;
        typeSelectCanvas.blocksRaycasts = true;

        confirmBackCanvas.alpha = 0;
        confirmBackCanvas.interactable = false;
        confirmBackCanvas.blocksRaycasts = false;
    }

    public void multiGameControl()
    {
        int i;
        //GameObject a;
        
        /* 1st Game */
        for(i = 0; i < hazards.Length; i++)
        {
            //a = hazards[i];
            hazards[i].GetComponent<Mover>().speed = -30;
        }

        modeName = "without NF";
        difficult = "hard";
        timeSet = 15;
        timeFixation = 1;

        Time.timeScale = 1;
        timeStart = Time.time;

        isTimeSet = true;
        isFixation = true;
        isContinue = !isContinue;
        settingCanvas.alpha = 0;
        settingCanvas.interactable = false;
        settingCanvas.blocksRaycasts = false;
        isStart = true;
        isStartGame = true;

        multiGame = true;
    }

}
