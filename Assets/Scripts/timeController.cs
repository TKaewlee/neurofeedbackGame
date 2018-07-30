using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class timeController : MonoBehaviour {

    public Dropdown modeDropdown;
    private int modeIndex;
    public static string modeName;

	public Button startButton;
    private bool isContinue;

    private float timeStart = 0;
    
    public InputField timeSetInput;
    public static int timeSet;

	// public Text timeTimeText;
	// public Text timeStartText;
    public Text timeCountText;
    // public Text timeDownText;
    public CanvasGroup settingCanvas;

    public static bool isTimeSet = false;
    public static bool isStart = false;
    public static bool isSetAvg = false;
    public static bool isOnSave = false;
    public static bool isSaving = false;

    public static List<float> timeUsedEachSession = new List<float>();

    // Use this for initialization
    void Start () 
    {
        print("Time Start");
        Time.timeScale = 1;
        timeStart = Time.time;
        startButton.onClick.AddListener(() => startOnClick()); 
        modeDropdown.onValueChanged.AddListener(delegate {
            actionDropdownValueChanged(modeDropdown);
        });
        settingCanvas.alpha = 1;
        settingCanvas.interactable = true;
        settingCanvas.blocksRaycasts = true;
    }

	// Update is called once per frame
	void Update () {
        if (isContinue)
        {
            isSaving = true;
            // Read2UDP.startRecieve();
            if (isTimeSet)
            {
                // timeTimeText.text = Mathf.Floor(Time.time / 60).ToString("00") + " : "
                // + Mathf.Floor(Time.time % 60).ToString("00"); 
                timeCountText.text = Mathf.Floor((Time.time - timeStart) / 60).ToString("00") + " : "
                + Mathf.Floor((Time.time - timeStart) % 60).ToString("00");

                // timeDownText.text = Mathf.Floor(timeSet / 60).ToString("00") + " : "
                // + Mathf.Floor(timeSet % 60).ToString("00");  

                if (Time.time - timeStart > timeSet)
                {
                    isContinue = !isContinue;
                    settingCanvas.alpha = 1;
                    settingCanvas.interactable = true;
                    settingCanvas.blocksRaycasts = true;
                    isSaving = false;
                    isOnSave = true; 
                    isSetAvg = true;                
                }
            }
            else
            {
                timeCountText.text = Mathf.Floor((Time.time - timeStart) / 60).ToString("00") + " : "
                + Mathf.Floor((Time.time - timeStart) % 60).ToString("00");                        
            }

            if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
            {
                isContinue = !isContinue;
                settingCanvas.alpha = 1;
                settingCanvas.interactable = true;
                settingCanvas.blocksRaycasts = true;  
                isSaving = false;
                isOnSave = true;  
                isSetAvg = true;
            }            
        }            
    }

    private void actionDropdownValueChanged(Dropdown actionTarget){
        modeIndex = actionTarget.value;
        // print(modeIndex + ">>" + actionTarget.options[modeIndex].text);
    }

    private void startOnClick(){
        print("fuckkkk");
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
        modeName = modeDropdown.options[modeIndex].text;
        isContinue = !isContinue;
        settingCanvas.alpha = 0;
        settingCanvas.interactable = false;
        settingCanvas.blocksRaycasts = false;  
        isStart = true;      
    }
}
