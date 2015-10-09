using UnityEngine;
using System.Collections;

public class BrickMover : MonoBehaviour {

	public float speed;
	public Rigidbody brick;
	void Start () {
		brick = GetComponent<Rigidbody> ();
	}
	void Update(){
		brick.velocity = new Vector3 (-speed, 0, 0);
	}
}
