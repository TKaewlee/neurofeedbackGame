using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour {
	[SerializeField] int playerLives = 99;
	[SerializeField] int score = 0;
	[SerializeField] Text scoreText;
	//[SerializeField] Text livesText;
	[SerializeField] float coincollections = 1;
	private void Awake(){
		int numGameSessions = FindObjectsOfType<GameSession>().Length;
		if(numGameSessions > 1)
		{
			Destroy(gameObject);
		}
		else{
			DontDestroyOnLoad(gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		//livesText.text = playerLives.ToString();
		scoreText.text = score.ToString();
	}
	void update(){
		
	}
	public void AddToScore (int pointsToAdd){
		score = score + pointsToAdd;
		scoreText.text = score.ToString();
		coincollections++;
	}
	public void ProcessPlayerDeath()
	{
		if (playerLives > 1)
		{
			TakeLife();
		}
		else{
		var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
		SceneManager.LoadScene(currentSceneIndex);
		playerLives = 3;
		//livesText.text = playerLives.ToString();
		}
	}
	public void TakeLife(){
		playerLives--;
		var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
		SceneManager.LoadScene(currentSceneIndex);
		//livesText.text = playerLives.ToString();
	}
	private void ResetGameSession(){
		SceneManager.LoadScene(0);
		Destroy(gameObject);
	}
}
