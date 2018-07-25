using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour {
	[SerializeField] AudioClip coinPickupSFX;
	[SerializeField] int pointsForCoinPickup = 100;
	BoxCollider2D coinBody;
void Start () {
		coinBody = GetComponent<BoxCollider2D>();
	}
	// Update is called once per frame
	void Update () {
		Getcoin();
	}

	private void Getcoin ()
	{
		if(coinBody.IsTouchingLayers(LayerMask.GetMask("Player"))){
		FindObjectOfType<GameSession>().AddToScore(pointsForCoinPickup);
		AudioSource.PlayClipAtPoint(coinPickupSFX, Camera.main.transform.position);
		Destroy(gameObject);
		}
	}
}
