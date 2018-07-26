using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eagle : MonoBehaviour {

	// Use this for initialization
	BoxCollider2D myHead;
	Animator myAnimator;
	void Start () {
		myHead = GetComponent<BoxCollider2D>();
		myAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		Hit();
	}
	private void Hit(){
		if (myHead.IsTouchingLayers(LayerMask.GetMask("Player"))){
			FindObjectOfType<Player>().Kill();
			myAnimator.SetTrigger("Dead");
			StartCoroutine(Destroy());
		}
	}
	IEnumerator Destroy(){
		yield return new WaitForSecondsRealtime(0.5f);
		Destroy(gameObject);
	}
}
