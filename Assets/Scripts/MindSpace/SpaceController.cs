using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;               // ui objects

public class SpaceController : MonoBehaviour {
	public GameObject[] hazards;
	public GameObject[] rewards;
	public Vector3 SpawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;
	public float distanceCount;

	public Text scoreText;
	// public GUIText distanceText;
	// public GUIText restartText;
	// public GUIText gameOverText;

	private int score;
	private int distance;
	public Text[] distanceTexts;
	public CanvasGroup distanceCanvas;
	public CanvasGroup slideCanvas;
	// private bool gameOver;
	// private bool restart;


	void Start ()
	{
		score = 0;
		distance = 350;
		// gameOver = false;
		// restart = false;
		// restartText.text = "";
		// gameOverText.text = "";
		
		UpdateScore ();
		// UpdateDistance ();
		StartCoroutine (SpawnWaves ());
		// StartCoroutine (RewardWaves ());
	}

	void Update ()
	{
		if(timeController.isContinue)
		{
			if(timeController.modeName == "without NF")
			{
				distanceCanvas.alpha = 1;
				AddDistance();
			}
			else if(timeController.modeName == "NF with slider")
			{
				slideCanvas.alpha = 1;
				
			}
			else if(timeController.modeName == "NF with moving object")
			{
				print("Hello");
			}
		}
		else
		{
			distanceCanvas.alpha = 0;
			slideCanvas.alpha = 0;
		}

		// if (restart)
		// {
		// 	if (Input.GetKeyDown (KeyCode.R))
		// 	{
		// 		Debug.Log(Application.loadedLevel);
		// 		Application.LoadLevel (Application.loadedLevel);
		// 	}
		// }
	}

	// IEnumerator RewardWaves ()
	// {
	// 	// yield return new WaitForSeconds (startWait);
	// 	while((distance / 35) % 10 == 0)
	// 	{
	// 		// for (int i = 0; i < hazardCount; i++)
	// 		// {
	// 			// GameObject hazard = hazards [Random.Range (0, hazards.Length)];
	// 		// 	Vector3 spawnPosition = new Vector3 (Random.Range (-SpawnValues.x, SpawnValues.x), SpawnValues.y, SpawnValues.z);
	// 		// 	Quaternion spawnRotation = Quaternion.identity;
	// 		// 	Instantiate (hazard, spawnPosition, spawnRotation);
	// 		// 	yield return new WaitForSeconds (spawnWait);
	// 		// }
	// 		yield return new WaitForSeconds (waveWait);
	// 	}
	// }

	IEnumerator SpawnWaves ()
	{
		yield return new WaitForSeconds (startWait);
		while(true)
		{
			for (int i = 0; i < hazardCount; i++)
			{
				GameObject hazard = hazards [Random.Range (0, hazards.Length)];
				Vector3 spawnPosition = new Vector3 (Random.Range (-SpawnValues.x, SpawnValues.x), SpawnValues.y, SpawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);

			// if (gameOver)
			// {
			// 	// restartText.text = "Press 'R' for Restart";
			// 	restart = true;
			// 	break;
			// }
		}
	}

	public void minusScore (int newScoreValue)
	{
		score -= newScoreValue;
		UpdateScore ();
	}

	void UpdateScore ()
	{
		scoreText.text = "Score: " + score;
	}

	private void AddDistance ()
	{
		distance += 1;
		if(distance % 35 == 0)
		{
			for (int i = 0; i < distanceTexts.Length; i++)
			{
				if(((distance / 35)-i) % 10 == 0)
				{
					distanceTexts[i].fontSize = 50;
				}
				else
				{
					distanceTexts[i].fontSize = 20;
				}
				distanceTexts[i].text = ((distance / 35)-i).ToString();
			}
			if((distance % 350) == 0)
			{
				minusScore(-10);
			}
		}
	}


	// public void GameOver ()
	// {
	// 	gameOverText.text = "Game Over";
	// 	gameOver = true;
	// }
}
