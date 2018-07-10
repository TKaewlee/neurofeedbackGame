// using UnityEngine;
// using System; // datetime
// using System.IO; // write read file
// using System.Collections.Generic; // list 
// using CsvHelper;
// using System.Linq;

// public static class getFromLog {

// 	// Use this for initialization

//     static int getHighScoreFromCSV(string logFilePath)
//     {
//         int tempMaxScore = 0;

//         using (var ftr = new FileStream(logFilePath, FileMode.Open))
//         using (var sr = new StreamReader(ftr))
//         using (var rd = new CsvReader(sr))
//         {
//             while (rd.Read())
//             {
//                 int eachScore;

//                 // need try parse because negative value couldn't be parsed
//                 // if catch, assign 0, worse than 0 would just give 0 (in case of finding highscore)
//                 try
//                 {
//                     eachScore = rd.GetField<int>("score");
//                 }
//                 catch
//                 {
//                     eachScore = 0;
//                 }

//                 if (eachScore > tempMaxScore)
//                 {
//                     tempMaxScore = eachScore;
//                 }
//             }

//             // close all
//             rd.Dispose();
//             sr.Close();
//             ftr.Close();
//         }

//         return tempMaxScore;
//     }

//     public static int getHighScoreOf(string game)
//     {
//         //string gameLogPath = @"log/";
//         string userGameLogPath = @"log/";

//         //gameLogPath += game + "/";
//         userGameLogPath += game + "/" + GameControl.currentUserId + ".csv";

//         return getHighScoreFromCSV(userGameLogPath);
//     }

//     private static List<DateTime> date = new List<DateTime>();
//     private static List<string> difficulty = new List<string>();
//     private static List<string> motorSpin = new List<string>();
//     private static List<bool> alwaysMove = new List<bool>();

//     public static int getLastMode(string game, string userName)
//     {
//         string[] modeList =  new string[3] { "easy", "medium", "hard" };
//         string logPath = @"log/" + game + "/" + userName + ".csv";

//         // clear all elements in list first
//         date.Clear();
//         difficulty.Clear();

//         if (File.Exists(logPath))
//         {
//             using (var ftr = new FileStream(logPath, FileMode.Open))
//             using (var sr = new StreamReader(ftr))
//             using (var rd = new CsvReader(sr))
//             {
//                 while (rd.Read())
//                 {
//                     var eachDate = rd.GetField<string>("date");
//                     var eachMode = rd.GetField<string>("difficulty");
    
//                     date.Add(DateTime.Parse(eachDate));
//                     difficulty.Add(eachMode);
//                 }
//                 // close all
//                 rd.Dispose();
//                 sr.Close();
//                 ftr.Close();
//             }

//             int indexOfLatest = date.IndexOf(date.Max());

//             //Debug.Log(GameControl.currentUserId + " got this level: " + difficulty[indexOfLatest]);

//             return Array.IndexOf(modeList, difficulty[indexOfLatest]);
//         }
//         else
//         {
//             Debug.Log("No " + logPath + " for this user");

//             return 0;
//         }
//     }

//     public static string getLastMotorSpin(string game, string userName)
//     {
//         string logPath = @"log/" + game + "/" + userName + ".csv";

//         // clear all elements in list first
//         date.Clear();
//         motorSpin.Clear();

//         if (File.Exists(logPath))
//         {
//             using (var ftr = new FileStream(logPath, FileMode.Open))
//             using (var sr = new StreamReader(ftr))
//             using (var rd = new CsvReader(sr))
//             {
//                 while (rd.Read())
//                 {
//                     var eachDate = rd.GetField<string>("date");
//                     var eachMotorSpin = rd.GetField<string>("motor_spin");

//                     date.Add(DateTime.Parse(eachDate));
//                     motorSpin.Add(eachMotorSpin);
//                 }
//                 // close all
//                 rd.Dispose();
//                 sr.Close();
//                 ftr.Close();
//             }

//             int indexOfLatest = date.IndexOf(date.Max());

//             return motorSpin[indexOfLatest];
//         }
//         else
//         {
//             Debug.Log("No " + logPath + " for this user");

//             return "med";
//         }
//     }

//     public static bool getLastAlwaysMove(string game, string userName)
//     {
//         string logPath = @"log/" + game + "/" + userName + ".csv";

//         // clear all elements in list first
//         date.Clear();
//         alwaysMove.Clear();

//         if (File.Exists(logPath))
//         {
//             using (var ftr = new FileStream(logPath, FileMode.Open))
//             using (var sr = new StreamReader(ftr))
//             using (var rd = new CsvReader(sr))
//             {
//                 while (rd.Read())
//                 {
//                     var eachDate = rd.GetField<string>("date");
//                     var eachAlwaysMove = rd.GetField<string>("always_move");

//                     date.Add(DateTime.Parse(eachDate));
//                     alwaysMove.Add(bool.Parse(eachAlwaysMove));
//                 }
//                 // close all
//                 rd.Dispose();
//                 sr.Close();
//                 ftr.Close();
//             }

//             int indexOfLatest = date.IndexOf(date.Max());

//             // return alwaysMove[indexOfLatest];
//             return false;
//         }
//         else
//         {
//             Debug.Log("No " + logPath + " for this user");

//             return false;
//         }
//     }

//     public static float getCognitiveSpeed(string userName)
//     {
//         string logPath = @"log/cognitiveRun/" + userName + ".csv";
//         List<float> speed = new List<float>();

//         // clear all elements in list first
//         date.Clear();

//         if (File.Exists(logPath))
//         {
//             using (var ftr = new FileStream(logPath, FileMode.Open))
//             using (var sr = new StreamReader(ftr))
//             using (var rd = new CsvReader(sr))
//             {
//                 while (rd.Read())
//                 {
//                     var eachDate = rd.GetField<string>("date");
//                     var eachSpeed = rd.GetField<float>("speed");

//                     date.Add(DateTime.Parse(eachDate));
//                     speed.Add(eachSpeed);
//                 }
//                 // close all
//                 rd.Dispose();
//                 sr.Close();
//                 ftr.Close();
//             }

//             int indexOfLatest = date.IndexOf(date.Max());

//             return speed[indexOfLatest];
//         }
//         else
//         {
//             Debug.Log("No " + logPath + " for this user");

//             return 0.1f;
//         }
//     }
// }
