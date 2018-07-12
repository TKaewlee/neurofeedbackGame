// using UnityEngine;
// // using System.Collections;
// using UnityEngine.UI;
// // using System.Text;

// using UnityEngine.SceneManagement;

// public class reportScoreController : MonoBehaviour
// {

//     public Text scoreReport;

//     // Use this for initialization
//     void Start()
//     {
//         // add listener for back button
//         back.onClick.AddListener(onBack);
//         // refresh score dictionary
//         LogCollector.addScoreToCollector();
//         // display score
//         scoreReport.text = LogCollector.score_collector[LogCollector.last_game].ToString();
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Return))
//         {
//             onBack();
//         }
//     }

//     public Button back;
//     void onBack()
//     {
//         LogWriter.GameLogData.updateGamePath();
//         LogWriter.GameLogData.getData();
//         LogWriter.GameLogData.writeData();
//         LogWriter.GameLogData.clearData();

//         // reset isPlaytimeSet state to get it playable in next play
//         timeController.isPlaytimeSet = false; // get out of elapsed loop

//         // back to gamePortal scene
//         SceneManager.LoadScene("gameportal");
//         //Destroy(GameObject.Find("logCollector"));
//     }
// }
