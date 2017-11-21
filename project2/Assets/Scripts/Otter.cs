using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Otter : MonoBehaviour {
	public GameObject waterParticle;

	//private properties (like P5's global variables) - but inaccessible by other scripts)
	Rigidbody2D rb;
	Animator anim;

	float targetAngle;
	Vector3 targetPosition;
	float timer;
	bool hitLand;
	float faceTimer;
	bool alive;

	[Header("Move Range")]
	public float range;
	[Header("Timer")]
	public float timerMin;
	public float timerMax;
	[Header("Acceleration")]
	public float acceleration;
	[Header("Turning Speed")]
	public float turnSpeed;

    public GameObject thePlayer;
    bool holdingHands;

	[Header("Animation Change Speed")]
	public float swimSpeed;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		timer = Random.Range (timerMin, timerMax);
		hitLand = false;
		alive = true;
		faceTimer = 10;
		waterParticle.GetComponent<ParticleSystem> ().Pause ();
		anim.SetBool ("Swim", false);

        holdingHands = thePlayer.GetComponent<PlayerController>().isHolding;
	}

	void Update(){
		timer -= Time.deltaTime;
        if (holdingHands == false) {
            if (timer <= 0) {
                targetPosition = transform.position + new Vector3(Random.Range(-range, range), Random.Range(-range, range), 0);
                float AngleRad = Mathf.Atan2(targetPosition.y - GetComponent<Transform>().position.y, targetPosition.x - GetComponent<Transform>().position.x);
                targetAngle = (180 / Mathf.PI) * AngleRad - 90;
                timer = Random.Range(timerMin, timerMax);
                hitLand = false;
            }
        }
        
		faceTimer -= Time.deltaTime;


		//animation controls
		if (rb.velocity.magnitude > swimSpeed) {
			anim.SetBool ("Swim", true);
			waterParticle.GetComponent<ParticleSystem> ().Play ();
		} else {
			anim.SetBool ("Swim", false);
			if (Random.Range (0, 100) < 0.1 && faceTimer<0) {
				anim.SetTrigger ("Face");
				faceTimer = 10;
			}
			//waterParticle.GetComponent<ParticleSystem> ().Pause ();
		}
	}

	// FixedUpdate is called before the physics system updates
	void FixedUpdate () {
		Vector3 force = targetPosition - transform.position;
		force.Normalize ();
		rb.AddForce (force*acceleration);
		rb.MoveRotation (Mathf.LerpAngle(rb.rotation, targetAngle,turnSpeed));
	}


	void OnTriggerEnter2D(Collider2D thisCollision){
		if (thisCollision.gameObject.CompareTag ("Bound")) {
			//game over
			alive = false;
			Debug.Log ("OUT");
		}
	}

	public void HoldLeftHand(){
		anim.SetBool ("Left", true);
	}

	public void HoldRightHand(){
		anim.SetBool ("Right", true);
	}

	public void ReleaseHand(){
		anim.SetBool ("Left", false);
		anim.SetBool ("Right", false);
	}

	public bool isAlive(){
		return alive;
	}
}