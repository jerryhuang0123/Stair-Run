using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Advertisements;

public class GameController : MonoBehaviour {
	public GameObject brick;
	public Vector3 SpawnValues;
	public float spawnWait;
	private int score;
	public Text ScoreText;
	public Text HighScoreText;
	private bool gameOver;

	void Start(){
		gameOver = false;
		HighScoreText.text = "High Score: " + UserDataManager.GetInstance ().GetPlayerHighScore ();
		score = 0;
		DisplayScoreText ();
		StartCoroutine (SpawnWaves ());
	}

	/*void Update(){
		if (gameOver) {
			if (Input.touchCount > 0){
				Touch touch = Input.GetTouch (0);
				if (touch.phase == TouchPhase.Began){
					RestartGame ();
				}
			}
		}
	}*/

	IEnumerator SpawnWaves(){
		while (true) {
			Vector3 spawnPosition = new Vector3 (Random.Range (-SpawnValues.x, SpawnValues.x), SpawnValues.y, SpawnValues.z);
			Quaternion spawnRotation = Quaternion.identity;
			Instantiate (brick, spawnPosition, spawnRotation);
			yield return new WaitForSeconds (spawnWait);

			if (gameOver){
				break;
			}
		}
	}

	public bool getGameOverValue(){
		return this.gameOver;
	}
	public void setGameOverValue(bool gameOver){
		this.gameOver = gameOver; 
	}

	public void AddScore(int NewScore){
		score += NewScore;
		DisplayScoreText ();
	}

	void DisplayScoreText(){
		ScoreText.text = "Score: " + score;
	}

	public void GameOver(){
		Time.timeScale = 0;
		UserDataManager.GetInstance ().UpdatePlayerHighScore (score);
		GameObject.FindObjectOfType<PlayerController> ().gameObject.transform.localScale = Vector3.zero;
	}

}
