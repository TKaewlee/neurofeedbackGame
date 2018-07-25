using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {

	[SerializeField] float runSpeed = 10f;
	[SerializeField] float JumpSpeed = 10f;
	[SerializeField] float climbSpeed = 10f;
	[SerializeField] Vector2 deathkick = new Vector2(-30f, 15f);

	bool isAlive = true;
	Rigidbody2D myRigidBody;
	Animator myAnimator;
	BoxCollider2D myFeet;
	CircleCollider2D myCirclebody;
	PolygonCollider2D myWeapon;
	float gravityScaleAtStart;
	float x;
	//float timer;
	//float[] timeCollect;
	private Read2UDP read2UDP;

	// Use this for initialization
	void Start () {
		myRigidBody = GetComponent<Rigidbody2D>();
		myAnimator = GetComponent<Animator>();
		myFeet = GetComponent<BoxCollider2D>();
		myCirclebody = GetComponent<CircleCollider2D>();
		myWeapon = GetComponent<PolygonCollider2D>();
		gravityScaleAtStart = myRigidBody.gravityScale;
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("UDPReceiver");
		if (gameControllerObject != null)
        {
            read2UDP = gameControllerObject.GetComponent <Read2UDP>();
        }
	}
	
	// Update is called once per frame
	void Update () {
		if (!isAlive){ return; }
		x = (float)read2UDP.dataChanged;
		Run();
		Jump();
		FlipSprite();
		ClimbLadder();
		Hurt();
	}

	private void Run(){
		float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal");
		Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidBody.velocity.y);
		myRigidBody.velocity = playerVelocity;

		bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
		myAnimator.SetBool("Running", playerHasHorizontalSpeed);
	}

	private void ClimbLadder(){
		if(!myCirclebody.IsTouchingLayers(LayerMask.GetMask("Climbing"))){
			myAnimator.SetBool("Climbing", false);
			myRigidBody.gravityScale = gravityScaleAtStart;
			//if(timer == 1){
			//	timeCollect = new float[] {Time.timeSinceLevelLoad};
			//	timer = 0;
			//}
			return;
		}
		
		float controlThrow = CrossPlatformInputManager.GetAxis("Vertical");
		Vector2 climbVelocity = new Vector2(myRigidBody.velocity.x, controlThrow * climbSpeed);
		//float controlThrow = x;
		//Vector2 climbVelocity = new Vector2(myRigidBody.velocity.x, controlThrow * climbSpeed);
		myRigidBody.velocity = climbVelocity;
		myRigidBody.gravityScale = 0f;

		bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.y) > Mathf.Epsilon;
		myAnimator.SetBool("Climbing", playerHasHorizontalSpeed);

		//timeCollect = new float[] {Time.timeSinceLevelLoad};
		//timer = 1;

	}
	private void Jump(){
		if(!myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"))){return;}
		if (CrossPlatformInputManager.GetButtonDown("Jump")){
			Vector2 jumpVelocityToAdd = new Vector2(0f, JumpSpeed);
			myRigidBody.velocity += jumpVelocityToAdd;
		}
	}

	private void Hurt(){
		if(myCirclebody.IsTouchingLayers(LayerMask.GetMask("Enemy", "Harzards"))){
			isAlive = false;
			myAnimator.SetTrigger("Dying");
			//StartCoroutine(Freeze());
			GetComponent<Rigidbody2D>().velocity = deathkick;
			FindObjectOfType<GameSession>().ProcessPlayerDeath();
		}
	}
	public void Kill(){
		Vector2 jumpVelocityToAdd = new Vector2(0f, JumpSpeed);
		myRigidBody.velocity = jumpVelocityToAdd;
	}

	private void FlipSprite(){
		bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
		if (playerHasHorizontalSpeed)
		{
			transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x) * 5, 5f);
		}
	}
	IEnumerator Freeze(){
        yield return new WaitForSecondsRealtime(2);
		isAlive = true;
	}

}
