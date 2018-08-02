using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//using System;
using UnityEngine.SceneManagement;

public class pauseScript : MonoBehaviour {

    // declare at first
    private bool isPause = true;

    // get gameObject to set active state
    // obsolated, moved to use canvas only
    // public GameObject pausePage;
    // public GameObject confirmQuit;

    // get main buttons of pausePanel to add listener
    public Button pauseBack;
    public Button pauseContinue;
    public Button promptYes;
    public Button promptNo;
    // public Button setPlaytime;

    // get canvasgroup of main windows to set alpha values
    public CanvasGroup pauseCanvas;
    public CanvasGroup confirmQuitCanvas;
	// public CanvasGroup playtimeCanvas;
    // private GameController gameController;

	// public Button startButton;
    // private bool isContinue = false;

    private float timeStart = 0;
    
    public InputField timeSetInput;
    private int timeSet = 10;

	// public Text timeTimeText;
	// public Text timeStartText;
    public GUIText timeCountText;
    // public Text timeDownText;

    #region main events
    // pause window will pop up every time the level int is loaded (from another level)
    // this is to let user adjust the game settings before enter the game


    // void OnEnable()
    // {
    //     // Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
    //     SceneManager.sceneLoaded += OnLevelFinishedLoading;
    // }

    // void OnDisable()
    // {
    //     // Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
    //     SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    // }

    // void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    // {
    //     if(isGameScene())
    //     {
    //         // fix each question time stuck at 0
    //         timeController.eachQTime = 0.001f;

    //         onPause();
    //     }
    // }

//    void OnLevelWasLoaded(int level)
//    {
//        //Debug.Log("this scene is being called : " + level);
//        //// add scene in which pausePage need to be callable here
//        //// check value of scene from build settings
//        //if (level == 5 || level == 6 || level == 8 || level == 9)
//        if (level != 0 || level != 1 || level != 2 || level != 3 || level != 4)
//        {
//            //pause scene to let user adjust settings
//            onPause();
//        }
//    }

    private bool isGameScene()
    {
        // string[] scenesList = new string[] {"spaceShooter", "cognitiveRun", "tetris", "gdrive", "gmath", "gmatch", "ordering", "grouping", "matching"};
        // foreach (string i in scenesList)
        // {
        if(SceneManager.GetActiveScene().name == "NeuroFB")
        {
            return true;
        }
        // }
        return false;
    }

    void Start()
    {
        onPause();
        // set default playtime for each game [PT OT suggested]
        // if (isGameScene())
        // {
        //     if(SceneManager.GetActiveScene().name == "tetris")
        //     {
        //         playtimeMinText.text = "10";
        //         playtimeMin = 10;    
        //     }
        //     else
        //     {
        //         playtimeMinText.text = "5";
        //         playtimeMin = 5;
        //     }
        // }

		// GameObject gameControllerObject = GameObject.FindWithTag("GameController");
		// if (gameControllerObject != null)
		// {
		// 	gameController = gameControllerObject.GetComponent<GameController>();
		// }
		// if (gameControllerObject == null)
		// {
		// 	Debug.Log("Connot find 'GameController' script");
		// }

        //// hide panel at scene start, comment if wanted user to adjust
        //// settings before start game
        //pauseCanvas.alpha = 0;
        //confirmQuitCanvas.alpha = 0;
        timeStart = Time.time;
        pauseBack.onClick.AddListener(() => onBack());
        pauseContinue.onClick.AddListener(() => onPause());
        promptYes.onClick.AddListener(onPromptYes);
        promptNo.onClick.AddListener(onPromptNo);

		// setPlaytime.onClick.AddListener(onSetTime);

        //pause game to let user adjust settings
        // onPause();
    }

	// Update is called once per frame
	void Update()
    {
		// // detect pause key pressed, only receive when in game (not settings)
        if (isGameScene())
        {
            if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
            {
                onPause();
            }
            
            timeCountText.text = Mathf.Floor((Time.time - timeStart) / 60).ToString("00") + " : "
                 + Mathf.Floor((Time.time - timeStart) % 60).ToString("00");

            if (Time.time - timeStart >= timeSet)
            {
                isPause = true;
                onPause();
        } 
        }



		// // detect playtime settings input
		// if (!int.TryParse (playtimeSecText.text, out playtimeSec)) 
		// {
		// 	playtimeSecText.text = "";
		// }
		// else
		// {
		// 	if (playtimeSec > 60)
		// 	{
		// 		playtimeSecText.text = "60";
		// 	}
		// }
		// if(!int.TryParse(playtimeMinText.text, out playtimeMin))
		// {
		// 	playtimeMinText.text = "";
		// }
		// else
		// {
		// 	if (playtimeMin > 60)
		// 	{
		// 		playtimeMinText.text = "60";
		// 	}
		// }
    }
    #endregion

    #region playtime settings
    // public InputField playtimeMinText;
	// public InputField playtimeSecText;

	// private int playtimeMin;
	// private int playtimeSec;
	// public static int playtimeSet;

    // set elapsed time to play game
	// void onSetTime()
	// {
	// 	if (int.TryParse(playtimeSecText.text, out playtimeSec) || int.TryParse(playtimeMinText.text, out playtimeMin)) 
	// 	{
	// 		if (!int.TryParse(playtimeMinText.text, out playtimeMin))
	// 		{
	// 			playtimeMin = 0;
	// 		}
	// 		if (!int.TryParse(playtimeSecText.text, out playtimeSec))
	// 		{
	// 			playtimeSec = 0;
	// 		}
	// 		playtimeSet = (playtimeMin*60)+playtimeSec;
	// 		//Debug.Log(playtimeSet);

    //         // send elapsed time and bool to tell if this play 
    //         // would have time limit
    //         timeController.elapseTime = Time.time + playtimeSet;
    //         timeController.isPlaytimeSet = true;
	// 	}
	// }


	#endregion

	#region methods
    public void onBack()
    {
        pauseCanvas.alpha = 0;
        pauseCanvas.interactable = false;
        pauseCanvas.blocksRaycasts = false;
        //confirmQuit.SetActive(true);
        Debug.Log("call onBack()");
        confirmQuitCanvas.alpha = 1;
        confirmQuitCanvas.interactable = true; 
		confirmQuitCanvas.blocksRaycasts = true; // blocksRaycasts if true is interactable false is not
    }

    // void resetMotorStatus()
    // {
    //     // turn off dc motor
    //     motorComm.incomingTargetPWM = 0;
    //     // stop always move center mass
    //     motorComm.toggleWheel = false;
    //     // move 3rd motor to the center
    //     //motorControl.slider_posf = 0.5f;
    //     motorComm.toggleWheel = false;
    // }

    public void onPromptYes()
    {
    //     // reset every motor status when confirm to quit
    //     // resetMotorStatus();

        // reset pause stage
        isPause = !isPause;
        Time.timeScale = 1;

    //     //// check if we're in which game
    //     //if (Application.loadedLevelName == "spaceShooter" || Application.loadedLevelName == "cognitiveRun" || Application.loadedLevelName == "tetris" || Application.loadedLevelName == "gdrive")
    //     //{
    //     // send the last game to logWriter
    //     // LogCollector.last_game = SceneManager.GetActiveScene().name;
    //     // Debug.Log("last_gameeee: " + LogCollector.last_game);

    //     // no more question for space shooter
    //     // questionController.continuePlay = false;

    //     // write log for space shooter (obsolated)
    //     //logWriter.TO_WRITE_SPACESHOOTER = true;

    //     // send duration of play too
    //     // LogCollector.final_duration = Time.time - timeController.startTime;
    //     //logWriter.duration_SpaceShooter = Time.time - timeController.startTime;
    //     //logWriter.duration_cognitive = Time.time - timeController.startTime;
    //     //logWriter.duration_tetris = Time.time - timeController.startTime;
    //     //}

        // if quit game without setting elapsed time
        // it still moves to scoreReport scene
        SceneManager.LoadScene("_MainMenu");
    }
	
	public void onPromptNo()
    {
        // if not exit, hide canvas
        //confirmQuit.SetActive(false); // obsolated
        pauseCanvas.alpha = 1;
        pauseCanvas.interactable = true;
        pauseCanvas.blocksRaycasts = true;
        confirmQuitCanvas.alpha = 0;
        confirmQuitCanvas.blocksRaycasts = false;
		confirmQuitCanvas.blocksRaycasts = false;
    }

    public void onPause()
    {
		if (isPause)
        {
            Time.timeScale = 0;
            isPause = !isPause;
            pauseCanvas.alpha = 1;
            pauseCanvas.interactable = true;
            pauseCanvas.blocksRaycasts = true;
        }
        else
        {
            timeStart = Time.time;
            // timeStartText.text = Mathf.Floor(timeStart / 60).ToString("00") + " : " 
            //     + Mathf.Floor(timeStart % 60).ToString("00");
            timeSet = int.Parse(timeSetInput.text);
            Time.timeScale = 1;
            isPause = !isPause;
            pauseCanvas.alpha = 0;
            pauseCanvas.interactable = false;
            pauseCanvas.blocksRaycasts = false;

            // if (playtimeCanvas.interactable) // just in case user forgot to click SET
            // {
            //     onSetTime();
            // }
        }
        // confirmQuitCanvas.alpha = 0;
		// confirmQuitCanvas.blocksRaycasts = false;
    }
	#endregion
}
