using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour 
{
	public float speed;
	public float tilt;
	public Boundary boundary;
	
	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;

	private float nextFire;

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

	void Update ()
	{
		if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
			gameController.minusScore (5);
            nextFire = Time.time + fireRate;
            // GameObject clone = 
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation); //as GameObject;
			GetComponent<AudioSource>().Play ();
        }
	}
	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		GetComponent<Rigidbody>().velocity = movement * speed;

		GetComponent<Rigidbody>().position = new Vector3 
		(
			Mathf.Clamp (GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax), 
			0.0f, 
			Mathf.Clamp (GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
		);

		GetComponent<Rigidbody>().rotation = 
		Quaternion.Euler (0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt);

	}
}
