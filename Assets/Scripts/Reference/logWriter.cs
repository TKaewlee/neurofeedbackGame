// using UnityEngine;
// using System.Collections.Generic;
// using System.IO;
// using CsvHelper;

// public class LogWriter : MonoBehaviour
// {

//     public static class GameLogData
//     {
//         private static string gameDir;
//         private static string gameCsv;

//         public static Dictionary<string, string> dataCollector = new Dictionary<string, string>();

//         public static void updateGamePath()
//         {
//             gameDir = @"log/" + LogCollector.last_game + "/";
//             // gameCsv = @"log/" + LogCollector.last_game + "/" + GameControl.currentUserId + ".csv";
//         }

//         public static void getData()
//         {
//             dataCollector.Clear();
//             dataCollector = LogCollector.getLogDictionary(LogCollector.last_game);
//         }

//         public static void writeData()
//         {
//             // check if directory exist
//             if (!Directory.Exists(gameDir))
//             {
//                 Directory.CreateDirectory(gameDir);
//                 Debug.Log("Create log/" + LogCollector.last_game + "/ in home directory!");
//             }
//             // check if file exists, if not create with header
//             if (!File.Exists(gameCsv))
//             {
//                 // create new csv file
//                 using (var ftw = new FileStream(gameCsv, FileMode.Append))
//                 using (var sw = new StreamWriter(ftw))
//                 using (var wt = new CsvWriter(sw))
//                 {
//                     foreach (string key in dataCollector.Keys)
//                     {
//                         wt.WriteField(key);
//                     }

//                     wt.NextRecord();

//                     // close all
//                     wt.Dispose();
//                     sw.Close();
//                     ftw.Close();
//                 }

//                 Debug.Log("File at path '" + gameCsv + "' is created with headers.");
//             }

//             using (var ftw = new FileStream(gameCsv, FileMode.Append))
//             using (var sw = new StreamWriter(ftw))
//             using (var wt = new CsvWriter(sw))
//             {
//                 foreach (string val in dataCollector.Values)
//                 {
//                     wt.WriteField(val);
//                 }

//                 wt.NextRecord();

//                 // close all
//                 wt.Dispose();
//                 sw.Close();
//                 ftw.Close();
//             }
//         }

//         public static void clearData()
//         {
//             dataCollector.Clear();
//             LogCollector.clearStaticLogs();
//         }

//     }
// }
