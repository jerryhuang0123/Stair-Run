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
			isFalling = false;
			if(gameController != null){
				gameController.AddScore (ScoreValue);
			}
		} else {
		}
	}

	void OnCollisionExit(Collision other){
		if (other.collider.tag == "Brick") {
			isFalling = true;
		}
	}

	void FixedUpdate () {
		Vector3 ScreenPosition = Camera.main.WorldToViewportPoint (transform.position);	
		//ScreenPosition.x = Mathf.Clamp01 (ScreenPosition.x);
		ScreenPosition.y = Mathf.Clamp01 (ScreenPosition.y);
		transform.position = Camera.main.ViewportToWorldPoint (ScreenPosition);
		Vector3 movement = new Vector3 ();
		GetComponent<Rigidbody>().freezeRotation = true;

		for (var i = 0; i < Input.touchCount; i++) {
			Touch touch = Input.GetTouch (i);
			if (touch.phase == TouchPhase.Began){
				if (touch.position.y > (Screen.height/2)){
					movement.y = PhoneXdir;
					GetComponent<Rigidbody>().velocity = movement;
				}
				else{
						movement.y = -PhoneXdir;
						GetComponent<Rigidbody>().velocity = movement;
				}
			}

			else if (touch.phase == TouchPhase.Ended){
				movement.y = 0;
				GetComponent<Rigidbody>().velocity = movement;
			}
		}
		if (isFalling == true) {
			//Debug.Log ("Player is falling");
			transform.position += new Vector3 (FallSpeed, 0, 0);
			//GetComponent<Collider>().isTrigger = true;
		}
		GetComponent<Rigidbody> ().rotation = Quaternion.Euler (GetComponent<Rigidbody>().velocity.y*tilt, 0.0f, 0.0f);
	}
	
}
