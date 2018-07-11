using UnityEngine;
using UnityEngine.UI;
// using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.IO;
using CsvHelper;

public class mainPortalScript : MonoBehaviour {
	// public Button timeStartButton;
    // public Button timeResetButton;
    // public Text timePresentDisplay;
    public Button saveButton;
    public Text welcomeText;
    public InputField nameInputField;
    public InputField birthdayInputField;
    public InputField informationInputField;
    private bool isSaved = false;
    // public Text showCalibVals;
    // public GameObject calibCard;
    // public GameObject gameCard;
    // public GameObject biofeedbackCard;
    // public Text navNoText;
	// private bool timeStarted = false;

	// private float timer;

    // private float startTime = 0;

	// public static Dictionary<string,
    // public Button leftB;
    // public Button rightB;
    // //public int pageNow = 1;
    // public int maxPage;
    // public int cardPerPage = 4;
    private static string gameDir;
    private static string gameCsv;
    public static Dictionary<string, string> dataCollector = new Dictionary<string, string>();


    void Start()
    {
        // showCalibVals.text = "Your calibrations were: \n\nYAW \t\t\t (" + GameControl.calibValues[1] + ", " + GameControl.calibValues[3] +
        //  ") \nROLL\t\t\t (" + GameControl.calibValues[0] + ", " + GameControl.calibValues[2] + ")";

        // if (GameControl.userStatus == 0)
        // {
        //     welcomeText.text = "HELLO... " + GameControl.currentUserNickname + "!";
        // }
        // else if (GameControl.userStatus == 1)
        // {
        welcomeText.text = GameControl.currentUserName;
        
        // nameInputField.onEndEdit.AddListener (AcceptStringInput);
        // Button saveBtn = saveButton.GetComponent<Button>();
        saveButton.onClick.AddListener(() => onSave());
        // }
        
        // timer = 0;							// Set the time limit for this round based on the RoundData object	
		// UpdateTimeDisplay();

        // maxPage = Mathf.CeilToInt((float)gameCards.Length / (float)cardPerPage);

        // leftB.onClick.AddListener(delegate {
        //     //if (GameControl.currentGamePortalPage == 1) return;

        //     //showPage(--GameControl.currentGamePortalPage);
        //     //updatePage();
        //     goLeft();
        // });

        // rightB.onClick.AddListener(delegate
        // {
        //     //if (GameControl.currentGamePortalPage == maxPage) return;

        //     //showPage(++GameControl.currentGamePortalPage);
        //     //updatePage();
        //     goRight();
        // });

        // showPage(GameControl.currentGamePortalPage);
        // updatePage();
        // string tempTag = calibCard.tag;
        // calibCard.GetComponent<Button>().onClick.AddListener(delegate
        // {
        //     SceneManager.LoadScene(tempTag);
        // });
        // calibCard.SetActive(true);


  		// timeStartButton.GetComponent<Button>().onClick.AddListener(delegate
        // {
        // 	startTime = Time.time;
        // 	timeCollector["start"] = Mathf.Floor(startTime / 60).ToString("00") + " : " + Mathf.Floor(startTime % 60).ToString("00");
        // 	// Debug.Log(timeCollector["start"]);
        // 	timeStarted = true;        
        // });

        // timeResetButton.GetComponent<Button>().onClick.AddListener(delegate
        // {
        //     startTime = 0;
        //     timeCollector["start"] = null;
        //     // Debug.Log(timeCollector["start"]);
        //     timeStarted = false;            
        // });       
    }
    public void onSave()
    {
        //confirmQuit.SetActive(true);
        Debug.Log("call onSave()");
        // isSaved = !isSaved;
        welcomeText.text = nameInputField.text;
        // using (var ftw = new FileStream(gameCsv, FileMode.Append))
        dataCollector["name"] = nameInputField.text;
        dataCollector["birthday"] = birthdayInputField.text;
        dataCollector["information"] = informationInputField.text;
        updateGamePath();
        writeData();
    }

    public static void updateGamePath()
    {
        gameDir = @"log/Noname/";
        gameCsv = @"log/Noname/" + "01" + ".csv";
    }    

    public static void writeData()
    {
        // check if directory exist
        if (!Directory.Exists(gameDir))
        {
            Directory.CreateDirectory(gameDir);
            Debug.Log("Create log/Noname" +  "/ in home directory!");
        }
        
        // check if file exists, if not create with header
        if (!File.Exists(gameCsv))
        {
            // create new csv file
            using (var ftw = new FileStream(gameCsv, FileMode.Append))
            using (var sw = new StreamWriter(ftw))
            using (var wt = new CsvWriter(sw))
            {
                foreach (string key in dataCollector.Keys)
                {
                    wt.WriteField(key);
                }

                wt.NextRecord();

                // close all
                wt.Dispose();
                sw.Close();
                ftw.Close();
            }

            Debug.Log("File at path '" + gameCsv + "' is created with headers.");
        }

        using (var ftw = new FileStream(gameCsv, FileMode.Append))
        using (var sw = new StreamWriter(ftw))
        using (var wt = new CsvWriter(sw))
        {
            foreach (string val in dataCollector.Values)
            {
                wt.WriteField(val);
            }

            wt.NextRecord();

            // close all
            wt.Dispose();
            sw.Close();
            ftw.Close();
        }
    }


    // void AcceptStringInput(string userInput)
    // {
    //     if(isSaved)
    //     {
    //         // userInput = userInput.ToLower ();
    //         welcomeText.text = userInput;
    //     }   
    // }

    // define methods for moving gameportal pages left and right
    // void goLeft()
    // {
    //     if (GameControl.currentGamePortalPage == 1) return;

    //     showPage(--GameControl.currentGamePortalPage);
    //     updatePage();
    // }

    // void goRight()
    // {
    //     if (GameControl.currentGamePortalPage == maxPage) return;

    //     showPage(++GameControl.currentGamePortalPage);
    //     updatePage();
    // }

    // dataCollectionScene is hidden require combo key to call
    // void Update()
    // {
        // if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.D))
        // {
        //     SceneManager.LoadScene("datacollection");
        // }      
			
		// if (timeStarted)
        // {
        //     timer = Time.time-startTime;	// If the round is active, subtract the time since Update() was last called from timeRemaining
        //     UpdateTimeDisplay();
        // } else {
        //     timer = 0;
        //     UpdateTimeDisplay();
        // }
         
        // // get keyboard input to change page
        // if (Input.GetKeyDown(KeyCode.LeftArrow))
        // {
        //     goLeft();
        // }

        // if (Input.GetKeyDown(KeyCode.RightArrow))
        // {
        //     goRight();
        // }
    // }

	// private void UpdateTimeDisplay()
	// {
    //     string hours = Mathf.Floor(timer / 3600).ToString("00");
	// 	string minutes = Mathf.Floor(timer / 60).ToString("00");
	// 	string seconds = Mathf.Floor(timer % 60).ToString("00");	
	// 	timePresentDisplay.text = hours + ":" + minutes + ":" + seconds;
	// }
  
    // void updatePage()
    // {
    //     navNoText.text = GameControl.currentGamePortalPage.ToString() + " / " + maxPage.ToString();
    // }

    // void showPage(int page)
    // {
    //     // Color halfAlphaColor = new Color(255f, 255f, 255f, 0.25f);
    //     // Color fullAlphaColor = new Color(255f, 255f, 255f, 1f);

    //     // if(maxPage > 1)
    //     // {
    //     //     if (page == 1)
    //     //     {
    //     //         leftB.enabled = false;
    //     //         rightB.enabled = true;
    //     //         // there is color bug now, so the disabled color would not work
    //     //         leftB.GetComponent<Image>().color = halfAlphaColor;
    //     //         rightB.GetComponent<Image>().color = fullAlphaColor;
    //     //     }
    //     //     else if (page == maxPage)
    //     //     {
    //     //         leftB.enabled = true;
    //     //         rightB.enabled = false;
    //     //         // there is color bug now, so the disabled color would not work
    //     //         leftB.GetComponent<Image>().color = fullAlphaColor;
    //     //         rightB.GetComponent<Image>().color = halfAlphaColor;
    //     //     }
    //     //     else
    //     //     {
    //     //         leftB.enabled = true;
    //     //         rightB.enabled = true;
    //     //         // there is color bug now, so the disabled color would not work
    //     //         leftB.GetComponent<Image>().color = fullAlphaColor;
    //     //         rightB.GetComponent<Image>().color = fullAlphaColor;
    //     //     }
    //     // }
    //     // else
    //     // {
    //     //     leftB.enabled = false;
    //     //     rightB.enabled = false;
    //     //     // there is color bug now, so the disabled color would not work
    //     //     leftB.GetComponent<Image>().color = halfAlphaColor;
    //     //     rightB.GetComponent<Image>().color = halfAlphaColor;
    //     // }

    //     foreach (GameObject card in gameCards)
    //     {
    //         card.SetActive(false);
    //     }

    //     int firstInd = (4 * page) - 4;
    //     int lastInd = 4 * page;

    //     for(int i = firstInd; i < lastInd; i++)
    //     {
    //         try
    //         {
    //             gameCards[i].SetActive(true);

    //             // to solve only get last value in the loop
    //             string tempTag = gameCards[i].tag;

    //             gameCards[i].GetComponent<Button>().onClick.AddListener(delegate
    //             {
    //                 SceneManager.LoadScene(tempTag);
    //             });
    //         }
    //         catch
    //         {
    //             break;
    //         }
    //     }
    // }
}
