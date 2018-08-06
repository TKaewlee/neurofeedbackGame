using UnityEngine;

using System.Collections.Generic;
using System.Linq;
//using System.IO.Ports;
using CsvHelper;
using System.IO;
using System;

using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour {

    // put this gameobject in prefabs folder and this will be singleton

    // to check if GameControl attached object exist or not 
    // the member of this class could be static because we don't 
    // initiate to use each members anyway (better be static)
    public static GameControl controller; 
    public static string currentScene;

    void Awake()
    {
        if (controller == null)
        {
            DontDestroyOnLoad(gameObject);
            controller = this;
        }
        else if (controller != this)
        {
            Destroy(gameObject);
            Debug.Log("There is GameControl from last scene, GameControl in _" + SceneManager.GetActiveScene().name + "_ is killed.");
        }
    }

    // static variables
    // user info
    public static string currentUserName  = "test"; //= ""; 
    public static float currentBaselineAvg = 0;
    public static float currentThresholdAvg = 1;

    // public static int userStatus = 0; // 0 for new, 1 for old

    // public static int[] calibValues = new int[4] { -20, -35, 20, 35 }; // [Left Roll, Left Yaw, Right Roll, Right Yaw]
    // public static int currentGamePage = 1;

    // private motorComm motorControl;


    // // new static class to hold calibration values
    // public static class CalibrationValues
    // {
    //     public static float LEFT_ROLL = -20f/90f;
    //     public static float LEFT_YAW = -35f/90f;
    //     public static float RIGHT_ROLL = 20f/90f; 
    //     public static float RIGHT_YAW = 35f/90f;
    // }

    void Update()
    {
        if (currentScene != SceneManager.GetActiveScene().name){
            currentScene = SceneManager.GetActiveScene().name;
        }
    }

        // != because this is supposed to call only when run game scene directly 
        // (not login from main)
        // if(SceneManager.GetActiveScene().buildIndex != 0)
        // {
        //     // calibration
        //     int[] temp = new int[4];
            // LoadDataUtility.getLastCalibVals(currentUserId, out temp);
        //     // if (getLastCalibVals(currentUserId, out temp))
        //     // {
        //     //     calibValues[0] = temp[0];
        //     //     calibValues[1] = temp[1];
        //     //     calibValues[2] = temp[2];
        //     //     calibValues[3] = temp[3];

        //     //     // new static class to hold calibration values
        //     //     CalibrationValues.LEFT_ROLL = temp[0]/90f;
        //     //     CalibrationValues.LEFT_YAW = temp[1]/90f;
        //     //     CalibrationValues.RIGHT_ROLL = temp[2]/90f;
        //     //     CalibrationValues.RIGHT_YAW = temp[3]/90f;
        //     // }
        // }

        // GameObject motorControlObj = GameObject.FindWithTag("motorControl");
        // if (motorControlObj != null)
        // {
        //     motorControl = motorControlObj.GetComponent<motorComm>();
        // }
        // if (motorControl == null)
        // {
        //     Debug.Log("cannot find motorComm script");
        // }
    // }

    // public void onClickTest()
	// {
	// 	// Debug.Log ("current id is " + currentUserId); 
	// 	Debug.Log ("current name is " + currentUserName);
	// }

    // void Update()
    // {
    //     //if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.R))
    //     //{
    //     //    motorControl.breakInstance();
    //     //    motorControl.initInstance();
    //     //}
    // }

    // // calibration variables
    // private string calibPath = @"log/calib/";
    // private List<DateTime> date = new List<DateTime>();
    // private List<int> LRollf = new List<int>();
    // private List<int> LYawf = new List<int>();
    // private List<int> RRollf = new List<int>();
    // private List<int> RYawf = new List<int>();

    // bool getLastCalibVals(string id, out int[] calibVals)
    // {
    //     if (File.Exists(calibPath + id + ".csv"))
    //     {
    //         using (var ftr = new FileStream(calibPath + id + ".csv", FileMode.Open))
    //         using (var sr = new StreamReader(ftr))
    //         using (var rd = new CsvReader(sr))
    //         {
    //             while (rd.Read())
    //             {
    //                 var eachDate = rd.GetField<string>("date");
    //                 var eachLRollf = rd.GetField<string>("LeftRoll");
    //                 var eachLYawf = rd.GetField<string>("LeftYaw");
    //                 var eachRRollf = rd.GetField<string>("RightRoll");
    //                 var eachRYawf = rd.GetField<string>("RightYaw");
    //                 date.Add(DateTime.Parse(eachDate));
    //                 LRollf.Add(int.Parse(eachLRollf));
    //                 LYawf.Add(int.Parse(eachLYawf));
    //                 RRollf.Add(int.Parse(eachRRollf));
    //                 RYawf.Add(int.Parse(eachRYawf));
    //             }
    //             // close all
    //             rd.Dispose();
    //             sr.Close();
    //             ftr.Close();
    //         }

    //         int indexOfLatest = date.IndexOf(date.Max());

    //         calibVals = new int[4]{
    //             LRollf[indexOfLatest],
    //             LYawf[indexOfLatest],
    //             RRollf[indexOfLatest],
    //             RYawf[indexOfLatest],
    //            };

    //         // catch if there is 0 in calibVals, it there is change to be 1
    //         for (int i = 0; i < calibVals.Length; i++)
    //         {
    //             if (calibVals[i] == 0)
    //             {
    //                 calibVals[i] = 1;
    //             }
    //         }

    //         // new static class to hold calibration values
    //         CalibrationValues.LEFT_ROLL = calibVals[0]/90f;
    //         CalibrationValues.LEFT_YAW = calibVals[1]/90f;
    //         CalibrationValues.RIGHT_ROLL = calibVals[2]/90f;
    //         CalibrationValues.RIGHT_YAW = calibVals[3]/90f;

    //         return true;
    //     }
    //     else
    //     {
    //         Debug.Log("This id does not have calibration data yet.");
    //         calibVals = new int[] { 1, 1, 1, 1 };

    //         // new static class to hold calibration values
    //         CalibrationValues.LEFT_ROLL = calibVals[0]/90f;
    //         CalibrationValues.LEFT_YAW = calibVals[1]/90f;
    //         CalibrationValues.RIGHT_ROLL = calibVals[2]/90f;
    //         CalibrationValues.RIGHT_YAW = calibVals[3]/90f;

    //         return false;
    //     }
    // }

    // public static float[] getCalibValues()
    // {
    //     float[] temp = new float[4];

    //     for (int i = 0; i <= 3; i++)
    //     {
    //         temp[i] = (float)calibValues[i] / 90f;
    //     }

    //     return temp;
    // }
}