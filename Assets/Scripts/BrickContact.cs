using UnityEngine;
using System.Collections;

public class BrickContact : MonoBehaviour {

	public int ScoreValue;
	public float FallSpeed;
	private GameController gameController;

	bool isFalling = true;
	//var playerObject = GameObject.Find ("Player");

	void Start(){
		GameObject GameControllerObject = GameObject.FindWithTag ("GameController");
		if (GameControllerObject != null) {
			gameController = GameControllerObject.GetComponent<GameController>();
		}
		if (gameController == null) {
			Debug.Log ("Cannot find 'GameController' object");
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			isFalling = false;
			//GetComponent<Collider>().isTrigger = false;
		} 
		else {
			if(gameController != null){
				gameController.AddScore (ScoreValue);
			}
		}
	}

	void OnTriggerExit(Collider other) {
		isFalling = true;
		//GetComponent<Collider> ().isTrigger = true;
		//Score++;
	}

	void Update(){
		//Debug.Log (isFalling);
		if (isFalling == true) {
			//Debug.Log ("Player is falling");
			GameObject.Find ("Player").transform.position += new Vector3 (FallSpeed, 0, 0);
			//GetComponent<Collider>().isTrigger = true;
		}
	}
}
