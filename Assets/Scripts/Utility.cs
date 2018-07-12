// using UnityEngine;
// // using System.Collections;
// using System.Collections.Generic;
// using System.Text;
// using System.Linq;
// using CsvHelper;
// using System.IO;
// using UnityEngine.SceneManagement;

// public class LoadDataUtility {
//     // calibration variables
//     private static string calibPath = @"log/calib/";
//     private static List<System.DateTime> date = new List<System.DateTime>();
//     private static List<int> LRollf = new List<int>();
//     private static List<int> LYawf = new List<int>();
//     private static List<int> RRollf = new List<int>();
//     private static List<int> RYawf = new List<int>();

//     private static void clearCalibrationVariables()
//     {
//         // reset collector before loading new calibrations from File
//         date.Clear();
//         LRollf.Clear();
//         LYawf.Clear();
//         RRollf.Clear();
//         RYawf.Clear();
//     }

//     public static bool getLastCalibVals(string id, out int[] calibVals)
//     {
//         clearCalibrationVariables();

//         if (File.Exists(calibPath + id + ".csv"))
//         {
//             using (var ftr = new FileStream(calibPath + id + ".csv", FileMode.Open))
//             using (var sr = new StreamReader(ftr))
//             using (var rd = new CsvReader(sr))
//             {
//                 while (rd.Read())
//                 {
//                     var eachDate = rd.GetField<string>("date");
//                     var eachLRollf = rd.GetField<string>("LeftRoll");
//                     var eachLYawf = rd.GetField<string>("LeftYaw");
//                     var eachRRollf = rd.GetField<string>("RightRoll");
//                     var eachRYawf = rd.GetField<string>("RightYaw");
//                     date.Add(System.DateTime.Parse(eachDate));
//                     LRollf.Add(int.Parse(eachLRollf));
//                     LYawf.Add(int.Parse(eachLYawf));
//                     RRollf.Add(int.Parse(eachRRollf));
//                     RYawf.Add(int.Parse(eachRYawf));
//                 }
//                 // close all
//                 rd.Dispose();
//                 sr.Close();
//                 ftr.Close();
//             }

//             int indexOfLatest = date.IndexOf(date.Max());

//             calibVals = new int[4]{
//                 LRollf[indexOfLatest],
//                 LYawf[indexOfLatest],
//                 RRollf[indexOfLatest],
//                 RYawf[indexOfLatest],
//                };

//             // catch if there is 0 in calibVals, it there is change to be 1
//             for (int i = 0; i < calibVals.Length; i++)
//             {
//                 if (calibVals[i] == 0)
//                 {
//                     calibVals[i] = 1;
//                 }
//             }

//             // set gameControl static variables right in here, and also return
//             // reduce complications
//             GameControl.calibValues[0] = calibVals[0];
//             GameControl.calibValues[1] = calibVals[1];
//             GameControl.calibValues[2] = calibVals[2];
//             GameControl.calibValues[3] = calibVals[3];

//                 // new static class to hold calibration values
//             GameControl.CalibrationValues.LEFT_ROLL = calibVals[0] / 90f;
//             GameControl.CalibrationValues.LEFT_YAW = calibVals[1] / 90f;
//             GameControl.CalibrationValues.RIGHT_ROLL = calibVals[2] / 90f;
//             GameControl.CalibrationValues.RIGHT_YAW = calibVals[3] / 90f;

//             return true;
//         }
//         else
//         {
//             Debug.Log("This id does not have calibration data yet.");
//             calibVals = new int[] { -20, -35, 20, 35 };

//             // set gameControl static variables right in here, and also return
//             // reduce complications
//             GameControl.calibValues[0] = calibVals[0];
//             GameControl.calibValues[1] = calibVals[1];
//             GameControl.calibValues[2] = calibVals[2];
//             GameControl.calibValues[3] = calibVals[3];

//                 // new static class to hold calibration values
//             GameControl.CalibrationValues.LEFT_ROLL = calibVals[0] / 90f;
//             GameControl.CalibrationValues.LEFT_YAW = calibVals[1] / 90f;
//             GameControl.CalibrationValues.RIGHT_ROLL = calibVals[2] / 90f;
//             GameControl.CalibrationValues.RIGHT_YAW = calibVals[3] / 90f;

//             // automatically bring this user to calibration!
//             SceneManager.LoadScene("calibration");

//             return false;
//         }
//     }
// }

// public class Utility {

//     public static string getAverageOfFloatList(List<float> floatList)
//     {
//         string temp;

//         try
//         {
//             temp = floatList.Average().ToString("f2");
//         }
//         catch
//         {
//             temp = string.Empty;
//         }

//         return temp;
//     }

// 	public static string getRandomStringFromEnumerable(List<string> listOfString)
//     {
//         int temp = Random.Range(0, listOfString.Count);

//         //printStringInEnumerable(listOfString);
//         //Debug.Log("size of list: " + listOfString.Count);

//         return listOfString[temp];
//     }

//     public static string getRandomStringFromEnumerable(string[] arrayOfString)
//     {
//         int temp = Random.Range(0, arrayOfString.Length);
//         return arrayOfString[temp];
//     }

//     public static void printStringInEnumerable(List<string> listOfString)
//     {
//         foreach (string i in listOfString)
//         {
//             Debug.Log("print util: " + i);
//         }
//     }

//     public static void printStringInEnumerable(string[] arrayOfString)
//     {
//         foreach (string i in arrayOfString)
//         {
//             Debug.Log("print util: " + i);
//         }
//     }

//     public static string getAppendString(List<float> floatEnum)
//     {
//         StringBuilder temp = new StringBuilder();

//         try
//         {
//             float last = floatEnum.Last();

//             foreach (float i in floatEnum)
//             {
//                 if (!i.Equals(last))
//                 {
//                     temp.Append(i.ToString("f2")).Append(" ");
//                 }
//                 else
//                 {
//                     temp.Append(i.ToString("f2"));
//                 }
//             }
//         }
//         catch 
//         {
//             return null;
//         }

//         return temp.ToString();
//     }

//     public static string getAppendString(float[] floatEnum)
//     {
//         StringBuilder temp = new StringBuilder();

//         try
//         {
//             float last = floatEnum.Last();

//             foreach (float i in floatEnum)
//             {
//                 if (!i.Equals(last))
//                 {
//                     temp.Append(i.ToString("f2")).Append(" ");
//                 }
//                 else
//                 {
//                     temp.Append(i.ToString("f2"));
//                 }
//             }
//         }
//         catch
//         {
//             return null;
//         }

//         return temp.ToString();
//     }

//     public static string getAppendString(List<int> intEnum)
//     {
//         StringBuilder temp = new StringBuilder();
        
//         try
//         {
//             int last = intEnum.Last();

//             foreach (int i in intEnum)
//             {
//                 if (!i.Equals(last))
//                 {
//                     temp.Append(i.ToString()).Append(" ");
//                 }
//                 else
//                 {
//                     temp.Append(i.ToString());
//                 }
//             }
//         }
//         catch 
//         {
//             return null;
//         }

//         return temp.ToString();
//     }

//     public static string getAppendString(int[] intEnum)
//     {
//         StringBuilder temp = new StringBuilder();

//         try
//         {
//             int last = intEnum.Last();

//             foreach (int i in intEnum)
//             {
//                 if (!i.Equals(last))
//                 {
//                     temp.Append(i.ToString()).Append(" ");
//                 }
//                 else
//                 {
//                     temp.Append(i.ToString());
//                 }
//             }
//         }
//         catch
//         {
//             return null;
//         }

//         return temp.ToString();
//     }

//     public static string getAppendString(string[] strEnum)
//     {
//         StringBuilder temp = new StringBuilder();

//         for (int i = 0; i < strEnum.Length; i++) 
//         {
//             if (i != strEnum.Length - 1)
//             {
//                 temp.Append(strEnum[i]).Append(" ");
//             }
//             else
//             {
//                 temp.Append(strEnum[i]);
//             }
//         }

//         return temp.ToString();
//     }

//     public static string getAppendString(List<string> strEnum)
//     {
//         StringBuilder temp = new StringBuilder();

//         for (int i = 0; i < strEnum.Count; i++)
//         {
//             if (i != strEnum.Count - 1)
//             {
//                 temp.Append(strEnum[i]).Append(" ");
//             }
//             else
//             {
//                 temp.Append(strEnum[i]);
//             }
//         }

//         return temp.ToString();
//     }

//     public static string getAppendStringNoSpace(List<int> intEnum)
//     {
//         StringBuilder temp = new StringBuilder();
//         try
//         {
//             // int last = intEnum.Last();

//             foreach (int i in intEnum)
//             {
//                 temp.Append(i.ToString());
//             }
//         }
//         catch
//         {
//             return null;
//         }

//         return temp.ToString();
//     }
// }
