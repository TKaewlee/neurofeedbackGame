using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceRandomRotator : MonoBehaviour 
{
	public float tumble;

	void Start ()
	{
		GetComponent<Rigidbody>().angularVelocity = Random.insideUnitCircle * tumble;
	}
}
