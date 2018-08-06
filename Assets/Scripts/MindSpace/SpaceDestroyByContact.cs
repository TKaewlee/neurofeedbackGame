using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceDestroyByContact : MonoBehaviour 
{
	public GameObject explosion;
	public GameObject playerExplosion;

	public int scoreValue;
	private SpaceController gameController;

	void Start ()
	{
		GameObject gameControllerObject = GameObject.FindWithTag("SpaceController");
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent<SpaceController>();
		}
		if (gameControllerObject == null)
		{
			Debug.Log("Connot find 'GameController' script");
		}
		
	}
	void OnTriggerEnter(Collider other){
		if (other.tag == "Boundary")
		{
			return;
		}

		if (other.tag == "Player")
		{
			Instantiate(playerExplosion, other.transform.position, other.transform.rotation); 
			gameController.minusScore (scoreValue);
			// gameController.GameOver ();
		}
		// gameController.AddScore (scoreValue);
		// Destroy(other.gameObject);

		if (other.tag == "Asteroid" || other.tag == "Reward")
		{
			return;
		}
		else
		{
			if(gameObject.tag == "Reward" && other.tag == "Bolt")
			{
				gameController.trigger = 1;
				gameController.minusScore (scoreValue);
			}
			Destroy(gameObject);
			Instantiate(explosion, transform.position, transform.rotation); 
		}
	}
}
