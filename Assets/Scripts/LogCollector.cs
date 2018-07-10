// using UnityEngine;
// //using System.Collections;
// // for this script
// //using UnityEngine.UI; // ui objects
// // using CsvHelper; // csv write and read
// // using System.IO; // file stream
// using System.Collections.Generic; // list
// // date time
// using System;
// // using System.Linq;
// // to write
// // using System.Text;
// using UnityEngine.SceneManagement;

// public class LogCollector : MonoBehaviour
// {
//     // declare variable to store latest played game
//     public static string last_game;
//     //public static bool TO_WRITE_SPACESHOOTER = false;

//     #region main events
//     // declare singleton
//     public static LogCollector logCollector;

//     // logWriter is the log collector and writer thus it needs to be singleton like gamecontrol
//     // it will hold every game log of every game and reset once all written
//     void Awake()
//     {
//         if (logCollector == null)
//         {
//             DontDestroyOnLoad(gameObject);
//             logCollector = this;
//         }
//         else if (logCollector != this)
//         {
//             Destroy(gameObject);
//             Debug.Log("There is LogWriter from last scene, LogWriter in _" + SceneManager.GetActiveScene().name + "_ is killed.");
//         }
//     }

//     // Use this for initialization
//     void Start()
//     {

//     }

//     // Update is called once per frame
//     void Update()
//     {
//         //if (TO_WRITE_SPACESHOOTER) {
//         //	// write data for space shooter
//         //	writeSpaceShooter ();
//         //	Debug.Log ("space shooter csv is written!");
//         //	TO_WRITE_SPACESHOOTER = false;
//         //}
//     }
//     #endregion

//     #region shared variables and settings
//     public static string difficulty; // easy medium hard
//     public static string motorSpin; // off med max
//     public static bool alwaysMove; // true false

//     public static float final_duration; // duration of each game play
//     public static Dictionary<string, int> score_collector = new Dictionary<string, int>();

//     // method to reset shared variables
//     static void resetSharedVars()
//     {
//         difficulty = "easy";
//         motorSpin = "med";
//         alwaysMove = true;
//         final_duration = 0f;
//     }
//     #endregion

//     #region games variables and settings
//     //// declare game log variables before writing
//     // spaceShooter
//     public static List<float> timeUsedEachQuestion_SpaceShooter = new List<float>();
//     public static int score_SpaceShooter;
//     public static string operator_SpaceShooter = "plus";
//     public static int noDummy_SpaceShooter = 0;
//     public static List<string> correctWrongList_SpaceShooter = new List<string>();
//     public static int correct_SpaceShooter;
//     public static int wrong_SpaceShooter;
//     public static int asteroidHit_SpaceShooter;
//     public static float answerSpeed_SpaceShooter = 0.5f;
//     public static float fireRate_SpaceShooter = 1.5f;
//     public static int maxNo_SpaceShooter = 10;

//     // method to clear log variables for spaceShooter
//     static void resetSpaceShooter()
//     {
//         timeUsedEachQuestion_SpaceShooter.Clear();
//         score_SpaceShooter = 0;
//         operator_SpaceShooter = "plus";
//         noDummy_SpaceShooter = 0;
//         correctWrongList_SpaceShooter.Clear();
//         correct_SpaceShooter = 0;
//         wrong_SpaceShooter = 0;
//         asteroidHit_SpaceShooter = 0;
//         answerSpeed_SpaceShooter = 0.5f;
//         fireRate_SpaceShooter = 1.5f;
//         maxNo_SpaceShooter = 10;
//     }

//     // cognitiveRun
//     public static int score_cognitive;
//     public static List<string> correctWrongList_cognitive = new List<string>();
//     public static int correct_cognitive;
//     public static int wrong_cognitive;
//     public static string mode_cognitive;
//     public static float speed_cognitive;

//     // method to clear log variables for cognitiveRun
//     static void resetCognitiveRun()
//     {
//         score_cognitive = 0;
//         correctWrongList_cognitive.Clear();
//         correct_cognitive = 0;
//         wrong_cognitive = 0;
//         // mode_cognitive; // do nothing
//         // speed_cognitive; // do nothing
//     }

//     // tetris
//     public static int score_tetris;
//     public static int dead_tetris;
//     public static int row_tetris;
//     public static List<int> combo_tetris = new List<int>();
//     public static float speed_tetris;

//     // method to clear log variables for tetris
//     static void resetTetris()
//     {
//         score_tetris = 0;
//         dead_tetris = 0;
//         row_tetris = 0;
//         combo_tetris.Clear();
//         // speed_tetris; // do nothing
//     }

//     // gdrive
//     public static int score_gdrive;
//     public static int count_coin_gdrive;
//     public static int keep_coin_gdrive;
//     public static int miss_coin_gdrive;

//     // method to clear log variables for gdrive
//     static void resetGdrive()
//     {
//         score_gdrive = 0;
//         count_coin_gdrive = 0;
//         keep_coin_gdrive = 0;
//         miss_coin_gdrive = 0;
//     }

//     // gmath
//     public static List<float> timeUsedEachQuestion_gmath = new List<float>();
//     //public static float duration_gmath;
//     public static int score_gmath;
//     public static List<string> correctWrongList_gmath = new List<string>();
//     public static int correct_gmath;
//     public static int wrong_gmath;

//     // method to clear log variables for gmath
//     static void resetGmath()
//     {
//         timeUsedEachQuestion_gmath.Clear();
//         score_gmath = 0;
//         correctWrongList_gmath.Clear();
//         correct_gmath = 0;
//         wrong_gmath = 0;
//     }

//     // gmatch
//     public static List<float> timeUsedEachQuestion_gmatch = new List<float>();
//     //public static float duration_gmatch;
//     public static int score_gmatch;
//     public static List<string> correctWrongList_gmatch = new List<string>();
//     public static int correct_gmatch;
//     public static int wrong_gmatch;

//     // method to clear log variables for gmatch
//     static void resetGmatch()
//     {
//         timeUsedEachQuestion_gmatch.Clear();
//         score_gmatch = 0;
//         correctWrongList_gmatch.Clear();
//         correct_gmatch = 0;
//         wrong_gmatch = 0;
//     }

//     // ordering
//     public static int score_ordering;
//     public static List<float> timeUsedPerItem_ordering = new List<float>();
//     public static float avgTimeUsedPerItem_ordering;
//     public static List<float> timeUsedPerQuestion_ordering = new List<float>();
//     public static float avgTimeUsedPerQuestion_ordering;
//     public static List<string> correctWrongList_ordering = new List<string>();
//     public static int correct_ordering;
//     public static int wrong_ordering;
//     public static List<string> correctOrder_ordering = new List<string>();
//     public static List<string> wrongOrder_ordering = new List<string>();

//     // method to clear log variables for ordering
//     static void resetOrdering()
//     {
//         score_ordering = 0;
//         timeUsedPerItem_ordering.Clear();
//         avgTimeUsedPerItem_ordering = 0f;
//         avgTimeUsedPerQuestion_ordering = 0f;
//         correctWrongList_ordering.Clear();
//         correct_ordering = 0;
//         wrong_ordering = 0;
//         correctOrder_ordering.Clear();
//         wrongOrder_ordering.Clear();
//     }

//     // grouping
//     public static int score_grouping;
//     public static List<float> timeUsedPerItem_grouping = new List<float>();
//     public static float avgTimeUsedPerItem_grouping;
//     public static List<float> timeUsedPerQuestion_grouping = new List<float>();
//     public static float avgTimeUsedPerQuestion_grouping;
//     public static List<string> correctWrongList_grouping = new List<string>();
//     public static int correct_grouping;
//     public static int wrong_grouping;
//     public static List<string> correctCat_grouping = new List<string>();
//     public static List<string> wrongCat_grouping = new List<string>();

//     // method to clear log variables for grouping
//     static void resetGrouping()
//     {
//         score_grouping = 0;
//         timeUsedPerItem_grouping.Clear();
//         avgTimeUsedPerItem_grouping = 0f;
//         timeUsedPerQuestion_grouping.Clear();
//         avgTimeUsedPerQuestion_grouping = 0f;
//         correctWrongList_grouping.Clear();
//         correct_grouping = 0;
//         wrong_grouping = 0;
//         correctCat_grouping.Clear();
//         wrongCat_grouping.Clear();
//     }

//     // matching
//     public static int score_matching;
//     //public static List<float> timeUsedPerItem_matching = new List<float>();
//     //public static float avgTimeUsedPerItem_matching;
//     public static List<float> timeUsedPerQuestion_matching = new List<float>();
//     public static float avgTimeUsedPerQuestion_matching;
//     public static List<string> correctWrongList_matching = new List<string>();
//     public static int correct_matching;
//     public static int wrong_matching;

//     // method to clear log variables for matching
//     static void resetMatching()
//     {
//         score_matching = 0;
//         timeUsedPerQuestion_matching.Clear();
//         avgTimeUsedPerQuestion_matching = 0f;
//         correctWrongList_matching.Clear();
//         correct_matching = 0;
//         wrong_matching = 0;
//     }
//     #endregion

//     #region current used methods
//     // this method is for reportScoreController.cs
//     public static void addScoreToCollector()
//     {
//         score_collector.Clear();

//         score_collector["spaceShooter"] = score_SpaceShooter;
//         score_collector["cognitiveRun"] = score_cognitive;
//         score_collector["tetris"] = score_tetris;
//         score_collector["gdrive"] = score_gdrive;
//         score_collector["gmath"] = score_gmath;
//         score_collector["gmatch"] = score_gmatch;
//         score_collector["ordering"] = score_ordering;
//         score_collector["grouping"] = score_grouping;
//         score_collector["matching"] = score_matching;

//         Debug.Log("scores have been add to dictionary!");
//     }

//     // this method is for clearing all the static data/ log variables of all games at once (should be called right after writing to log files)
//     public static void clearStaticLogs()
//     {
//         resetSpaceShooter();
//         resetCognitiveRun();
//         resetTetris();
//         resetGdrive();
//         resetGmath();
//         resetGmatch();
//         resetOrdering();
//         resetGrouping();
//         resetMatching();
//     }

//     // this method is for logWriter.cs to get dictionary of log data and use it to write to log files
//     public static Dictionary<string, string> getLogDictionary(string gameName)
//     {
//         Dictionary<string, string> temp = new Dictionary<string, string>();

//         switch (gameName)
//         {
//             case "spaceShooter":
//                 temp["date"] = DateTime.Now.ToString();
//                 temp["duration"] = final_duration.ToString("f2");
//                 temp["playtime"] = pauseScript.playtimeSet.ToString("f2");

//                 temp["max_value"] = maxNo_SpaceShooter.ToString();
//                 temp["operator"] = operator_SpaceShooter;
//                 temp["time_used"] = Utility.getAppendString(timeUsedEachQuestion_SpaceShooter);
//                 temp["avg_time_used"] = Utility.getAverageOfFloatList(timeUsedEachQuestion_SpaceShooter);
//                 temp["dummies"] = noDummy_SpaceShooter.ToString();
//                 temp["correctWrong_list"] = Utility.getAppendString(correctWrongList_SpaceShooter);
//                 temp["correct"] = correct_SpaceShooter.ToString();
//                 temp["wrong"] = wrong_SpaceShooter.ToString();
//                 temp["asteroid_hit"] = asteroidHit_SpaceShooter.ToString("f1");
//                 temp["answer_speed"] = answerSpeed_SpaceShooter.ToString("f1");
//                 temp["firerate"] = fireRate_SpaceShooter.ToString();
//                 temp["score"] = score_SpaceShooter.ToString();

//                 temp["difficulty"] = difficulty;
//                 temp["motor_spin"] = motorSpin;
//                 temp["always_move"] = alwaysMove.ToString();
//                 break;

//             case "cognitiveRun":
//                 temp["date"] = DateTime.Now.ToString();
//                 temp["duration"] = final_duration.ToString("f2");
//                 temp["playtime"] = pauseScript.playtimeSet.ToString("f2");

//                 temp["mode"] = mode_cognitive;
//                 temp["speed"] = speed_cognitive.ToString();
//                 temp["correctWrong_list"] = Utility.getAppendString(correctWrongList_cognitive);
//                 temp["correct"] = correct_cognitive.ToString();
//                 temp["wrong"] = wrong_cognitive.ToString();
//                 temp["score"] = score_cognitive.ToString();

//                 temp["difficulty"] = difficulty;
//                 temp["motor_spin"] = motorSpin;
//                 temp["always_move"] = alwaysMove.ToString();
//                 break;

//             case "tetris":
//                 temp["date"] = DateTime.Now.ToString();
//                 temp["duration"] = final_duration.ToString("f2");
//                 temp["playtime"] = pauseScript.playtimeSet.ToString("f2");

//                 temp["speed"] = speed_tetris.ToString();
//                 temp["row"] = row_tetris.ToString();
//                 temp["combo"] = Utility.getAppendString(combo_tetris);
//                 temp["dead"] = dead_tetris.ToString();
//                 temp["score"] = score_tetris.ToString();

//                 temp["difficulty"] = difficulty;
//                 temp["motor_spin"] = motorSpin;
//                 temp["always_move"] = alwaysMove.ToString();
//                 break;

//             case "gdrive":
//                 temp["date"] = DateTime.Now.ToString();
//                 temp["duration"] = final_duration.ToString("f2");
//                 temp["playtime"] = pauseScript.playtimeSet.ToString("f2");

//                 temp["count_coin"] = count_coin_gdrive.ToString();
//                 temp["keep_coin"] = keep_coin_gdrive.ToString();
//                 temp["miss_coin"] = miss_coin_gdrive.ToString();
//                 temp["is_yaw_on"] = gdriveController.isYawOn.ToString();
//                 temp["is_roll_on"] = gdriveController.isRollOn.ToString();
//                 temp["score"] = score_gdrive.ToString();

//                 temp["difficulty"] = difficulty;
//                 temp["motor_spin"] = motorSpin;
//                 temp["always_move"] = alwaysMove.ToString();
//                 break;

//             case "gmath":
//                 temp["date"] = DateTime.Now.ToString();
//                 temp["duration"] = final_duration.ToString("f2");
//                 temp["playtime"] = pauseScript.playtimeSet.ToString("f2");

//                 temp["time_used"] = Utility.getAppendString(timeUsedEachQuestion_gmath);
//                 temp["avg_time_used"] = Utility.getAverageOfFloatList(timeUsedEachQuestion_gmath);
//                 temp["correctWrong_list"] = Utility.getAppendString(correctWrongList_gmath);
//                 temp["correct"] = correct_gmath.ToString();
//                 temp["wrong"] = wrong_gmath.ToString();
//                 temp["is_yaw_on"] = gmathController.isYawOn.ToString();
//                 temp["is_roll_on"] = gmathController.isRollOn.ToString();
//                 temp["score"] = score_gmath.ToString();

//                 temp["difficulty"] = difficulty;
//                 temp["motor_spin"] = motorSpin;
//                 temp["always_move"] = alwaysMove.ToString();
//                 break;

//             case "gmatch":
//                 temp["date"] = DateTime.Now.ToString();
//                 temp["duration"] = final_duration.ToString("f2");
//                 temp["playtime"] = pauseScript.playtimeSet.ToString("f2");

//                 temp["time_used"] = Utility.getAppendString(timeUsedEachQuestion_gmatch);
//                 temp["avg_time_used"] = Utility.getAverageOfFloatList(timeUsedEachQuestion_gmatch);
//                 temp["correctWrong_list"] = Utility.getAppendString(correctWrongList_gmatch);
//                 temp["correct"] = correct_gmatch.ToString();
//                 temp["wrong"] = wrong_gmatch.ToString();
//                 temp["is_yaw_on"] = gmatchController.isYawOn.ToString();
//                 temp["is_roll_on"] = gmatchController.isRollOn.ToString();
//                 temp["score"] = score_gmatch.ToString();

//                 temp["difficulty"] = difficulty;
//                 temp["motor_spin"] = motorSpin;
//                 temp["always_move"] = alwaysMove.ToString();
//                 break;

//             case "ordering":
//                 temp["date"] = DateTime.Now.ToString();
//                 temp["duration"] = final_duration.ToString("f2");
//                 temp["playtime"] = pauseScript.playtimeSet.ToString("f2");

//                 temp["time_per_item"] = Utility.getAppendString(timeUsedPerItem_ordering);
//                 temp["avg_time_per_item"] = Utility.getAverageOfFloatList(timeUsedPerItem_ordering);
//                 temp["time_per_question"] = Utility.getAppendString(timeUsedPerQuestion_ordering);
//                 temp["avg_time_per_question"] = Utility.getAverageOfFloatList(timeUsedPerQuestion_ordering);
//                 temp["correctWrong_list"] = Utility.getAppendString(correctWrongList_ordering);
//                 temp["correct_item"] = correct_ordering.ToString();
//                 temp["wrong_item"] = wrong_ordering.ToString();
//                 temp["correct_order"] = Utility.getAppendString(correctOrder_ordering);
//                 temp["wrong_order"] = Utility.getAppendString(wrongOrder_ordering);

//                 temp["is_yaw_on"] = orderingController.isYawOn.ToString();
//                 temp["is_roll_on"] = orderingController.isRollOn.ToString();
//                 temp["score"] = score_ordering.ToString();

//                 temp["difficulty"] = difficulty;
//                 temp["motor_spin"] = motorSpin;
//                 temp["always_move"] = alwaysMove.ToString();
//                 break;

//             case "grouping":
//                 temp["date"] = DateTime.Now.ToString();
//                 temp["duration"] = final_duration.ToString("f2");
//                 temp["playtime"] = pauseScript.playtimeSet.ToString("f2");

//                 temp["time_per_item"] = Utility.getAppendString(timeUsedPerItem_grouping);
//                 temp["avg_time_per_item"] = Utility.getAverageOfFloatList(timeUsedPerItem_grouping);
//                 temp["time_per_question"] = Utility.getAppendString(timeUsedPerQuestion_grouping);
//                 temp["avg_time_per_question"] = Utility.getAverageOfFloatList(timeUsedPerQuestion_grouping);
//                 temp["correctWrong_list"] = Utility.getAppendString(correctWrongList_grouping);
//                 temp["correct_item"] = correct_grouping.ToString();
//                 temp["wrong_item"] = wrong_grouping.ToString();
//                 temp["correct_category"] = Utility.getAppendString(correctCat_grouping);
//                 temp["wrong_category"] = Utility.getAppendString(wrongCat_grouping);
//                 temp["score"] = score_grouping.ToString();

//                 temp["difficulty"] = difficulty;
//                 temp["motor_spin"] = motorSpin;
//                 temp["always_move"] = alwaysMove.ToString();
//                 break;

//             case "matching":
//                 temp["date"] = DateTime.Now.ToString();
//                 temp["duration"] = final_duration.ToString("f2");
//                 temp["playtime"] = pauseScript.playtimeSet.ToString("f2");

//                 temp["time_per_question"] = Utility.getAppendString(timeUsedPerQuestion_matching);
//                 temp["avg_time_per_question"] = Utility.getAverageOfFloatList(timeUsedPerQuestion_matching);
//                 temp["correctWrong_list"] = Utility.getAppendString(correctWrongList_matching);
//                 temp["correct_item"] = correct_matching.ToString();
//                 temp["wrong_item"] = wrong_matching.ToString();

//                 temp["is_yaw_on"] = matchingController.isYawOn.ToString();
//                 temp["is_roll_on"] = matchingController.isRollOn.ToString();
//                 temp["score"] = score_matching.ToString();

//                 temp["difficulty"] = difficulty;
//                 temp["motor_spin"] = motorSpin;
//                 temp["always_move"] = alwaysMove.ToString();
//                 break;
//         }

//         return temp;

//     }
//     #endregion
// }




// ///////////////
// // Archived - will be depricated soon 
// ///////////////

// //     #region spaceShooter
// //     // //// declare game log variables before writing
// //     // //SpaceShooter
// //     // //public static DateTime start_SpaceShooter;
// //     // public static List<float> timeUsedEachQuestion_SpaceShooter = new List<float>();
// //     // //public static float duration_SpaceShooter;
// //     // public static int score_SpaceShooter;
// //     // public static string operator_SpaceShooter = "plus";
// //     // public static int noDummy_SpaceShooter = 0;
// //     // public static List<string> correctWrongList_SpaceShooter = new List<string>();
// //     // public static int correct_SpaceShooter;
// //     // public static int wrong_SpaceShooter;
// //     // public static int asteroidHit_SpaceShooter;
// //     // public static float answerSpeed_SpaceShooter = 0.5f;
// //     // public static float fireRate_SpaceShooter = 1.5f;
// //     // public static int maxNo_SpaceShooter = 10;
// //     private string spaceShooterDir = @"log/spaceShooter/";
// //     private string spaceShooterCsv = @"log/spaceShooter/" + GameControl.currentUserId + ".csv";

// //     public void writeSpaceShooter()
// //     {
// //         // check if directory exist
// //         if (!Directory.Exists(spaceShooterDir))
// //         {
// //             Directory.CreateDirectory(spaceShooterDir);
// //             Debug.Log("Create log/spaceShooter/ in home directory!");
// //         }
// //         // check if file exists, if not create with header
// //         if (!File.Exists(spaceShooterCsv))
// //         {
// //             // create new csv file
// //             using (var ftw = new FileStream(spaceShooterCsv, FileMode.Append))
// //             using (var sw = new StreamWriter(ftw))
// //             using (var wt = new CsvWriter(sw))
// //             {
// //                 // write headers
// //                 wt.WriteField("date"); // system datetime
// //                 wt.WriteField("duration"); // from timeController
// //                 wt.WriteField("playtime"); // playtimeset from pauseScript
// //                 wt.WriteField("max_value"); // from question controller
// //                 wt.WriteField("operator"); // from question controller (game settings)
// //                 wt.WriteField("time_used"); // from destroy answer by appending to the list after each shot
// //                 wt.WriteField("avg_time_used"); // avg value of above
// //                 wt.WriteField("dummies"); // from question controller (game settings)
// //                 wt.WriteField("correctWrong_list"); // from destroy answer by subscribe after each answer 
// //                 wt.WriteField("correct"); // same as above
// //                 wt.WriteField("wrong"); // same as above
// //                 wt.WriteField("asteroid_hit"); // same as above
// //                 wt.WriteField("answer_speed"); // from question controller
// //                 wt.WriteField("firerate"); // same as above
// //                 wt.WriteField("score"); // from spaceShooter controller

// //                 // shared variables
// //                 wt.WriteField("difficulty");
// //                 wt.WriteField("motor_spin");
// //                 wt.WriteField("always_move");

// //                 wt.NextRecord();

// //                 // close all
// //                 wt.Dispose();
// //                 sw.Close();
// //                 ftw.Close();
// //             }

// //             Debug.Log("File at path '" + spaceShooterCsv + "' is created with headers.");
// //         }

// //         float avgTimeUsed = 0;
// //         foreach (float i in timeUsedEachQuestion_SpaceShooter)
// //         {
// //             avgTimeUsed += i;
// //         }
// //         avgTimeUsed = avgTimeUsed / timeUsedEachQuestion_SpaceShooter.Count;

// //         StringBuilder strTime = new StringBuilder();
// //         foreach (float i in timeUsedEachQuestion_SpaceShooter)
// //         {
// //             strTime.Append(i.ToString("f2")).Append(" ");
// //         }
// //         StringBuilder strCWList = new StringBuilder(); // correct wrong list
// //         foreach (string i in correctWrongList_SpaceShooter)
// //         {
// //             strCWList.Append(i).Append(" ");
// //         }

// //         using (var ftw = new FileStream(spaceShooterCsv, FileMode.Append))
// //         using (var sw = new StreamWriter(ftw))
// //         using (var wt = new CsvWriter(sw))
// //         {
// //             // write headers
// //             wt.WriteField(DateTime.Now.ToString()); // date
// //             wt.WriteField(final_duration.ToString("f2")); // duration
// //             wt.WriteField(pauseScript.playtimeSet.ToString("f2")); // playtimeset
// //             wt.WriteField(maxNo_SpaceShooter); // max value
// //             wt.WriteField(operator_SpaceShooter); // operator
// //             wt.WriteField(strTime.ToString()); // time use
// //             wt.WriteField(avgTimeUsed.ToString("f2")); // avg time use
// //             wt.WriteField(noDummy_SpaceShooter); // dummies
// //             wt.WriteField(strCWList.ToString()); // correct wrong list
// //             wt.WriteField(correct_SpaceShooter); // correct
// //             wt.WriteField(wrong_SpaceShooter); // wrong
// //             wt.WriteField(asteroidHit_SpaceShooter); // asteroid hit
// //             wt.WriteField(answerSpeed_SpaceShooter.ToString("f1")); // answer speed
// //             wt.WriteField(fireRate_SpaceShooter.ToString("f1")); // firerate
// //             wt.WriteField(score_SpaceShooter); // score

// //             // shared variables
// //             wt.WriteField(difficulty);
// //             wt.WriteField(motorSpin);
// //             wt.WriteField(alwaysMove);

// //             wt.NextRecord();

// //             // close all
// //             wt.Dispose();
// //             sw.Close();
// //             ftw.Close();
// //         }

// //         resetSpaceShooter();
// //         resetSharedVars();
// //     }

// //     // reset all variables after they are written
// //     void resetSpaceShooter()
// //     {
// //         timeUsedEachQuestion_SpaceShooter = new List<float>();
// //         //duration_SpaceShooter = 0f; 
// //         score_SpaceShooter = 0;
// //         operator_SpaceShooter = "plus";
// //         noDummy_SpaceShooter = 0;
// //         correctWrongList_SpaceShooter = new List<string>();
// //         correct_SpaceShooter = 0;
// //         wrong_SpaceShooter = 0;
// //         asteroidHit_SpaceShooter = 0;
// //         answerSpeed_SpaceShooter = 0.5f;
// //         fireRate_SpaceShooter = 1.5f;
// //         maxNo_SpaceShooter = 10;
// //     }
// //     #endregion

// //     #region cognitive run
// //     //// declare game log variables before writing
// //     //Cognitive run
// //     //public static float duration_cognitive;
// //     // public static int score_cognitive;
// //     // public static List<string> correctWrongList_cognitive = new List<string>();
// //     // public static int correct_cognitive;
// //     // public static int wrong_cognitive;
// //     // public static string mode_cognitive;
// //     // public static float speed_cognitive;
// //     private string cognitiveDir = @"log/cognitiveRun/";
// //     private string cognitiveCsv = @"log/cognitiveRun/" + GameControl.currentUserId + ".csv";

// //     public void writeCognitiveRun()
// //     {
// //         // check if directory exist
// //         if (!Directory.Exists(cognitiveDir))
// //         {
// //             Directory.CreateDirectory(cognitiveDir);
// //             Debug.Log("Create log/cognitiveDir/ in home directory!");
// //         }
// //         // check if file exists, if not create with header
// //         if (!File.Exists(cognitiveCsv))
// //         {
// //             // create new csv file
// //             using (var ftw = new FileStream(cognitiveCsv, FileMode.Append))
// //             using (var sw = new StreamWriter(ftw))
// //             using (var wt = new CsvWriter(sw))
// //             {
// //                 // write headers
// //                 wt.WriteField("date"); // system datetime
// //                 wt.WriteField("duration"); // from timeController (get auto)
// //                 wt.WriteField("playtime"); // playtimeset from pauseScript
// //                 wt.WriteField("mode"); // from cognitiveRun Controller
// //                 wt.WriteField("speed"); // same as above
// //                 wt.WriteField("correctWrong_list"); // from destroy cognitive by subscribe after each answer 
// //                 wt.WriteField("correct"); // same as above
// //                 wt.WriteField("wrong"); // same as above
// //                 wt.WriteField("score"); // from cognitiveRun controller

// //                 // shared variables
// //                 wt.WriteField("difficulty");
// //                 wt.WriteField("motor_spin");
// //                 wt.WriteField("always_move");

// //                 wt.NextRecord();

// //                 // close all
// //                 wt.Dispose();
// //                 sw.Close();
// //                 ftw.Close();
// //             }

// //             Debug.Log("File at path '" + cognitiveCsv + "' is created with headers.");
// //         }

// //         StringBuilder strCWList = new StringBuilder(); // correct wrong list
// //         foreach (string i in correctWrongList_cognitive)
// //         {
// //             strCWList.Append(i).Append(" ");
// //         }

// //         using (var ftw = new FileStream(cognitiveCsv, FileMode.Append))
// //         using (var sw = new StreamWriter(ftw))
// //         using (var wt = new CsvWriter(sw))
// //         {
// //             // write headers
// //             wt.WriteField(DateTime.Now.ToString()); // date
// //             wt.WriteField(final_duration.ToString("f2")); // duration
// //             wt.WriteField(pauseScript.playtimeSet.ToString("f2")); // playtimeset
// //             wt.WriteField(mode_cognitive); // mode
// //             wt.WriteField(speed_cognitive.ToString("f1")); // speed
// //             wt.WriteField(strCWList.ToString()); // correct wrong list
// //             wt.WriteField(correct_cognitive); // correct
// //             wt.WriteField(wrong_cognitive); // wrong
// //             wt.WriteField(score_cognitive); // score

// //             // shared variables
// //             wt.WriteField(difficulty);
// //             wt.WriteField(motorSpin);
// //             wt.WriteField(alwaysMove);

// //             wt.NextRecord();

// //             // close all
// //             wt.Dispose();
// //             sw.Close();
// //             ftw.Close();
// //         }

// //         resetCognitiveRun();
// //         resetSharedVars();
// //     }

// //     // reset all variables after they are written
// //     void resetCognitiveRun()
// //     {
// //         //duration_cognitive = 0;
// //         score_cognitive = 0;
// //         correctWrongList_cognitive = new List<string>();
// //         correct_cognitive = 0;
// //         wrong_cognitive = 0;
// //         mode_cognitive = "exact";
// //         //speed_cognitive = 0.1f;
// //     }
// //     #endregion

// //     #region tetris
// //     //// declare game log variables before writing
// //     //Tetris
// //     //public static float duration_tetris;
// //     // public static int score_tetris;
// //     // public static int dead_tetris;
// //     // public static int row_tetris;
// //     // public static List<int> combo_tetris = new List<int>();
// //     // public static float speed_tetris;
// //     private string tetrisDir = @"log/tetris/";
// //     private string tetrisCsv = @"log/tetris/" + GameControl.currentUserId + ".csv";

// //     public void writeTetris()
// //     {
// //         // check if directory exist
// //         if (!Directory.Exists(tetrisDir))
// //         {
// //             Directory.CreateDirectory(tetrisDir);
// //             Debug.Log("Create log/tetrisDir/ in home directory!");
// //         }
// //         // check if file exists, if not create with header
// //         if (!File.Exists(tetrisCsv))
// //         {
// //             // create new csv file
// //             using (var ftw = new FileStream(tetrisCsv, FileMode.Append))
// //             using (var sw = new StreamWriter(ftw))
// //             using (var wt = new CsvWriter(sw))
// //             {
// //                 // write headers
// //                 wt.WriteField("date"); // system datetime
// //                 wt.WriteField("duration"); // from timeController
// //                 wt.WriteField("playtime"); // playtimeset from pauseScript
// //                 wt.WriteField("speed"); // from tetris controller
// //                 wt.WriteField("row"); // row from grid
// //                 wt.WriteField("combo"); // from grid
// //                 wt.WriteField("dead"); // from tetrisPlayer
// //                 wt.WriteField("score"); // from tetris controller

// //                 // shared variables
// //                 wt.WriteField("difficulty");
// //                 wt.WriteField("motor_spin");
// //                 wt.WriteField("always_move");

// //                 wt.NextRecord();

// //                 // close all
// //                 wt.Dispose();
// //                 sw.Close();
// //                 ftw.Close();
// //             }

// //             Debug.Log("File at path '" + tetrisCsv + "' is created with headers.");
// //         }

// //         StringBuilder strComboList = new StringBuilder(); // correct wrong list
// //         foreach (int i in combo_tetris)
// //         {
// //             strComboList.Append(i).Append(" ");
// //         }

// //         using (var ftw = new FileStream(tetrisCsv, FileMode.Append))
// //         using (var sw = new StreamWriter(ftw))
// //         using (var wt = new CsvWriter(sw))
// //         {
// //             // write headers
// //             wt.WriteField(DateTime.Now.ToString()); // date
// //             wt.WriteField(final_duration.ToString("f2")); // duration
// //             wt.WriteField(pauseScript.playtimeSet.ToString("f2")); // playtimeset
// //             wt.WriteField(speed_tetris.ToString("f1")); // speed
// //             wt.WriteField(row_tetris); // row
// //             wt.WriteField(strComboList); // combo list
// //             wt.WriteField(dead_tetris / 2);
// //             wt.WriteField(score_tetris);

// //             // shared variables
// //             wt.WriteField(difficulty);
// //             wt.WriteField(motorSpin);
// //             wt.WriteField(alwaysMove);

// //             wt.NextRecord();

// //             // close all
// //             wt.Dispose();
// //             sw.Close();
// //             ftw.Close();
// //         }

// //         resetSharedVars();
// //         resetTetris();
// //     }

// //     // reset all variables after they are written
// //     void resetTetris()
// //     {
// //         //duration_tetris = 0;
// //         score_tetris = 0;
// //         dead_tetris = 0;
// //         row_tetris = 0;
// //         combo_tetris = new List<int>();
// //         speed_tetris = 1.0f;

// //     }
// //     #endregion

// //     #region gdrive
// //     //public static float duration_gdrive;
// //     // public static int score_gdrive;
// //     // public static int count_coin_gdrive;
// //     // public static int keep_coin_gdrive;
// //     // public static int miss_coin_gdrive;
// //     private string gdriveDir = @"log/gdrive/";
// //     private string gdriveCsv = @"log/gdrive/" + GameControl.currentUserId + ".csv";

// //     public void writeGdrive()
// //     {
// //         // check if directory exist
// //         if (!Directory.Exists(gdriveDir))
// //         {
// //             Directory.CreateDirectory(gdriveDir);
// //             Debug.Log("Create log/gdriveDir/ in home directory!");
// //         }
// //         // check if file exists, if not create with header
// //         if (!File.Exists(gdriveCsv))
// //         {
// //             // create new csv file
// //             using (var ftw = new FileStream(gdriveCsv, FileMode.Append))
// //             using (var sw = new StreamWriter(ftw))
// //             using (var wt = new CsvWriter(sw))
// //             {
// //                 // write headers
// //                 wt.WriteField("date"); // system datetime
// //                 wt.WriteField("duration"); // from timeController
// //                 wt.WriteField("playtime"); // playtimeset from pauseScript
// //                 wt.WriteField("count_coin"); // from coinSpawner
// //                 wt.WriteField("keep_coin"); // from coinDestroy
// //                 wt.WriteField("miss_coin"); // row coinDestroy
// //                 wt.WriteField("is_yaw_on"); // from gdriveController
// //                 wt.WriteField("is_roll_on"); // from gdriveController
// //                 wt.WriteField("score"); // from gdriveController

// //                 // shared variables
// //                 wt.WriteField("difficulty");
// //                 wt.WriteField("motor_spin");
// //                 wt.WriteField("always_move");

// //                 wt.NextRecord();

// //                 // close all
// //                 wt.Dispose();
// //                 sw.Close();
// //                 ftw.Close();
// //             }

// //             Debug.Log("File at path '" + gdriveCsv + "' is created with headers.");
// //         }

// //         using (var ftw = new FileStream(gdriveCsv, FileMode.Append))
// //         using (var sw = new StreamWriter(ftw))
// //         using (var wt = new CsvWriter(sw))
// //         {
// //             // write headers
// //             wt.WriteField(DateTime.Now.ToString()); // date
// //             wt.WriteField(final_duration.ToString("f2")); // duration
// //             wt.WriteField(pauseScript.playtimeSet.ToString("f2")); // playtimeset
// //             wt.WriteField(count_coin_gdrive.ToString()); // count coin
// //             wt.WriteField(keep_coin_gdrive.ToString()); // keep coin
// //             wt.WriteField(miss_coin_gdrive.ToString()); // miss coin
// //             wt.WriteField(gdriveController.isYawOn.ToString()); // is yaw on
// //             wt.WriteField(gdriveController.isRollOn.ToString()); // is roll on
// //             wt.WriteField(score_gdrive); // score

// //             // shared variables
// //             wt.WriteField(difficulty);
// //             wt.WriteField(motorSpin);
// //             wt.WriteField(alwaysMove);

// //             wt.NextRecord();

// //             // close all
// //             wt.Dispose();
// //             sw.Close();
// //             ftw.Close();
// //         }

// //         resetSharedVars();
// //         resetGdrive();
// //     }

// //     // reset all variables after they are written
// //     void resetGdrive()
// //     {
// //         //duration_gdrive = 0;
// //         score_gdrive = 0;
// //         count_coin_gdrive = 0;
// //         keep_coin_gdrive = 0;
// //         miss_coin_gdrive = 0;
// //     }
// //     #endregion

// //     #region gmath
// //     // public static List<float> timeUsedEachQuestion_gmath = new List<float>();
// //     // //public static float duration_gmath;
// //     // public static int score_gmath;
// //     // public static List<string> correctWrongList_gmath = new List<string>();
// //     // public static int correct_gmath;
// //     // public static int wrong_gmath;

// //     private string gmathDir = @"log/gmath/";
// //     private string gmathCsv = @"log/gmath/" + GameControl.currentUserId + ".csv";

// //     public void writeGmath()
// //     {
// //         // check if directory exist
// //         if (!Directory.Exists(gmathDir))
// //         {
// //             Directory.CreateDirectory(gmathDir);
// //             Debug.Log("Create log/gmathDir/ in home directory!");
// //         }
// //         // check if file exists, if not create with header
// //         if (!File.Exists(gmathCsv))
// //         {
// //             // create new csv file
// //             using (var ftw = new FileStream(gmathCsv, FileMode.Append))
// //             using (var sw = new StreamWriter(ftw))
// //             using (var wt = new CsvWriter(sw))
// //             {
// //                 // write headers
// //                 wt.WriteField("date"); // system datetime
// //                 wt.WriteField("duration"); // from timeController
// //                 wt.WriteField("playtime"); // playtimeset from pauseScript
// //                 wt.WriteField("time_used"); // from answerChecker by appending to the list after each correct answer
// //                 wt.WriteField("avg_time_used"); // avg value of above
// //                 wt.WriteField("correctWrong_list"); // from answerChecker by subscribe after each answer 
// //                 wt.WriteField("correct"); // from answerChecker
// //                 wt.WriteField("wrong"); // from answerChecker
// //                 wt.WriteField("is_yaw_on"); // from gmathController
// //                 wt.WriteField("is_roll_on"); // from gmathController
// //                 wt.WriteField("score"); // from gmathController

// //                 // shared variables
// //                 wt.WriteField("difficulty");
// //                 wt.WriteField("motor_spin");
// //                 wt.WriteField("always_move");

// //                 wt.NextRecord();

// //                 // close all
// //                 wt.Dispose();
// //                 sw.Close();
// //                 ftw.Close();
// //             }

// //             Debug.Log("File at path '" + gmathCsv + "' is created with headers.");
// //         }

// //         float avgTimeUsed = 0;
// //         foreach (float i in timeUsedEachQuestion_gmath)
// //         {
// //             avgTimeUsed += i;
// //         }
// //         avgTimeUsed = avgTimeUsed / timeUsedEachQuestion_gmath.Count;

// //         StringBuilder strTime = new StringBuilder();
// //         foreach (float i in timeUsedEachQuestion_gmath)
// //         {
// //             strTime.Append(i.ToString("f2")).Append(" ");
// //         }
// //         StringBuilder strCWList = new StringBuilder(); // correct wrong list
// //         foreach (string i in correctWrongList_gmath)
// //         {
// //             strCWList.Append(i).Append(" ");
// //         }

// //         using (var ftw = new FileStream(gmathCsv, FileMode.Append))
// //         using (var sw = new StreamWriter(ftw))
// //         using (var wt = new CsvWriter(sw))
// //         {
// //             wt.WriteField(DateTime.Now.ToString()); // date
// //             wt.WriteField(final_duration.ToString("f2")); // duration
// //             wt.WriteField(pauseScript.playtimeSet.ToString("f2")); // playtimeset
// //             wt.WriteField(strTime.ToString());
// //             wt.WriteField(avgTimeUsed.ToString("f2"));
// //             wt.WriteField(strCWList.ToString());
// //             wt.WriteField(correct_gmath.ToString()); // count coin
// //             wt.WriteField(wrong_gmath.ToString()); // keep coin
// //             wt.WriteField(gmathController.isYawOn.ToString()); // is yaw on
// //             wt.WriteField(gmathController.isRollOn.ToString()); // is roll on
// //             wt.WriteField(score_gmath); // score

// //             // shared variables
// //             wt.WriteField(difficulty);
// //             wt.WriteField(motorSpin);
// //             wt.WriteField(alwaysMove);

// //             wt.NextRecord();

// //             // close all
// //             wt.Dispose();
// //             sw.Close();
// //             ftw.Close();
// //         }

// //         resetSharedVars();
// //         resetGmath();
// //     }

// //     // reset all variables after they are written
// //     void resetGmath()
// //     {
// //         //duration_gmath = 0;
// //         score_gmath = 0;
// //         correct_gmath = 0;
// //         wrong_gmath = 0;
// //         timeUsedEachQuestion_gmath = new List<float>();
// //         correctWrongList_gmath = new List<string>();
// //     }
// //     #endregion

// //     #region gmatch
// //     // public static List<float> timeUsedEachQuestion_gmatch = new List<float>();
// //     // //public static float duration_gmatch;
// //     // public static int score_gmatch;
// //     // public static List<string> correctWrongList_gmatch = new List<string>();
// //     // public static int correct_gmatch;
// //     // public static int wrong_gmatch;

// //     private string gmatchDir = @"log/gmatch/";
// //     private string gmatchCsv = @"log/gmatch/" + GameControl.currentUserId + ".csv";

// //     public void writeGmatch()
// //     {
// //         // check if directory exist
// //         if (!Directory.Exists(gmatchDir))
// //         {
// //             Directory.CreateDirectory(gmatchDir);
// //             Debug.Log("Create log/gmatchDir/ in home directory!");
// //         }
// //         // check if file exists, if not create with header
// //         if (!File.Exists(gmatchCsv))
// //         {
// //             // create new csv file
// //             using (var ftw = new FileStream(gmatchCsv, FileMode.Append))
// //             using (var sw = new StreamWriter(ftw))
// //             using (var wt = new CsvWriter(sw))
// //             {
// //                 // write headers
// //                 wt.WriteField("date"); // system datetime
// //                 wt.WriteField("duration"); // from timeController
// //                 wt.WriteField("playtime"); // playtimeset from pauseScript
// //                 wt.WriteField("time_used"); // from answerChecker by appending to the list after each correct answer
// //                 wt.WriteField("avg_time_used"); // avg value of above
// //                 wt.WriteField("correctWrong_list"); // from answerChecker by subscribe after each answer 
// //                 wt.WriteField("correct"); // from answerChecker
// //                 wt.WriteField("wrong"); // from answerChecker
// //                 wt.WriteField("is_yaw_on"); // from gmatchController
// //                 wt.WriteField("is_roll_on"); // from gmatchController
// //                 wt.WriteField("score"); // from gmatchController

// //                 // shared variables
// //                 wt.WriteField("difficulty");
// //                 wt.WriteField("motor_spin");
// //                 wt.WriteField("always_move");

// //                 wt.NextRecord();

// //                 // close all
// //                 wt.Dispose();
// //                 sw.Close();
// //                 ftw.Close();
// //             }

// //             Debug.Log("File at path '" + gmatchCsv + "' is created with headers.");
// //         }

// //         float avgTimeUsed = 0;
// //         foreach (float i in timeUsedEachQuestion_gmatch)
// //         {
// //             avgTimeUsed += i;
// //         }
// //         avgTimeUsed = avgTimeUsed / timeUsedEachQuestion_gmatch.Count;

// //         StringBuilder strTime = new StringBuilder();
// //         foreach (float i in timeUsedEachQuestion_gmatch)
// //         {
// //             strTime.Append(i.ToString("f2")).Append(" ");
// //         }
// //         StringBuilder strCWList = new StringBuilder(); // correct wrong list
// //         foreach (string i in correctWrongList_gmatch)
// //         {
// //             strCWList.Append(i).Append(" ");
// //         }

// //         using (var ftw = new FileStream(gmatchCsv, FileMode.Append))
// //         using (var sw = new StreamWriter(ftw))
// //         using (var wt = new CsvWriter(sw))
// //         {
// //             wt.WriteField(DateTime.Now.ToString()); // date
// //             wt.WriteField(final_duration.ToString("f2")); // duration
// //             wt.WriteField(pauseScript.playtimeSet.ToString("f2")); // playtimeset
// //             wt.WriteField(strTime.ToString());
// //             wt.WriteField(avgTimeUsed.ToString("f2"));
// //             wt.WriteField(strCWList.ToString());
// //             wt.WriteField(correct_gmatch.ToString()); // count coin
// //             wt.WriteField(wrong_gmatch.ToString()); // keep coin
// //             wt.WriteField(gmatchController.isYawOn.ToString()); // is yaw on
// //             wt.WriteField(gmatchController.isRollOn.ToString()); // is roll on
// //             wt.WriteField(score_gmatch); // score

// //             // shared variables
// //             wt.WriteField(difficulty);
// //             wt.WriteField(motorSpin);
// //             wt.WriteField(alwaysMove);

// //             wt.NextRecord();

// //             // close all
// //             wt.Dispose();
// //             sw.Close();
// //             ftw.Close();
// //         }

// //         resetSharedVars();
// //         resetGmatch();
// //     }

// //     // reset all variables after they are written
// //     void resetGmatch()
// //     {
// //         //duration_gmatch = 0;
// //         score_gmatch = 0;
// //         correct_gmatch = 0;
// //         wrong_gmatch = 0;
// //         timeUsedEachQuestion_gmatch = new List<float>();
// //         correctWrongList_gmatch = new List<string>();
// //     }
// //     #endregion

// //     #region ordering
// //     // objects to be written on log
// //     // date, duration, playtime, time-used-per-item, avg-time-used-per-item, 
// //     // time-used-per-question, avg-time-used-per-question, correct-wrong-item, 
// //     // no-of-correct-item, no-of-wrong-item, correct-order, wrong-order,
// //     // is-yaw-on, is-roll-on, score, mode, motor-spin, always_move

// //     //public static float duration_gdrive;
// //     // public static int score_ordering;
// //     // public static List<float> timeUsedPerItem_ordering = new List<float>();
// //     // public static float avgTimeUsedPerItem_ordering;
// //     // public static List<float> timeUsedPerQuestion_ordering = new List<float>();
// //     // public static float avgTimeUsedPerQuestion_ordering;
// //     // public static List<string> correctWrongList_ordering = new List<string>();
// //     // public static int correct_ordering;
// //     // public static int wrong_ordering;
// //     // public static List<string> correctOrder_ordering = new List<string>();
// //     // public static List<string> wrongOrder_ordering = new List<string>();
// //     private string orderingDir = @"log/ordering/";
// //     private string orderingCsv = @"log/ordering/" + GameControl.currentUserId + ".csv";

// //     public void writeOrdering()
// //     {
// //         // check if directory exist
// //         if (!Directory.Exists(orderingDir))
// //         {
// //             Directory.CreateDirectory(orderingDir);
// //             Debug.Log("Create log/orderingDir/ in home directory!");
// //         }
// //         // check if file exists, if not create with header
// //         if (!File.Exists(orderingCsv))
// //         {
// //             // create new csv file
// //             using (var ftw = new FileStream(orderingCsv, FileMode.Append))
// //             using (var sw = new StreamWriter(ftw))
// //             using (var wt = new CsvWriter(sw))
// //             {
// //                 // date, duration, playtime, time-used-per-item, avg-time-used-per-item, 
// //                 // time-used-per-question, avg-time-used-per-question, correct-wrong-item, 
// //                 // no-of-correct-item, no-of-wrong-item, correct-order, wrong-order,
// //                 // is-yaw-on, is-roll-on, score, mode, motor-spin, always_move

// //                 // write headers
// //                 wt.WriteField("date"); // system datetime
// //                 wt.WriteField("duration"); // from timeController
// //                 wt.WriteField("playtime"); // playtimeset from pauseScript

// //                 wt.WriteField("time_per_item");
// //                 wt.WriteField("avg_time_per_item");
// //                 wt.WriteField("time_per_question");
// //                 wt.WriteField("avg_time_per_question");
// //                 wt.WriteField("correctWrong_list");
// //                 wt.WriteField("correct_item");
// //                 wt.WriteField("wrong_item");
// //                 wt.WriteField("correct_order");
// //                 wt.WriteField("wrong_order");

// //                 wt.WriteField("is_yaw_on"); // from gdriveController
// //                 wt.WriteField("is_roll_on"); // from gdriveController
// //                 wt.WriteField("score"); // from gdriveController

// //                 // shared variables
// //                 wt.WriteField("difficulty");
// //                 wt.WriteField("motor_spin");
// //                 wt.WriteField("always_move");

// //                 wt.NextRecord();

// //                 // close all
// //                 wt.Dispose();
// //                 sw.Close();
// //                 ftw.Close();
// //             }

// //             Debug.Log("File at path '" + orderingCsv + "' is created with headers.");
// //         }

// //         using (var ftw = new FileStream(orderingCsv, FileMode.Append))
// //         using (var sw = new StreamWriter(ftw))
// //         using (var wt = new CsvWriter(sw))
// //         {
// //             // write headers
// //             wt.WriteField(DateTime.Now.ToString()); // date
// //             wt.WriteField(final_duration.ToString("f2")); // duration
// //             wt.WriteField(pauseScript.playtimeSet.ToString("f2")); // playtimeset

// //             wt.WriteField(Utility.getAppendString(timeUsedPerItem_ordering));
// //             try
// //             {
// //                 wt.WriteField(timeUsedPerItem_ordering.Average().ToString("f2"));
// //             }
// //             catch
// //             {
// //                 wt.WriteField(string.Empty);
// //             }
// //             wt.WriteField(Utility.getAppendString(timeUsedPerQuestion_ordering));
// //             try
// //             {
// //                 wt.WriteField(timeUsedPerQuestion_ordering.Average().ToString("f2"));
// //             }
// //             catch
// //             {
// //                 wt.WriteField(string.Empty);
// //             }

// //             //wt.WriteField(Utility.getAppendString(correctWrongList_grouping));

// //             //wt.WriteField("time_per_item");
// //             //wt.WriteField("avg_time_per_item");
// //             //wt.WriteField("time_per_question");
// //             //wt.WriteField("avg_time_per_question");
// //             wt.WriteField(Utility.getAppendString(correctWrongList_ordering));
// //             wt.WriteField(correct_ordering);
// //             wt.WriteField(wrong_ordering);
// //             wt.WriteField(Utility.getAppendString(correctOrder_ordering));
// //             wt.WriteField(Utility.getAppendString(wrongOrder_ordering));
// //             //wt.WriteField(count_coin_gdrive.ToString()); // count coin
// //             //wt.WriteField(keep_coin_gdrive.ToString()); // keep coin
// //             //wt.WriteField(miss_coin_gdrive.ToString()); // miss coin

// //             wt.WriteField(orderingController.isYawOn.ToString()); // is yaw on
// //             wt.WriteField(orderingController.isRollOn.ToString()); // is roll on
// //             wt.WriteField(score_ordering); // score

// //             // shared variables
// //             wt.WriteField(difficulty);
// //             wt.WriteField(motorSpin);
// //             wt.WriteField(alwaysMove);

// //             wt.NextRecord();

// //             // close all
// //             wt.Dispose();
// //             sw.Close();
// //             ftw.Close();
// //         }

// //         resetSharedVars();
// //         resetOrdering();
// //     }

// //     // reset all variables after they are written
// //     void resetOrdering()
// //     {
// //         score_ordering = 0;
// //         timeUsedPerItem_ordering = new List<float>();
// //         avgTimeUsedPerItem_ordering = 0;
// //         timeUsedPerQuestion_ordering = new List<float>();
// //         avgTimeUsedPerQuestion_ordering = 0;
// //         correctWrongList_ordering = new List<string>();
// //         correct_ordering = 0;
// //         wrong_ordering = 0;
// //         correctOrder_ordering = new List<string>();
// //         wrongOrder_ordering = new List<string>();
// //     }
// //     #endregion

// //     #region grouping
// //     // objects to be written on log
// //     // date, duration, playtime, time-used-per-item, avg-time-used-per-item, 
// //     // time-used-per-question, avg-time-usedGrouping-per-question, correct-wrong-item, 
// //     // no-of-correct-item, no-of-wrong-item, correct-category, wrong-category,
// //     // flip-roll-yaw, score, mode, motor-spin, always_move

// //     //public static float duration_gdrive;
// //     // public static int score_grouping;
// //     // public static List<float> timeUsedPerItem_grouping = new List<float>();
// //     // public static float avgTimeUsedPerItem_grouping;
// //     // public static List<float> timeUsedPerQuestion_grouping = new List<float>();
// //     // public static float avgTimeUsedPerQuestion_grouping;
// //     // public static List<string> correctWrongList_grouping = new List<string>();
// //     // public static int correct_grouping;
// //     // public static int wrong_grouping;
// //     // public static List<string> correctCat_grouping = new List<string>();
// //     // public static List<string> wrongCat_grouping = new List<string>();
// //     private string groupingDir = @"log/grouping/";
// //     private string groupingCsv = @"log/grouping/" + GameControl.currentUserId + ".csv";

// //     public void writeGrouping()
// //     {
// //         // check if directory exist
// //         if (!Directory.Exists(groupingDir))
// //         {
// //             Directory.CreateDirectory(groupingDir);
// //             Debug.Log("Create log/groupingDir/ in home directory!");
// //         }
// //         // check if file exists, if not create with header
// //         if (!File.Exists(groupingCsv))
// //         {
// //             // create new csv file
// //             using (var ftw = new FileStream(groupingCsv, FileMode.Append))
// //             using (var sw = new StreamWriter(ftw))
// //             using (var wt = new CsvWriter(sw))
// //             {
// //                 // date, duration, playtime, time-used-per-item, avg-time-used-per-item, 
// //                 // time-used-per-question, avg-time-used-per-question, correct-wrong-item, 
// //                 // no-of-correct-item, no-of-wrong-item, correct-order, wrong-order,
// //                 // is-yaw-on, is-roll-on, score, mode, motor-spin, always_move

// //                 // write headers
// //                 wt.WriteField("date"); // system datetime
// //                 wt.WriteField("duration"); // from timeController
// //                 wt.WriteField("playtime"); // playtimeset from pauseScript

// //                 wt.WriteField("time_per_item");
// //                 wt.WriteField("avg_time_per_item");
// //                 wt.WriteField("time_per_question");
// //                 wt.WriteField("avg_time_per_question");
// //                 wt.WriteField("correctWrong_list");
// //                 wt.WriteField("correct_item");
// //                 wt.WriteField("wrong_item");
// //                 wt.WriteField("correct_category");
// //                 wt.WriteField("wrong_category");

// //                 wt.WriteField("score"); // from gdriveController

// //                 // shared variables
// //                 wt.WriteField("difficulty");
// //                 wt.WriteField("motor_spin");
// //                 wt.WriteField("always_move");

// //                 wt.NextRecord();

// //                 // close all
// //                 wt.Dispose();
// //                 sw.Close();
// //                 ftw.Close();
// //             }

// //             Debug.Log("File at path '" + groupingCsv + "' is created with headers.");
// //         }

// //         using (var ftw = new FileStream(groupingCsv, FileMode.Append))
// //         using (var sw = new StreamWriter(ftw))
// //         using (var wt = new CsvWriter(sw))
// //         {
// //             // write headers
// //             wt.WriteField(DateTime.Now.ToString()); // date
// //             wt.WriteField(final_duration.ToString("f2")); // duration
// //             wt.WriteField(pauseScript.playtimeSet.ToString("f2")); // playtimeset

// //             wt.WriteField(Utility.getAppendString(timeUsedPerItem_grouping));
// //             try
// //             {
// //                 wt.WriteField(timeUsedPerItem_grouping.Average().ToString("f2"));
// //             }
// //             catch
// //             {
// //                 wt.WriteField(string.Empty);
// //             }
// //             wt.WriteField(Utility.getAppendString(timeUsedPerQuestion_grouping));
// //             try
// //             {
// //                 wt.WriteField(timeUsedPerQuestion_grouping.Average().ToString("f2"));
// //             }
// //             catch
// //             {
// //                 wt.WriteField(string.Empty);
// //             }
// //             wt.WriteField(Utility.getAppendString(correctWrongList_grouping));
// //             wt.WriteField(correct_grouping);
// //             wt.WriteField(wrong_grouping);
// //             wt.WriteField(Utility.getAppendString(correctCat_grouping));
// //             wt.WriteField(Utility.getAppendString(wrongCat_grouping));
// //             //wt.WriteField(count_coin_gdrive.ToString()); // count coin
// //             //wt.WriteField(keep_coin_gdrive.ToString()); // keep coin
// //             //wt.WriteField(miss_coin_gdrive.ToString()); // miss coin

// //             // no yaw and roll in grouping
// //             //wt.WriteField(groupingController.isYawOn.ToString()); // is yaw on
// //             //wt.WriteField(groupingController.isRollOn.ToString()); // is roll on
// //             wt.WriteField(score_grouping); // score

// //             // shared variables
// //             wt.WriteField(difficulty);
// //             wt.WriteField(motorSpin);
// //             wt.WriteField(alwaysMove);

// //             wt.NextRecord();

// //             // close all
// //             wt.Dispose();
// //             sw.Close();
// //             ftw.Close();
// //         }

// //         resetSharedVars();
// //         resetGrouping();
// //     }

// //     // reset all variables after they are written
// //     void resetGrouping()
// //     {
// //         score_grouping = 0;
// //         timeUsedPerItem_grouping = new List<float>();
// //         avgTimeUsedPerItem_grouping = 0;
// //         timeUsedPerQuestion_grouping = new List<float>();
// //         avgTimeUsedPerQuestion_grouping = 0;
// //         correctWrongList_grouping = new List<string>();
// //         correct_grouping = 0;
// //         wrong_grouping = 0;
// //         correctCat_grouping = new List<string>();
// //         wrongCat_grouping = new List<string>();
// //     }
// //     #endregion

// //     #region matching
// //     // objects to be written on log
// //     // date, duration, playtime, time-used-per-question, 
// //     // avg-time-used-per-question, correct-wrong-item, 
// //     // no-of-correct-item, no-of-wrong-item
// //     // is-yaw-on, is-roll-on, score, mode, motor-spin, always_move

// //     //public static float duration_gdrive;
// //     // public static int score_matching;
// //     // //public static List<float> timeUsedPerItem_matching = new List<float>();
// //     // //public static float avgTimeUsedPerItem_matching;
// //     // public static List<float> timeUsedPerQuestion_matching = new List<float>();
// //     // public static float avgTimeUsedPerQuestion_matching;
// //     // public static List<string> correctWrongList_matching = new List<string>();
// //     // public static int correct_matching;
// //     // public static int wrong_matching;
// //     //public static List<string> correctOrder_matching = new List<string>();
// //     //public static List<string> wrongOrder_matching = new List<string>();
// //     private string matchingDir = @"log/matching/";
// //     private string matchingCsv = @"log/matching/" + GameControl.currentUserId + ".csv";

// //     public static Dictionary<string, int> score_collector = new Dictionary<string, int>();



// //     public void writeMatching()
// //     {
// //         // check if directory exist
// //         if (!Directory.Exists(matchingDir))
// //         {
// //             Directory.CreateDirectory(matchingDir);
// //             Debug.Log("Create log/matchingDir/ in home directory!");
// //         }
// //         // check if file exists, if not create with header
// //         if (!File.Exists(matchingCsv))
// //         {
// //             // create new csv file
// //             using (var ftw = new FileStream(matchingCsv, FileMode.Append))
// //             using (var sw = new StreamWriter(ftw))
// //             using (var wt = new CsvWriter(sw))
// //             {
// //                 // date, duration, playtime, time-used-per-item, avg-time-used-per-item, 
// //                 // time-used-per-question, avg-time-used-per-question, correct-wrong-item, 
// //                 // no-of-correct-item, no-of-wrong-item, correct-order, wrong-order,
// //                 // is-yaw-on, is-roll-on, score, mode, motor-spin, always_move

// //                 // write headers
// //                 wt.WriteField("date"); // system datetime
// //                 wt.WriteField("duration"); // from timeController
// //                 wt.WriteField("playtime"); // playtimeset from pauseScript

// //                 //wt.WriteField("time_per_item");
// //                 //wt.WriteField("avg_time_per_item");
// //                 wt.WriteField("time_per_question");
// //                 wt.WriteField("avg_time_per_question");
// //                 wt.WriteField("correctWrong_list");
// //                 wt.WriteField("correct_item");
// //                 wt.WriteField("wrong_item");
// //                 //wt.WriteField("correct_order");
// //                 //wt.WriteField("wrong_order");

// //                 wt.WriteField("is_yaw_on"); // from gdriveController
// //                 wt.WriteField("is_roll_on"); // from gdriveController
// //                 wt.WriteField("score"); // from gdriveController

// //                 // shared variables
// //                 wt.WriteField("difficulty");
// //                 wt.WriteField("motor_spin");
// //                 wt.WriteField("always_move");

// //                 wt.NextRecord();

// //                 // close all
// //                 wt.Dispose();
// //                 sw.Close();
// //                 ftw.Close();
// //             }

// //             Debug.Log("File at path '" + matchingCsv + "' is created with headers.");
// //         }

// //         using (var ftw = new FileStream(matchingCsv, FileMode.Append))
// //         using (var sw = new StreamWriter(ftw))
// //         using (var wt = new CsvWriter(sw))
// //         {
// //             // write headers
// //             wt.WriteField(DateTime.Now.ToString()); // date
// //             wt.WriteField(final_duration.ToString("f2")); // duration
// //             wt.WriteField(pauseScript.playtimeSet.ToString("f2")); // playtimeset

// //             //wt.WriteField("time_per_item");
// //             //wt.WriteField("avg_time_per_item");
// //             wt.WriteField(Utility.getAppendString(timeUsedPerQuestion_matching));
// //             // try
// //             // {
// //             //     wt.WriteField(timeUsedPerQuestion_matching.Average());
// //             // }
// //             // catch
// //             // {
// //             //     wt.WriteField(string.Empty);
// //             // }
// //             wt.WriteField(Utility.getAverageOfFloatList(timeUsedPerQuestion_matching));
// //             wt.WriteField(Utility.getAppendString(correctWrongList_matching));
// //             wt.WriteField(correct_matching);
// //             wt.WriteField(wrong_matching);
// //             //wt.WriteField("correct_order");
// //             //wt.WriteField("wrong_order");
// //             //wt.WriteField(count_coin_gdrive.ToString()); // count coin
// //             //wt.WriteField(keep_coin_gdrive.ToString()); // keep coin
// //             //wt.WriteField(miss_coin_gdrive.ToString()); // miss coin

// //             wt.WriteField(matchingController.isYawOn.ToString()); // is yaw on
// //             wt.WriteField(matchingController.isRollOn.ToString()); // is roll on
// //             wt.WriteField(score_matching); // score

// //             // shared variables
// //             wt.WriteField(difficulty);
// //             wt.WriteField(motorSpin);
// //             wt.WriteField(alwaysMove);

// //             wt.NextRecord();

// //             // close all
// //             wt.Dispose();
// //             sw.Close();
// //             ftw.Close();
// //         }

// //         resetSharedVars();
// //         resetMatching();
// //     }

// //     // reset all variables after they are written
// //     public static void resetMatching()
// //     {
// //         score_matching = 0;
// //         //timeUsedPerItem_matching = new List<float>();
// //         //avgTimeUsedPerItem_matching = 0;
// //         timeUsedPerQuestion_matching = new List<float>();
// //         avgTimeUsedPerQuestion_matching = 0;
// //         correctWrongList_matching = new List<string>();
// //         correct_matching = 0;
// //         wrong_matching = 0;
// //         //correctOrder_matching = new List<string>();
// //         //wrongOrder_matching = new List<string>();
// //     }
// //     #endregion
// // }