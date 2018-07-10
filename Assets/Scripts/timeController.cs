using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class timeController : MonoBehaviour {
	
	public Text timeCountText;
	public Text eachQTimeText;

	public static float startTime;
	public static float eachQTime;
	
	private int showMin, showSec;

    public static bool isPlaytimeSet = false;
    public static bool isFinishPlay = false;

    public static float elapseTime; // get from pauseScript

    public static bool updateEachQTime = true;


    // method to set interactable of temporary settings in each game after a period of time
    // by default we set this as 5 seconds

    private int tempSettingsLifeTime = 5;

    IEnumerator disableSettings()
    {
        GameObject[] tempSettings = GameObject.FindGameObjectsWithTag("tempSettings");

        yield return new WaitForSeconds(tempSettingsLifeTime);

        foreach (GameObject tempCanvas in tempSettings)
        {
            tempCanvas.GetComponent<CanvasGroup>().interactable = false;
        }
    }

    // Use this for initialization
    void Start () {
        StartCoroutine(disableSettings());

		startTime = Time.time;
    }

	// Update is called once per frame
	void Update () {
        if (isPlaytimeSet)
        {
            if(elapseTime - Time.time >= 0)
            {
                showMin = (int)((elapseTime - Time.time) / 60);
                showSec = (int)((elapseTime - Time.time) % 60);
                timeCountText.text = showMin.ToString() + " m " + showSec.ToString() + " s";
            }
            else // game end
            {
                // resetMotorStatus();

                Debug.Log(elapseTime - Time.time);

                timeCountText.text = "0 m 0 s";

                isFinishPlay = true;  
                // change duration to be shared variable!!!!!!!!
                // LogCollector.final_duration = Time.time - startTime;
                // LogCollector.last_game = SceneManager.GetActiveScene().name; // send game name to logwriter
                // Debug.Log("last_gameeee: " + SceneManager.GetActiveScene().name);
                SceneManager.LoadScene("scoreReport");
            }
        }
        else
        {
    		showMin = (int)((Time.time - startTime) / 60);
    		showSec = (int)((Time.time - startTime) % 60);
    		timeCountText.text = showMin.ToString() + " m " + showSec.ToString() + " s";
        }

        if(SceneManager.GetActiveScene().name == "gmath" || SceneManager.GetActiveScene().name == "gmatch")
        {
            if (updateEachQTime)
            {
                // show it as 0 if it is at start of the game
                if (eachQTime == 0)
                {
                    eachQTimeText.text = "0.0";
                }
                else
                {
                    eachQTimeText.text = (Time.time - eachQTime).ToString("f1");
                }
            }
        }
        else
        {
            // show it as 0 if it is at start of the game
            if (eachQTime == 0)
            {
                eachQTimeText.text = "0.0";
            }
            else
            {
                eachQTimeText.text = (Time.time - eachQTime).ToString("f1");
            }
        }
    }

    // void resetMotorStatus()
    // {
    //     // turn off dc motor
    //     motorComm.incomingTargetPWM = 0;
    //     // stop always move center mass
    //     motorComm.toggleWheel = false;
    //     // move 3rd motor to the center
    //     //motorControl.slider_posf = 0.5f;
    //     //motorComm.toggleWheel = false;
    // }

    public static void resetEachQTime()
    {
        eachQTime = Time.time;
    }

    public static void setZeroEachQTime()
    {
        eachQTime = 0;
    }
}
