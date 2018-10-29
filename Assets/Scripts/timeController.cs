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
    public Dropdown modeDropdown;
    private int modeIndex;
    public static string modeName;

    public Button startButton;
    public static bool isContinue;

    public Button backButton;
    public Button promptYes;
    public Button promptNo;

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
            actionDropdownValueChanged(modeDropdown);
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

    private void actionDropdownValueChanged(Dropdown actionTarget)
    {
        modeIndex = actionTarget.value;
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

        modeName = modeDropdown.options[modeIndex].text;
        isContinue = !isContinue;
        settingCanvas.alpha = 0;
        settingCanvas.interactable = false;
        settingCanvas.blocksRaycasts = false;
        isStart = true;
        isStartGame = true;
    }


    public void onBack()
    {
        settingCanvas.alpha = 0;
        settingCanvas.interactable = false;
        settingCanvas.blocksRaycasts = false;
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
        settingCanvas.alpha = 1;
        settingCanvas.interactable = true;
        settingCanvas.blocksRaycasts = true;

        confirmBackCanvas.alpha = 0;
        confirmBackCanvas.blocksRaycasts = false;
        confirmBackCanvas.blocksRaycasts = false;
    }

}
