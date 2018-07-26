using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour {
	[SerializeField] float moveSpeed = 1f;
	Rigidbody2D myRigidBody;
	CircleCollider2D myHead;
	Animator myAnimator;
	// Use this for initialization
	void Start () {
		myRigidBody = GetComponent<Rigidbody2D>();
		myHead = GetComponent<CircleCollider2D>();
		myAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (IsFacingRight()){
		myRigidBody.velocity = new Vector2(-moveSpeed, 0f);
		}
		else {
		myRigidBody.velocity = new Vector2(moveSpeed, 0f);
		}
	}

	bool IsFacingRight(){
		return transform.localScale.x < 0;
	}

	private void OnTriggerExit2D(Collider2D collision){
		transform.localScale = new Vector2((Mathf.Sign(myRigidBody.velocity.x)) * 5, 5f);
	}
	private void Hit(){
		if (myHead.IsTouchingLayers(LayerMask.GetMask("Player"))){
			FindObjectOfType<Player>().Kill();
			myRigidBody.velocity = new Vector2(moveSpeed * 0, 0f);
			myAnimator.SetTrigger("Dead");
			StartCoroutine(Destroy());
		}
	}
	IEnumerator Destroy(){
		yield return new WaitForSecondsRealtime(0.5f);
		Destroy(gameObject);
	}
}
