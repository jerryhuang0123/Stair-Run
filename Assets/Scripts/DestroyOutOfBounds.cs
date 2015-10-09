using UnityEngine;
using System.Collections;

public class DestroyOutOfBounds : MonoBehaviour {

	//public GameObject BrickObject;
	//public GameObject BrickPrefab;
	private GameController gameController;

	void Start(){
		GameObject GameControllerObject = GameObject.FindWithTag ("GameController");
		if (GameControllerObject != null) {
			gameController = GameControllerObject.GetComponent<GameController>();
		}
		if (gameController == null) {
			Debug.Log ("Cannot find 'GameController' object");
		}
		//BrickObject = GameObject.FindWithTag ("Brick");
		//Instantiate(BrickPrefab, BrickObject.transform.position, BrickObject.transform.rotation) as GameObject;
	}
	

	void OnTriggerExit(Collider other){
		if (other.tag == "Brick") {
			Destroy (other.gameObject);
			//other.gameObject.SetActive (false);
		}

		if (other.tag == "Player") {
			if(gameController != null){
				//Debug.Log ("Game has ended");
				gameController.GameOver();
			}

		}
	}


}
