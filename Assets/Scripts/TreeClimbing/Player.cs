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
	float timer;
	private static List<float> timefocus = new List<float>();
	private float TimeClimbing;
	//float[] timeCollect;
	private Read2UDP read2UDP;
	public float Alpha = 1.0f;
	public float baseAlpha = 1.0f;
	private static List<float> dataAvgChanged = new List<float>();
	// private float dataAvg;
	private bool isSaved;
	public static Dictionary<string, string> tempCalibation = new Dictionary<string, string>();
	public float a,i=0.0f;

    public float baseline;
    public float threshold;

	// Use this for initialization
	void Start () {
		baseline = GameControl.currentBaselineAvg;
        threshold = GameControl.currentThresholdAvg;
		isSaved = false;
        GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("UDPReciever");
        if (gameControllerObject != null)
        {
            read2UDP = gameControllerObject.GetComponent <Read2UDP>();
        }
        if (read2UDP == null)
        {
            Debug.Log("Cannot find 'read2UDP' script");
        }
		myRigidBody = GetComponent<Rigidbody2D>();
		myAnimator = GetComponent<Animator>();
		myFeet = GetComponent<BoxCollider2D>();
		myCirclebody = GetComponent<CircleCollider2D>();
		myWeapon = GetComponent<PolygonCollider2D>();
		gravityScaleAtStart = myRigidBody.gravityScale;
	}
	
	// Update is called once per frame
	void Update () {
		if(timeController.isSaving)
		{
			Alpha = read2UDP.dataTempChanged;
			dataAvgChanged.Add(Alpha);
            a=1-((baseline-Alpha)/threshold);
			if (!isAlive){ return; }
			Run();
			Jump();
			FlipSprite();
			ClimbLadder();
			Hurt();	
        }
        // Read2UDP.tempData["average"] = dataAvg.ToString();
        // dataAvgChanged.Clear();
        // timeController.isSetAvg = false;
		if (!isAlive){ return; }
		if(timeController.isOnSave)
		{
			Read2UDP.tempData["FocusingTime"] = DataController.GameDataController.getAppendString(timefocus);
		}
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
			if(timer == 1){
				timefocus.Add(read2UDP.timeTempChanged);
			//	timeCollect = new float[] {Time.timeSinceLevelLoad};
				timer = 0;
			}
			return;
		}
		
		//float controlThrow = CrossPlatformInputManager.GetAxis("Vertical");
		//Vector2 climbVelocity = new Vector2(myRigidBody.velocity.x, controlThrow * climbSpeed);
		float controlThrow = a;
		Vector2 climbVelocity = new Vector2(myRigidBody.velocity.x, controlThrow * climbSpeed);
		myRigidBody.velocity = climbVelocity;
		myRigidBody.gravityScale = 0f;

		bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.y) > Mathf.Epsilon;
		myAnimator.SetBool("Climbing", playerHasHorizontalSpeed);
		if(timer == 0){
		timefocus.Add(read2UDP.timeTempChanged);
		//timeCollect = new float[] {Time.timeSinceLevelLoad};
		timer = 1;
		}
		
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
			StartCoroutine(Freeze());
			GetComponent<Rigidbody2D>().velocity = deathkick;
			//FindObjectOfType<GameSession>().ProcessPlayerDeath();
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
