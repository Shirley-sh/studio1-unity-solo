using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager: MonoBehaviour {
	public float countdownTimer;
	public GameObject[] lands;
	public GameObject[] shells;
	public GameObject[] otters;
	public GameObject player;
	public Text timer;
	public Text score;
	public int targetSocre;
	public bool gameover;
	bool flooded;


	public GameObject levelClearedPanel;
	public GameObject gameOverPanel;
	void Start(){
		lands = GameObject.FindGameObjectsWithTag ("Land");
		otters = GameObject.FindGameObjectsWithTag ("Otter");
		gameover = false;
		flooded = false;
	}

	// Update is called once per frame
	void Update () {
		foreach (GameObject otter in otters) {
			if (!otter.GetComponent<Otter> ().isAlive ()) {
				gameover = true;
				break;
			}
		}

		if (!gameover) {
			if (player.GetComponent<PlayerController> ().score == targetSocre) {
				gameover = true;
				levelClearedPanel.SetActive (true);
			}



			if (countdownTimer > 0 && !flooded) {
				countdownTimer -= Time.deltaTime;
			} else {
				countdownTimer = 0;
			}
			if (countdownTimer < 1 && !flooded) {
				foreach (GameObject go in lands) {
					go.GetComponent<Land> ().flood ();

				}
				shells = GameObject.FindGameObjectsWithTag ("Collectable");
				foreach (GameObject shell in shells) {
					shell.GetComponent<Wave> ().Flood ();
				}
			}
			//update ui
			timer.text = "" + (int)countdownTimer;
			score.text = player.GetComponent<PlayerController> ().score + " / 10";
		} else {
			gameOverPanel.SetActive (true);
		}
	}
}
