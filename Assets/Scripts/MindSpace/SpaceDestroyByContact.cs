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
			gameController.MinusScore (scoreValue);
			// gameController.GameOver ();
		}

        if (other.tag == "Bolt")
        {
            //Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            gameController.MinusScore(scoreValue * (-1));
            Destroy(other.gameObject);
            // gameController.GameOver ();
        }

        if (other.tag == "Asteroid" || other.tag == "Reward")
		{
			return;
		}
		else
		{
			// if(gameObject.tag == "Reward" && other.tag == "Bolt")
			// {
			// 	gameController.trigger = 1;
			// 	gameController.minusScore (scoreValue);
			// }
			Destroy(gameObject);
			Instantiate(explosion, transform.position, transform.rotation); 
		}
	}
}
