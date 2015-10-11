using UnityEngine;
using System.Collections;

public class AddScore : MonoBehaviour {

	public int ScoreValue;
	private GameController gameController;

	//bool isFalling = true;

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
			if(gameController != null){
				gameController.AddScore (ScoreValue);
			}
		} 

	}
	
}
