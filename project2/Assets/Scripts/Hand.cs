using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour {
	public bool isLeftHand;
	GameObject playerParent;
	float holdHandCD;
	GameObject otterParent;
	// Use this for initialization
	void Start () {
		playerParent = gameObject.transform.parent.gameObject;
		holdHandCD = 3;
	}

	// Update is called once per frame
	void Update () {
		if (holdHandCD>=0) {
			holdHandCD -= Time.deltaTime;
		}
	}

	void OnTriggerEnter2D(Collider2D thisCollision) {

		if ((thisCollision.gameObject.CompareTag ("LeftHand")|| thisCollision.gameObject.CompareTag ("RightHand")) && !playerParent.GetComponent<PlayerController>().isHolding) {

		
			otterParent = thisCollision.gameObject.transform.parent.gameObject;
			playerParent.AddComponent<SpringJoint2D> ();  
			playerParent.GetComponent<SpringJoint2D>().connectedBody=otterParent.GetComponent<Rigidbody2D>() ;
			playerParent.GetComponent<SpringJoint2D> ().autoConfigureDistance = false;
			playerParent.GetComponent<SpringJoint2D> ().distance = 0.001f;
			playerParent.GetComponent<SpringJoint2D> ().enableCollision = true;
			playerParent.GetComponent<SpringJoint2D> ().frequency = 3.0f;
			playerParent.GetComponent<SpringJoint2D> ().dampingRatio = 0.7f;

			if (isLeftHand) {
				playerParent.GetComponent<SpringJoint2D> ().anchor = new Vector2 (-0.25f, 0.25f);
				playerParent.GetComponent<PlayerController> ().HoldLeftHand ();
			} else {
				playerParent.GetComponent<SpringJoint2D> ().anchor = new Vector2 (0.25f, 0.25f);
				playerParent.GetComponent<PlayerController> ().HoldRightHand ();
			}

			if (thisCollision.gameObject.CompareTag ("LeftHand")) {
				playerParent.GetComponent<SpringJoint2D> ().connectedAnchor = new Vector2 (-0.25f, 0.25f);

				otterParent.GetComponent<Otter> ().HoldLeftHand ();
			}
			if (thisCollision.gameObject.CompareTag ("RightHand")) {
				playerParent.GetComponent<SpringJoint2D> ().connectedAnchor = new Vector2 (0.25f, 0.25f);
				otterParent.GetComponent<Otter> ().HoldRightHand ();

			}

			playerParent.GetComponent<PlayerController>().isHolding = true;
			playerParent.GetComponent<PlayerController> ().holdingHand = gameObject;
		}
	}

	public void Release(){
		holdHandCD = 3.0f;
		otterParent.GetComponent<Otter> ().ReleaseHand ();
		otterParent = null;

	}

}
