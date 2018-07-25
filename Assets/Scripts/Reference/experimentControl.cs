// using UnityEngine;
// using UnityEngine.UI;
// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine.SceneManagement;

// public class experimentControl : MonoBehaviour {
//     public InputField sessionInput;
//     public InputField timeRestInput;
//     private int timeRest = 5;
//     private int timeAction = 3;
//     private int timeRep = 10;

//     public Dropdown actionDropdown;
//     public Sprite[] actions;
//     public Text actionSelected;
//     private int actionIndex;
//     // private Image actionImg;
//     public Text actionTitle;
//     public Text actionCount;
//     public Text actionRepetitions;

//     private bool isContinue = false;
// 	public Button continueButton;
//     public CanvasGroup settingPanel;

//     public Button stopButton;

//     // public Button timeResetButton;
//     public Text timePresentDisplay;
//     public Text timeEstimateDisplay;
//     private int timeConsume;

// 	private float timer;
//     private float startTime = 0;

// 	// public static Dictionary<string, string> timeCollector = new Dictionary<string, string>();

//     public CanvasGroup actionMetronome;
//     public CanvasGroup restMetronome;
//     public CanvasGroup prepareMetronome;

//     // action image
//     private GameObject actionImageObj;
//     private Image actionImage;

//     public Button pauseBack;
//     public Button promptYes;
//     public Button promptNo;  

//     public CanvasGroup confirmQuitCanvas;  

//     public AudioClip metronomeSound;
//     private int nextTime = 0;

//     void Start()
//     {
//         sessionInput.text = 1.ToString();
//         timeRestInput.text = 5.ToString();
//         estimateTime(timeRestInput.text);

//         // get action image
//         actionImageObj = GameObject.FindGameObjectWithTag("actionImage");
//         actionImage = actionImageObj.GetComponent<Image>();
//         actionImage.sprite = actions[0];

//         // Button continueBtn = continueButton.GetComponent<Button>();
//         pauseBack.onClick.AddListener(onBack);
//         promptYes.onClick.AddListener(onPromptYes);
//         promptNo.onClick.AddListener(onPromptNo);
//         continueButton.onClick.AddListener(continueOnClick);
//         stopButton.onClick.AddListener(stopOnClick);

//         // InputField restInput = timeRestInput.GetComponent<InputField>();
//         timeRestInput.onEndEdit.AddListener(estimateTime);
//         actionDropdown.onValueChanged.AddListener(delegate {
//             actionDropdownValueChanged(actionDropdown);
//         });

//         List<Dropdown.OptionData> actionItems = new List<Dropdown.OptionData>();

//         foreach(var action in actions)
//         {
//             string actionName = action.name;
//             int dot = action.name.IndexOf('.');
//             if (dot >= 0){
//                 actionName = actionName.Substring(dot+1);
//                 if (actionName.Contains("_")){
//                     actionName = actionName.Replace('_', ' ');
//                 }
//             }
//             var actionOption = new Dropdown.OptionData(actionName, action);
//             actionItems.Add(actionOption);
//         }
//         actionDropdown.AddOptions(actionItems);
//     }

//     void Update()
//     {
//         timePresentDisplay.text = System.DateTime.Now.ToString("HH : mm");    //"yyyy/MM/dd HH:mm:ss"
//         if (isContinue){
//             timer = Time.time-startTime;
            
//             if (timer >= nextTime) {
//                 StartCoroutine("playsound");
//             }
//             StopCoroutine("playsound");

//             if(timer <= timeConsume){
//                 actionCount.text = (Mathf.Round(timer)%(timeAction+timeRest)+1).ToString();
//                 if(Mathf.Round(timer)%(timeAction+timeRest) < 3){
//                     actionMetronome.alpha = 1;
//                     restMetronome.alpha = 0;
//                     prepareMetronome.alpha = 0;
//                 } else if (Mathf.Round(timer)%(timeAction+timeRest) > timeAction+timeRest-3){
//                     actionMetronome.alpha = 0;
//                     restMetronome.alpha = 0;
//                     prepareMetronome.alpha = 1;
//                 } else {
//                     actionMetronome.alpha = 0;
//                     restMetronome.alpha = 1;
//                     prepareMetronome.alpha = 0;
//                 }
//                 if(Mathf.Round(timer)%(timeAction+timeRest) == 0){
//                     actionRepetitions.text = Mathf.Round(timer/(timeAction+timeRest)).ToString() + " x";
//                 }
//             } else {
//                 // if (emgSetScript.isFinishEMG){
//                     // StartCoroutine("delayTime", 5);
//                 Debug.Log("Finished session");
//                 // StopCoroutine("delayTime");
//                 stopOnClick();
//                 // }
//             }
//         }
//     }

//     IEnumerator playsound()
//     {
//         AudioSource.PlayClipAtPoint(metronomeSound, transform.position);
//         nextTime = Mathf.RoundToInt(timer) + 1;
//         // Debug.Log("play sound");
//         StopCoroutine("playsound");
//         yield return nextTime;
//     }

//     IEnumerator delayTime(int time)
//     {
//         yield return new WaitForSeconds(time);
//     }

//     private void actionDropdownValueChanged(Dropdown actionTarget){
//         actionIndex = actionTarget.value;
//     }

//     public void continueOnClick(){
//         if(sessionInput.text != "" && int.Parse(sessionInput.text) <= 3){
//             Debug.Log(sessionInput.text + ' ' + actionIndex + ' ' + actionSelected.text + ' ' + timeConsume);
//         }
//         actionImage.sprite = actions[actionIndex];
//         actionTitle.text = actionSelected.text;
//         startTime = Time.time;
//         nextTime = 0;
//         // emgSetScript.targetSamplinginSec = timeConsume; // in seconds
//         // emgSetScript.isCallingEMG = true;
//         // StartCoroutine("delayTime", 5);
//         // Debug.Log("Finished session");
//         // StopCoroutine("delayTime");  
//         isContinue = !isContinue;
//         settingPanel.alpha = 0;
//         settingPanel.interactable = false;
//         settingPanel.blocksRaycasts = false;

      
//         // timeCollector["start"] = Mathf.Floor(startTime / 60).ToString("00") + " : " + Mathf.Floor(startTime % 60).ToString("00");
//         // Debug.Log(timeCollector["start"]);
//     }

//     public void stopOnClick(){
//         emgSetScript.isFinishEMGThread = true;    // exit thread and coroutine
//         startTime = 0;
//         // timeCollector["start"] = null;
//         isContinue = !isContinue;
//         settingPanel.alpha = 1;
//         settingPanel.interactable = true;
//         settingPanel.blocksRaycasts = true;
//     }

//     public void onBack()
//     {
//         //confirmQuit.SetActive(true);
//         //Debug.Log("call onBack()");
//         settingPanel.interactable = false;
//         // settingPanel.blocksRaycasts = false;
//         confirmQuitCanvas.alpha = 1;
//         confirmQuitCanvas.interactable = true;
// 		confirmQuitCanvas.blocksRaycasts = true; // blocksRaycasts if true is interactable false is not
//     }

//     void resetMotorStatus()
//     {
//         // turn off dc motor
//         motorComm.incomingTargetPWM = 0;
//         // stop always move center mass
//         motorComm.toggleWheel = false;
//         // move 3rd motor to the center
//         //motorControl.slider_posf = 0.5f;
//         motorComm.toggleWheel = false;
//     }

//     public void onPromptYes()
//     {
//         // reset every motor status when confirm to quit
//         resetMotorStatus();

//         // reset pause stage
//         // isPause = !isPause;
//         // Time.timeScale = 1;

//         //// check if we're in which game
//         //if (Application.loadedLevelName == "spaceShooter" || Application.loadedLevelName == "cognitiveRun" || Application.loadedLevelName == "tetris" || Application.loadedLevelName == "gdrive")
//         //{
//         // send the last game to logWriter
//         // LogCollector.last_game = SceneManager.GetActiveScene().name;
//         // Debug.Log("last_gameeee: " + LogCollector.last_game);

//         // no more question for space shooter
//         // questionController.continuePlay = false;

//         // write log for space shooter (obsolated)
//         //logWriter.TO_WRITE_SPACESHOOTER = true;

//         // send duration of play too
//         // LogCollector.final_duration = Time.time - timeController.startTime;
//         //logWriter.duration_SpaceShooter = Time.time - timeController.startTime;
//         //logWriter.duration_cognitive = Time.time - timeController.startTime;
//         //logWriter.duration_tetris = Time.time - timeController.startTime;
//         //}

//         // if quit game without setting elapsed time
//         // it still moves to scoreReport scene
//         SceneManager.LoadScene("mainportal");
//     }
	
// 	public void onPromptNo()
//     {
//         // if not exit, hide canvas

//         //confirmQuit.SetActive(false); // obsolated
//         confirmQuitCanvas.alpha = 0;
// 		confirmQuitCanvas.blocksRaycasts = false;
//         confirmQuitCanvas.interactable = false;
//         settingPanel.interactable = true;
//     }

//     void estimateTime(string timeInput){
//         timeRest = int.Parse(timeInput);
//         if(int.Parse(timeInput) <= 20 && timeInput.Length > 0){
//             timeConsume = (timeRep+2)*(int.Parse(timeInput)+timeAction);
//             UpdateTime(timeConsume);
//         }
//     }

// 	private void UpdateTime(int timer)
// 	{
//         // string hours = Mathf.Floor(timer / 3600).ToString("00");
// 		string minutes = Mathf.Floor(timer / 60).ToString();
// 		string seconds = Mathf.Floor(timer % 60).ToString();	
// 		timeEstimateDisplay.text = minutes + " m " + seconds + " s";
// 	}
// }
