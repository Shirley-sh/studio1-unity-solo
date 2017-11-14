using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	//private properties (like P5's global variables) - but inaccessible by other scripts)
	Rigidbody2D rb;
	Camera cam;
	Animator anim;

	float targetAngle;
	Vector3 targetPosition;

	[Header("Acceleration")]
	public float acceleration;
	[Header("Turning Speed")]
	public float turnSpeed;

	public bool isHolding;
	public GameObject holdingHand;
	public GameObject waterParticle;

	public int score;

	float swimTimer;

	// Use this for initialization
	void Start () {
		isHolding = false;
		rb = GetComponent<Rigidbody2D> ();
		cam = Camera.main;
		anim = GetComponent<Animator> ();
		swimTimer = 0;
		waterParticle.GetComponent<ParticleSystem> ().Pause ();
		score = 0;
	}
	void Update(){
		if (swimTimer >= 0) {
			swimTimer -= Time.deltaTime;
		}

		if (swimTimer > 0) {
			anim.SetBool ("Swim", true);
			waterParticle.GetComponent<ParticleSystem> ().Play ();
		} else {
			anim.SetBool ("Swim", false);
			waterParticle.GetComponent<ParticleSystem> ().Pause();
		}


	}
	// FixedUpdate is called before the physics system updates
	void FixedUpdate () {
		if (Input.GetMouseButtonDown (0)) {
			//update target angle and position
			// Distance from camera to object.  We need this to get the proper calculation.
			float camDis = cam.transform.position.y - GetComponent <Transform> ().position.y;

			// Get the mouse position in world space. Using camDis for the Z axis.
			Vector3 mouse = cam.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, camDis));

			float AngleRad = Mathf.Atan2 (mouse.y - GetComponent <Transform> ().position.y, mouse.x - GetComponent <Transform> ().position.x);
			targetAngle = (180 / Mathf.PI) * AngleRad -90;
			targetPosition = mouse;

			swimTimer = 3;
		}

		if (Input.GetMouseButtonDown (1) && isHolding) {
			Debug.Log ("ReleaseHand");

			ReleaseHand ();
			isHolding = false;

		}


		Vector3 force = targetPosition - transform.position;
		force.Normalize ();
		rb.AddForce (force*acceleration);
		rb.MoveRotation (Mathf.LerpAngle(rb.rotation, targetAngle,turnSpeed));
	}


	void OnCollisionEnter2D(Collision2D thisCollision){
		if (thisCollision.gameObject.CompareTag ("Collectable")) {
			Destroy (thisCollision.gameObject);
			score += 1;

		}
	}

	public void ReleaseHand(){
		//set current hand cd time to 3
		holdingHand.GetComponent<Hand>().Release();
		holdingHand = null;


		//remove joint
		Destroy (GetComponent<SpringJoint2D>());
		Debug.Log ("Removed");


		anim.SetBool ("Left", false);
		anim.SetBool ("Right", false);
	}


	public void HoldLeftHand(){
		anim.SetBool ("Left", true);
	}

	public void HoldRightHand(){
		anim.SetBool ("Right", true);
	}
		

}

