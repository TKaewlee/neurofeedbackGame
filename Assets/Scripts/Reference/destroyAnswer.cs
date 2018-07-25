// using UnityEngine;
// using System.Collections;

// public class destroyAnswer : MonoBehaviour
// {
//     public GameObject explosion;
//     public GameObject playerExplosion;
//     public int scoreValue;
//     private spaceShooterController spaceshootercontroller;

//     void Start()
//     {
//         GameObject spaceControllerObj = GameObject.FindWithTag("spaceController");
//         if (spaceControllerObj != null)
//         {
//             spaceshootercontroller = spaceControllerObj.GetComponent<spaceShooterController>();
//         }
//         if (spaceshootercontroller == null)
//         {
//             Debug.Log("Cannot find 'spaceShooterController' script");
//         }
//     }

//     private float lastTime;

//     void OnTriggerEnter(Collider other)
//     {
//         if (other.tag == "Player")
//         {
//             // only destroy the answer
//             Instantiate(explosion, transform.position, transform.rotation);
//             spaceshootercontroller.AddScore(scoreValue);
//             if (gameObject.tag == "Answer")
//             {
//                 LogCollector.timeUsedEachQuestion_SpaceShooter.Add(Time.time - timeController.eachQTime);

//                 Destroy(gameObject);
//                 Instantiate(explosion, transform.position, transform.rotation);

//                 // destroy all dummies
//                 foreach (GameObject dummy in GameObject.FindGameObjectsWithTag("Dummy"))
//                 {
//                     Destroy(dummy);
//                     Instantiate(explosion, dummy.transform.position, dummy.transform.rotation);
//                 }

//                 // create new question
//                 questionController.toGetNewQuestion = true;
//                 // GameObject.FindWithTag("spaceController").GetComponent<questionController>().newQuestion(10, "multiply", 4);

//                 // add to log
//                 LogCollector.correct_SpaceShooter += 1;
//                 LogCollector.correctWrongList_SpaceShooter.Add("correct");
//             }
//             else
//             {
//                 Destroy(gameObject);
//                 Instantiate(explosion, transform.position, transform.rotation);

//                 // add to log
//                 LogCollector.wrong_SpaceShooter += 1;
//                 LogCollector.correctWrongList_SpaceShooter.Add("wrong");
//             }
//         }

//         if (other.tag == "Bolt") // do not collide with Player
//         {  
// //            Debug.Log("destroy Answer access BOLT!");
//             // instantiate explosive effect of asteroid
//             Instantiate(explosion, transform.position, transform.rotation);
//             // if tag is Player, also initiate explosive effect of player
//             //if (other.tag == "PlayerCollide")
//             //{
//             //    Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
//             //    spaceshootercontroller.GameOver();
//             //    Debug.Log(other.tag);
//             //}

//             // update score
//             spaceshootercontroller.AddScore(scoreValue);
//             // destroy all the colliders
//             Destroy(other.gameObject);
//             // check if object being shot is actual answer
//             // if yes destroy all, else destroy only itself
//             if (gameObject.tag == "Answer")
//             {
//                 //Debug.Log(Time.time - questionController.startTime);
//                 LogCollector.timeUsedEachQuestion_SpaceShooter.Add(Time.time - timeController.eachQTime);

//                 Destroy(gameObject);
//                 Instantiate(explosion, transform.position, transform.rotation);
//                 foreach (GameObject dummy in GameObject.FindGameObjectsWithTag("Dummy"))
//                 {
//                     Destroy(dummy);
//                     Instantiate(explosion, dummy.transform.position, dummy.transform.rotation);
//                 }

//                 // create new question
// 				questionController.toGetNewQuestion = true;
//                 // GameObject.FindWithTag("spaceController").GetComponent<questionController>().newQuestion(10, "multiply", 4);

//                 // add to log
//                 LogCollector.correct_SpaceShooter += 1;
//                 LogCollector.correctWrongList_SpaceShooter.Add("correct");
//             }
//             else
//             {
//                 Destroy(gameObject);
//                 Instantiate(explosion, transform.position, transform.rotation);

//                 // add to log
//                 LogCollector.wrong_SpaceShooter += 1;
//                 LogCollector.correctWrongList_SpaceShooter.Add("wrong");
//             }
//         }
//     }
// }
