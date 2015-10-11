using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float PhoneXdir;
	public float tilt;
	private GameController gameController;
	private bool isFalling = true;
	[Range(0.0f, 3.0f)]
	public float FallSpeed;
	public int ScoreValue;
	Vector3 movement = new Vector3 ();


	void Start(){
		GameObject GameControllerObject = GameObject.FindWithTag ("GameController");
		if (GameControllerObject != null) {
			gameController = GameControllerObject.GetComponent<GameController>();
		}
		if (gameController == null) {
			Debug.Log ("Cannot find 'GameController' object");
		}
	}


	void OnCollisionEnter(Collision other){
		if (other.collider.tag == "Brick") {
			Debug.Log ("Collision!");
			isFalling = false;
			if(gameController != null){
				gameController.AddScore (ScoreValue);
			}
		} 
	}

	void OnCollisionExit(Collision other){
		Debug.Log ("Collision exit!");
		if (other.collider.tag == "Brick") {
			isFalling = true;
		}
	}

	void FixedUpdate () {
		Vector3 ScreenPosition = Camera.main.WorldToViewportPoint (transform.position);	
		//ScreenPosition.x = Mathf.Clamp01 (ScreenPosition.x);
		ScreenPosition.y = Mathf.Clamp01 (ScreenPosition.y);
		transform.position = Camera.main.ViewportToWorldPoint (ScreenPosition);
		GetComponent<Rigidbody>().freezeRotation = true;
		Debug.Log ("Velocity: " + GetComponent<Rigidbody> ().velocity);
		Debug.Log ("movement: " + movement);
		//if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began) 
		for (var i = 0; i < Input.touchCount; i++){
			Touch touch = Input.GetTouch (i);
			if (touch.phase == TouchPhase.Began){
				if (touch.position.x > (Screen.width/2)){
					movement.x = -PhoneXdir;
					GetComponent<Rigidbody>().velocity = movement;
				}
				else{
						movement.x = PhoneXdir;
						GetComponent<Rigidbody>().velocity = movement;
				}
			}

			else if (touch.phase == TouchPhase.Ended){
				movement.x = 0;
				GetComponent<Rigidbody>().velocity = movement;
			}
		}

		if (isFalling == true) {
			//Debug.Log ("Player is falling");
			transform.position += new Vector3 (0, -FallSpeed, 0);
			//GetComponent<Collider>().isTrigger = true;
		}

		GetComponent<Rigidbody> ().rotation = Quaternion.Euler (0.0f, GetComponent<Rigidbody>().velocity.x*tilt, 0.0f);
	}
	
}
