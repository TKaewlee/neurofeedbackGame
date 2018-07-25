// using UnityEngine;
// using System.Collections.Generic;
// using UnityEngine.UI;
// // for coroutine
// using System.Collections;

// using UnityEngine.SceneManagement;

// public class questionController : MonoBehaviour
// {
//     #region  main events
//     void Start () {
//         // reset game settings for each round
//         //resetSettings();

//         // reset value to be able to create answer
//         continuePlay = true; // get new question
//         toGetNewQuestion = true; // same as above

//         //// default center wheel move
//         //motorControl.toggleWheel = true;

//         // set level mode based on last log
//         // if no log - it returns easy lvl
//         // dropdown must be set before call change level function
//         levelDropdown.value = getFromLog.getLastMode(SceneManager.GetActiveScene().name, GameControl.currentUserId);
//         spaceShooterLevel(levelDropdown.value);

//         //Debug.Log("maxno" + maxNumber);
//         //Debug.Log("nodummy" + noDummy);
//         //Debug.Log("oprt" + oprt);

//         // setting listener
//         levelDropdown.onValueChanged.AddListener(delegate 
//         {
//             // get case from dropdown item
//             //int lvl = levelDropdown.value;

//             //Debug.Log(lvl);
//             spaceShooterLevel(levelDropdown.value);
//         });

//         moreSettingsToggle.onValueChanged.AddListener(delegate
//         {
//             if (moreSettingsToggle.isOn)
//             {
//                 moreSettingsCanvasGrp.alpha = 1;
//                 moreSettingsCanvasGrp.blocksRaycasts = true;
//             }
//             else
//             {
//                 moreSettingsCanvasGrp.alpha = 0;
//                 moreSettingsCanvasGrp.blocksRaycasts = false;
//             }
//         });

//         // add listener too all game setting elements
// 		maxNoDD.onValueChanged.AddListener(delegate {onMaxNoChanged();});
// 		oprtDD.onValueChanged.AddListener(delegate {onOprtChanged();});
// 		noDummyDD.onValueChanged.AddListener(delegate {onNoDummyChanged();});
// 		ansSpeedSD.onValueChanged.AddListener (delegate {onAnsSpeedChanged ();});
// 		fireRateSD.onValueChanged.AddListener (delegate {onFireRateChanged ();});

//         ansSpeedText.text = (ansSpeedSD.value / 10).ToString();
//         fireRateText.text = fireRateSD.value.ToString("f1");

//         // start coroutine to get new question
//         StartCoroutine(getNewQuestion());
//     }

//     // Update is called once per frame
//     void Update () {
        
//     }
//     #endregion

//     #region game settings
//     // level settings
//     // 2nd settings (simplified as 3 levels)
//     public Dropdown levelDropdown;
//     public Toggle moreSettingsToggle;
//     public CanvasGroup moreSettingsCanvasGrp;

//     void spaceShooterLevel(int lvl)
//     {
//         switch (lvl)
//         {
//             case 0:
//                 maxNumber = 10;
//                 noDummy = 0;
// 				// add number of dummy to LogCollector
// 				LogCollector.noDummy_SpaceShooter = noDummy;
//                 oprt = "plus";

//                 LogCollector.difficulty = "easy";
//                 break;
//             case 1:
//                 maxNumber = 10;
//                 noDummy = 1;
// 				// add number of dummy to LogCollector
// 				LogCollector.noDummy_SpaceShooter = noDummy;
//                 if(Random.Range(0,10) > 5)
//                 {
//                     oprt = "plus";
//                 }
//                 else
//                 {
//                     oprt = "minus";
//                 }
//                 //Debug.Log(oprt);

//                 LogCollector.difficulty = "medium";
//                 break;
//             case 2:
//                 maxNumber = 15;
//                 noDummy = 3;
// 				// add number of dummy to LogCollector
// 				LogCollector.noDummy_SpaceShooter = noDummy;
//                 if (Random.Range(0, 10) > 5)
//                 {
//                     oprt = "plus";
//                 }
//                 else
//                 {
//                     oprt = "minus";
//                 }
//                 //Debug.Log(oprt);

//                 LogCollector.difficulty = "hard";
//                 break;
//         }
//     }


//     // get UI elements
//     public Dropdown maxNoDD;
//     public Dropdown oprtDD;
//     public Dropdown noDummyDD;
//     public Slider ansSpeedSD;
//     public Slider fireRateSD;

//     public Text ansSpeedText;
//     public Text fireRateText;

//     // default settings to spawn new question
//     public static int maxNumber = 10;
//     public static string oprt = "plus"; // operator
//     public static int noDummy = 0;

//     // coroutine loop controller
//     public static bool toGetNewQuestion = true;
//     public static bool continuePlay = true;

//     // function to call when UI changed
//     void onMaxNoChanged()
//     {
//         maxNumber = int.Parse(maxNoDD.captionText.text);
//         // logwriter
//         LogCollector.maxNo_SpaceShooter = maxNumber;
//     }

//     void onOprtChanged()
//     {
//         oprt = oprtDD.captionText.text;
//         // logwriter
//         LogCollector.operator_SpaceShooter = oprt;
//         //		Debug.Log (oprt);
//     }

//     void onNoDummyChanged()
//     {
//         noDummy = int.Parse(noDummyDD.captionText.text);
//         // logwriter
//         LogCollector.noDummy_SpaceShooter = noDummy;
//     }

//     void onAnsSpeedChanged()
//     {
//         mover.answerSpeed = -(ansSpeedSD.value / 10);
//         ansSpeedText.text = (ansSpeedSD.value / 10).ToString();
//         // logwriter
//         LogCollector.answerSpeed_SpaceShooter = ansSpeedSD.value / 10;
//     }

//     void onFireRateChanged()
//     {
//         playerController.fireRate = fireRateSD.value;
//         fireRateText.text = fireRateSD.value.ToString("f1");
//         // logwriter
//         LogCollector.fireRate_SpaceShooter = fireRateSD.value;
// 		playerController.onFireRateChanged();
//     }
//     #endregion

//     #region resetSettings
//     void resetSettings()
//     {
//         maxNumber = 10;
//         oprt = "plus";
//         noDummy = 0;
//         mover.answerSpeed = -0.5f;
//         playerController.fireRate = 1.5f;
//         pauseScript.playtimeSet = 0;
//     }
//     #endregion

//     #region questions spawn part
//     // question spawn rate
//     public Vector3 spawnValues;
// 	public float spawnWait;
// 	public float startWait;
// 	public float waveWait;
	
// 	// question
// 	public GameObject answer;
// 	public TextMesh answerText;
// 	public Text questionText;
// 	private int q1, q2;
// 	private int ans;

// 	// coroutine to be called for new question
// 	IEnumerator getNewQuestion()
// 	{
// //		Debug.Log ("this cooroutine started!");
// 		// give some short period to brace self
// 		yield return new WaitForSeconds (0.5f);
		
// 		while (continuePlay)
// 		{
// 			if(toGetNewQuestion)
// 			{
// 				timeController.eachQTime = Time.time;
// 				newQuestion(maxNumber, oprt, noDummy+1);
// 				toGetNewQuestion = false;
// 				//				yield return new WaitForSeconds (3f);
// 			}
// //			Debug.Log("toGetNewQuestion: " + toGetNewQuestion);
// 			yield return new WaitForSeconds (0.5f);
// 		}
// 	}

// 	// method to get random consecutive int from 0 to len
// 	int[] getRandInt(int len)
// 	{
// 		int[] returnArr = new int[len];
// 		List<int> lastRand = new List<int>();
		
// 		for (int i = 0; i < returnArr.Length; i++)
// 		{
// 			int newRand = Random.Range(0, len);
// 			while (lastRand.Contains(newRand))
// 			{
// 				newRand = Random.Range(0, len);
// 			}
			
// 			returnArr[i] = newRand;
// 			lastRand.Add(newRand);
// 		}
		
// 		return returnArr;
// 	}

// 	// method to get random position for answer and dummies
// 	Vector3[] getAnswerRandomPosition(int numberOfAnswer, Vector3 spawnPos)
// 	{
// 		float sx, shiftx;
// 		Vector3[] placePos;
// 		int[] randOrder;
// 		switch (numberOfAnswer)
// 		{
// 		case 5:
// 			randOrder = getRandInt(numberOfAnswer);
// 			sx = spawnPos.x;
// 			shiftx = (sx * 2) / numberOfAnswer;
// 			placePos = new Vector3[numberOfAnswer];
// 			placePos[randOrder[0]] = new Vector3(Random.Range(-sx, -sx + shiftx - 0.5f), spawnPos.y, spawnPos.z);
// 			placePos[randOrder[1]] = new Vector3(Random.Range(-sx + shiftx, -sx + (2 * shiftx) - 0.5f), spawnPos.y, spawnPos.z);
// 			placePos[randOrder[2]] = new Vector3(Random.Range(-sx + (2 * shiftx), -sx + (3 * shiftx) - 0.5f), spawnPos.y, spawnPos.z);
// 			placePos[randOrder[3]] = new Vector3(Random.Range(-sx + (3 * shiftx), -sx + (4 * shiftx) - 0.5f), spawnPos.y, spawnPos.z);
// 			placePos[randOrder[4]] = new Vector3(Random.Range(-sx + (4 * shiftx), -sx + (5 * shiftx) - 0.5f), spawnPos.y, spawnPos.z);
			
// 			return placePos;
			
// 		case 4:
// 			randOrder = getRandInt(numberOfAnswer);
// 			sx = spawnPos.x;
// 			shiftx = (sx * 2) / numberOfAnswer;
// 			placePos = new Vector3[numberOfAnswer];
// 			placePos[randOrder[0]] = new Vector3(Random.Range(-sx, -sx + shiftx - 0.5f), spawnPos.y, spawnPos.z);
// 			placePos[randOrder[1]] = new Vector3(Random.Range(-sx + shiftx, -sx + (2 * shiftx) - 0.5f), spawnPos.y, spawnPos.z);
// 			placePos[randOrder[2]] = new Vector3(Random.Range(-sx + (2 * shiftx), -sx + (3 * shiftx) - 0.5f), spawnPos.y, spawnPos.z);
// 			placePos[randOrder[3]] = new Vector3(Random.Range(-sx + (3 * shiftx), -sx + (4 * shiftx) - 0.5f), spawnPos.y, spawnPos.z);
			
// 			return placePos;
// 		case 3:
// 			randOrder = getRandInt(numberOfAnswer);
// 			sx = spawnPos.x;
// 			shiftx = (sx * 2) / numberOfAnswer;
// 			placePos = new Vector3[numberOfAnswer];
// 			placePos[randOrder[0]] = new Vector3(Random.Range(-sx, -sx + shiftx - 0.5f), spawnPos.y, spawnPos.z);
// 			placePos[randOrder[1]] = new Vector3(Random.Range(-sx + shiftx, -sx + (2 * shiftx) - 0.5f), spawnPos.y, spawnPos.z);
// 			placePos[randOrder[2]] = new Vector3(Random.Range(-sx + (2 * shiftx), -sx + (3 * shiftx) - 0.5f), spawnPos.y, spawnPos.z);
			
// 			return placePos;
// 		case 2:
// 			randOrder = getRandInt(numberOfAnswer);
// 			sx = spawnPos.x;
// 			shiftx = (sx * 2) / numberOfAnswer;
// 			placePos = new Vector3[numberOfAnswer];
// 			placePos[randOrder[0]] = new Vector3(Random.Range(-sx, -sx + shiftx - 0.5f), spawnPos.y, spawnPos.z);
// 			placePos[randOrder[1]] = new Vector3(Random.Range(-sx + shiftx, -sx + (2 * shiftx) - 0.5f), spawnPos.y, spawnPos.z);
			
// 			return placePos;
// 		case 1:
// 			sx = spawnPos.x;
// 			shiftx = (sx * 2) / numberOfAnswer;
// 			placePos = new Vector3[numberOfAnswer];
// 			placePos[0] = new Vector3(Random.Range(-sx, -sx + shiftx - 0.5f), spawnPos.y, spawnPos.z);
			
// 			return placePos;
// 		default:
// 			placePos = new Vector3[1];
			
// 			return placePos;
// 		}
		
// 	}
	
// 	// Use this for initialization
// 	Vector3 randSpawnPos(float x, float y, float z)
// 	{
// 		return new Vector3(Random.Range(-x, x), y, z);
// 	}
	
// 	// function to create new question
// 	// digits is maximum random value
// 	// operation is string of "plus", "minus", "multiply" or "divide"
// 	string getQuestionText(int digits, string operation, out int answer)
// 	{
// 		int q1 = Random.Range(0, digits);
// 		int q2 = Random.Range(0, digits);
// 		int qtemp;
		
// 		if (q2 > q1)
// 		{
// 			qtemp = q1;
// 			q1 = q2;
// 			q2 = qtemp;
// 		}
		
// 		string questionText;
		
// 		switch (operation)
// 		{
// 		case "plus":
// 			answer = q1 + q2;
// 			questionText = q1.ToString() + " + " + q2.ToString();
// 			break;
// 		case "minus":
// 			answer = q1 - q2;
// 			questionText = q1.ToString() + " - " + q2.ToString();
// 			break;
// 		case "multiply":
// 			answer = q1 * q2;
// 			questionText = q1.ToString() + " * " + q2.ToString();
// 			break;
// 		case "divide":
// 			answer = q1 / q2;
// 			questionText = q1.ToString() + " / " + q2.ToString();
// 			break;
// 		default:
// 			answer = 0;
// 			questionText = "Wrong conditions";
// 			break;
// 		}
		
// 		return questionText;
// 	}
	
// 	// function to instantiate answer
// 	// type is real or dummy
// 	void instantiateAnswer(string type, int ans, GameObject answer, Vector3 spawnPos, Quaternion spawnRo)
// 	{
// 		switch (type)
// 		{
// 		case "dummy":
// 			if (Random.Range(0,1) == 1)
// 			{
// 				ans = ans + Random.Range(1, ans);
// 			}
// 			else
// 			{
// 				ans = ans - Random.Range(1, ans);
// 			}
			
// 			answer.tag = "Dummy";
// 			answerText.text = ans.ToString();
// 			answer.GetComponent<destroyAnswer>().scoreValue = -5;
// 			break;
// 		default:
// 			answer.tag = "Answer";
// 			answerText.text = ans.ToString();
// 			answer.GetComponent<destroyAnswer>().scoreValue = 10;
// 			break;
// 		}
		
// 		Instantiate(answer, spawnPos, spawnRo);
// 	}
	
// 	public void newQuestion(int maxNumber, string Operator, int numberOfDummy)
// 	{
// 		Quaternion spawnRotation = Quaternion.Euler(new Vector3(90, 0, 0));
// 		questionText.text = getQuestionText(maxNumber, Operator, out ans);
// 		Vector3[] ansPos = getAnswerRandomPosition(numberOfDummy, spawnValues);
		
//         //foreach(Vector3 a in ansPos)
//         //{
//         //    Debug.Log(a);
//         //}
//         //Debug.Log(numberOfDummy);
//         //Debug.Log(spawnValues);

// 		int i = 0;
// 		while (i < ansPos.Length)
// 		{
// 			if (i == 0)
// 			{
// 				instantiateAnswer("real", ans, answer, ansPos[i], spawnRotation);
// 			}
// 			else
// 			{
// 				instantiateAnswer("dummy", ans, answer, ansPos[i], spawnRotation);
// 			}
// 			i++;
// 		}
// 	}
// 	#endregion
// }
