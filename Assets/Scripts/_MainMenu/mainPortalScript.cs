using UnityEngine;
using UnityEngine.UI;               // ui objects
using System.Collections.Generic;   // list
using UnityEngine.SceneManagement;
using System.IO;                    // file stream
using CsvHelper;                    // csv write and read
using System;                       // Date
using System.Linq;

public class mainPortalScript : MonoBehaviour {

    public Button saveButton;
    public Text welcomeText;
    public Text statusInfoText;
    public InputField nameInputField;
    public InputField birthdayInputField;
    public InputField informationInputField;

    private static bool isInfoExisted = false;
    private static string gameDir;
    private static string gameCsv;
    public static Dictionary<string, string> dataCollector = new Dictionary<string, string>();

    private static List<string> birthdays = new List<string>();
    private static List<string> informations = new List<string>();
    private static List<DateTime> dates = new List<DateTime>();
    public CanvasGroup playCanvas;
    void Start()
    {   
        saveButton.onClick.AddListener(() => onSave());
    }

    public void clearData()
    {
        nameInputField.text = "";
        birthdayInputField.text = "";
        informationInputField.text = "";
        statusInfoText.text = "Please input your name : )";
        dataCollector.Clear();

        GameControl.currentUserName = "";
        welcomeText.text = "Name, Please!";

        playCanvas.alpha = 0.7f;
        playCanvas.interactable = false; 
        playCanvas.blocksRaycasts = false;  

    }

    public void onSave()
    {
        Debug.Log("call onSave()");
        dataCollector["date"] = DateTime.Now.ToString();
        dataCollector["name"] = nameInputField.text.ToLower();       
        
        if (nameInputField.text != "")
        {
            updateGamePath();
            if(isInfoExisted)
            {
                statusInfoText.text = "Welcome back, " + dataCollector["name"];
                if (File.Exists(gameCsv))
                {
                    using (var ftr = new FileStream(gameCsv, FileMode.Open))
                    using (var sr = new StreamReader(ftr))
                    using (var rd = new CsvReader(sr))
                    {
                        while (rd.Read())
                        {
                            var date = rd.GetField<string>("date");
                            var birthday = rd.GetField<string>("birthday");
                            var information = rd.GetField<string>("information");
                            
                            dates.Add(DateTime.Parse(date));
                            birthdays.Add(birthday);
                            informations.Add(information);
                        }
                        // close all
                        rd.Dispose();
                        sr.Close();
                        ftr.Close();
                    }
                    int indexOfLatest = dates.IndexOf(dates.Max());
                    if (birthdayInputField.text == "")
                    {
                        birthdayInputField.text = birthdays[indexOfLatest];
                    }

                    if (informationInputField.text == "")
                    {
                        informationInputField.text = informations[indexOfLatest];
                    }
                }
            }
            else
            {
                statusInfoText.text = "Hello " + dataCollector["name"] + ", data saved";
            }

            dataCollector["birthday"] = birthdayInputField.text;
            dataCollector["information"] = informationInputField.text;
            writeData();

            playCanvas.alpha = 1;
            playCanvas.interactable = true; 
            playCanvas.blocksRaycasts = true;            

            GameControl.currentUserName = dataCollector["name"];
            welcomeText.text = GameControl.currentUserName;
        }
    }

    public static void updateGamePath()
    {
        gameDir = @"log/" + dataCollector["name"] + "/";
        gameCsv = @"log/" + dataCollector["name"] + "/" + "infomation.csv";
        if (Directory.Exists(gameDir))
        { 
            isInfoExisted = true;
        }
        else
        {
            isInfoExisted = false;
        }
    }    

    public static void writeData()
    {
        // check if directory exist
        if (!isInfoExisted)
        {
            Directory.CreateDirectory(gameDir);
            Debug.Log("Create log/" + dataCollector["name"] +  "/ in home directory!");
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
}
