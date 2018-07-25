// using UnityEngine;
// using System.Collections;
// // for this script
// using UnityEngine.UI;
// using CsvHelper;
// using System; // DateTime
// using System.IO;
// using System.Collections.Generic;

// using UnityEngine.SceneManagement;


// public class registrationScript : MonoBehaviour
// {

//   // UI objects
//   public InputField id_in;
//   public InputField fullname_in;
//   public InputField nickname_in;
//   public Text sex_in;
//   public InputField birthday_Day_in;
//   public InputField birthday_Month_in;
//   public InputField birthday_Year_in;
//   public Text domHand_in;
//   public Text affectedSide_in;
//   public InputField latestStrokeDiag_Year_in;
//   public InputField latestStrokeDiag_Month_in;

//   public Text firstStrokeToggle_in;
//   public Text note_in;
//   //public InputField rehabOnset_Year_in;
//   //public InputField rehabOnset_Month_in;

//   // variables to save
//   public string id;
//   public string fullname;
//   public string nickname;
//   public string sex;
//   public string birthday_Day;
//   public string birthday_Month;
//   public string birthday_Year;
//   public string domHand;
//   public string affectedSide;
//   public string latestStrokeDiag_Year;
//   public string latestStrokeDiag_Month;

//   public string firstStrokeToggle;
//   public string note;
//   //public string rehabOnset_Year;
//   //public string rehabOnset_Month;

//   // gameobject related
//   public GameObject warningID;
//   public GameObject warnBD;
//   public GameObject warnFS;
//   public GameObject warnRO;

//   // variables to use
//   private List<string> checkid = new List<string>();
//   private string userInfoPath = @"log/userInfo.csv";
//   private bool bd, bm, by, fsy, fsm, roy, rom;

//   void Start()
//   {

//   }

//   void Update()
//   {
//     // id ---------------------------
//     id = id_in.text;
//     // not more than 8 chars for id
//     int maxIdLen = 8;
//     if (id_in.text.Length > maxIdLen) { id_in.text = id_in.text.Substring(0, maxIdLen); }
//     // ------------------------------

//     fullname = fullname_in.text;
//     nickname = nickname_in.text;
//     sex = sex_in.text;

//     // bd ---------------------------
//     birthday_Day = birthday_Day_in.text;
//     birthday_Month = birthday_Month_in.text;
//     birthday_Year = birthday_Year_in.text;
//     // limit date month year as it should be - birthday
//     bd = checkDate(birthday_Day_in, 1, 31);
//     bm = checkDate(birthday_Month_in, 1, 12);
//     by = checkDate(birthday_Year_in, 1, 3000);
//     if (bd | bm | by == true) { warnBD.SetActive(true); }
//     else { warnBD.SetActive(false); }
//     // ------------------------------

//     domHand = domHand_in.text;
//     affectedSide = affectedSide_in.text;
//     firstStrokeToggle = firstStrokeToggle_in.text;
//     note = note_in.text;

//     // fs ---------------------------
//     latestStrokeDiag_Year = latestStrokeDiag_Year_in.text;
//     latestStrokeDiag_Month = latestStrokeDiag_Month_in.text;
//     // limit date month year as it should be - first stroke
//     fsy = checkDate(latestStrokeDiag_Year_in, 1, 3000);
//     fsm = checkDate(latestStrokeDiag_Month_in, 1, 12);
//     if (fsy | fsm == true) { warnFS.SetActive(true); }
//     else { warnFS.SetActive(false); }
//     // ------------------------------

//     // ro ---------------------------
//     //rehabOnset_Year = rehabOnset_Year_in.text;
//     //rehabOnset_Month = rehabOnset_Month_in.text;
//     // limit date month year as it should be - rehab onset
//     //roy = checkDate (rehabOnset_Year_in, 0, 99);
//     //rom = checkDate (rehabOnset_Month_in, 0, 12);
//     //if (roy | rom == true) {warnRO.SetActive(true);}
//     //else {warnRO.SetActive(false);}
//     // ------------------------------
//   }

//   bool checkDate(InputField input, int init, int end)
//   {
//     int i;
//     bool tosetactive = false;

//     if (int.TryParse(input.text, out i))
//     {
//       if (i < init) { input.text = init.ToString(); }
//       else if (i > end) { input.text = end.ToString(); }
//       return tosetactive;
//     }
//     else
//     {
//       if (input.text == "") { }
//       else { tosetactive = true; input.text = " "; }
//       return tosetactive;
//     }
//   }

//   string getAge(string yearStr, string monthStr)
//   {
//     int year, month;

//     if (int.TryParse(yearStr, out year) && int.TryParse(monthStr, out month))
//     {
//       int ageYear, ageMonth;

//       if (year - DateTime.Now.Year > 393)
//       {
//         year = year - 543;
//         Debug.Log("Year: " + year);
//       }

//       ageYear = DateTime.Now.Year - year;
//       Debug.Log("YearNow: " + DateTime.Now.Year);

//       if (DateTime.Now.Month < month)
//       {
//         ageYear--;
//         ageMonth = 12 - (month - DateTime.Now.Month);
//       }
//       else if (DateTime.Now.Month > month)
//       {
//         ageMonth = DateTime.Now.Month - month;
//       }
//       else
//       {
//         ageMonth = 0;
//       }

//       return ageYear.ToString() + "." + ageMonth.ToString();
//     }
//     else
//     {
//       return "";
//     }

//   }

//   bool checkDupID(string id)
//   {
//     // collect all items in id field to variable checkid
//     using (var ftr = new FileStream(userInfoPath, FileMode.Open))
//     using (var sr = new StreamReader(ftr))
//     using (var rd = new CsvReader(sr))
//     {
//       while (rd.Read())
//       {
//         var eachID = rd.GetField<string>("id");
//         checkid.Add(eachID);
//       }

//       // close all
//       rd.Dispose();
//       sr.Close();
//       ftr.Close();
//     }

//     // check duplication
//     if (checkid.Contains(id))
//     {
//       warningID.SetActive(true);
//       return true;
//     }
//     else
//     {
//       warningID.SetActive(false);
//       return false;
//     }
//   }

//   public void csvWrite()
//   {
//     if (!Directory.Exists(@"log/"))
//     {
//       Directory.CreateDirectory(@"log/");
//     }

//     if (File.Exists(userInfoPath))
//     {
//       Debug.Log("File is already created");
//     }
//     else
//     {
//       // create new csv file
//       using (var ftw = new FileStream(userInfoPath, FileMode.Append))
//       using (var sw = new StreamWriter(ftw))
//       using (var wt = new CsvWriter(sw))
//       {
//         // write headers
//         wt.WriteField("date");
//         wt.WriteField("id");
//         wt.WriteField("fullname");
//         wt.WriteField("nickname");
//         wt.WriteField("sex");
//         wt.WriteField("birthDay");
//         wt.WriteField("birthMonth");
//         wt.WriteField("birthYear");
//         wt.WriteField("age");
//         wt.WriteField("domHand");
//         wt.WriteField("effectedSide");
//         wt.WriteField("latestStrokeMonth");
//         wt.WriteField("latestStrokeYear");
//         wt.WriteField("firstStrokeOccur");
//         wt.WriteField("note");
//         //wt.WriteField("rehabOnsetYear");
//         //wt.WriteField("rehabOnsetMonth");
//         wt.NextRecord();

//         // close all
//         wt.Dispose();
//         sw.Close();
//         ftw.Close();
//       }

//       Debug.Log("File at path '" + userInfoPath + "' is created with headers.");
//     }

//     if (checkDupID(id))
//     {
//       // if id is duplicated, change id first
//       Debug.Log("Please change your username...");

//     }
//     else
//     {
//       // if not duplicated, write to csv
//       using (var ftw = new FileStream(userInfoPath, FileMode.Append))
//       using (var sw = new StreamWriter(ftw))
//       using (var wt = new CsvWriter(sw))
//       {
//         wt.WriteField(DateTime.Now.ToString());
//         wt.WriteField(id);
//         wt.WriteField(fullname);
//         wt.WriteField(nickname);
//         wt.WriteField(sex);
//         wt.WriteField(birthday_Day);
//         wt.WriteField(birthday_Month);
//         wt.WriteField(birthday_Year);
//         wt.WriteField(getAge(birthday_Year, birthday_Month));
//         wt.WriteField(domHand);
//         wt.WriteField(affectedSide);
//         wt.WriteField(latestStrokeDiag_Month);
//         wt.WriteField(latestStrokeDiag_Year);
//         wt.WriteField(firstStrokeToggle);
//         wt.WriteField(note);
//         //wt.WriteField(rehabOnset_Year);
//         //wt.WriteField(rehabOnset_Month);
//         wt.NextRecord();

//         // close all
//         wt.Dispose();
//         sw.Close();
//         ftw.Close();
//       }

//       Debug.Log("Data for " + id + " is added.");

//       // set gameControl global object
//       // GameControl.currentUserId = id;
//       // GameControl.currentUserNickname = nickname;
//       GameControl.userStatus = 0; // indicate this is new user

//       // load calibration, if not exist load default
//       // calibration
//       int[] temp = new int[4];
//       // if (LoadDataUtility.getLastCalibVals(id, out temp))
//       // {
//       //   // call next scene
//       //   SceneManager.LoadScene("mainportal");
//       // }
//     }
//   }

//   public void backToMain()
//   {
//     SceneManager.LoadScene("main");
//   }
// }
