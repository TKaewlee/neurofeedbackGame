using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceController : MonoBehaviour {
	public GameObject[] hazards;
	public Vector3 SpawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;
	public float distanceCount;

	public GUIText scoreText;
	public GUIText distanceText;
	public GUIText restartText;
	public GUIText gameOverText;

	private int score;
	private int distance;
	private bool gameOver;
	private bool restart;


	void Start ()
	{
		score = 0;
		distance = 0;
		gameOver = false;
		restart = false;
		restartText.text = "";
		gameOverText.text = "";
		
		UpdateScore ();
		UpdateDistance ();
		StartCoroutine (SpawnWaves ());
	}

	void Update ()
	{
		if (restart)
		{
			if (Input.GetKeyDown (KeyCode.R))
			{
				Debug.Log(Application.loadedLevel);
				Application.LoadLevel (Application.loadedLevel);
			}
		}
		AddDistance();
	}

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

			if (gameOver)
			{
				restartText.text = "Press 'R' for Restart";
				restart = true;
				break;
			}
		}
	}

	public void AddScore (int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore ();
	}

	void UpdateScore ()
	{
		scoreText.text = "Score: " + score;
	}

	private void AddDistance ()
	{
		distance += 1;
		UpdateDistance ();
	}

	void UpdateDistance ()
	{
		distanceText.text = "Distance: " + distance;
	}

	public void GameOver ()
	{
		gameOverText.text = "Game Over";
		gameOver = true;
	}
}
