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
	public Text restartText;
	public Text HighScoreText;
	private bool gameOver;

	void Start(){
		gameOver = false;
		restartText.gameObject.SetActive (false);
		HighScoreText.text = "High Score: " + UserDataManager.GetInstance ().GetPlayerHighScore ();
		score = 0;
		DisplayScoreText ();
		StartCoroutine (SpawnWaves ());
	}

	void Update(){
		if (gameOver) {
			if (Input.touchCount > 0){
				Touch touch = Input.GetTouch (0);
				if (touch.phase == TouchPhase.Began){
					RestartGame ();
				}
			}
			/*if(Input.touchCount > 0){
				Touch touch = Input.GetTouch(0);
				if (touch.position == restartButton.gameObject.){
					RestartGame ();
				}
			}*/
			/*if (restartButton.OnPointerDown) {
				RestartGame ();
			}*/
		}
	}


	public void RestartGame(){
		UserDataManager.GetInstance ().UpdateAdCount (UserDataManager.GetInstance().GetAdCount());
		if (UserDataManager.GetInstance ().GetAdCount() == 2) {
			ShowAd ();
			PlayerPrefs.SetInt ("Ad Count", 0);
		}
		Debug.Log ("restart button clicked!");
		Application.LoadLevel (Application.loadedLevel);
	}

	IEnumerator SpawnWaves(){
		while (true) {
			Vector3 spawnPosition = new Vector3 (SpawnValues.x, Random.Range (-SpawnValues.y, SpawnValues.y), SpawnValues.z);
			Quaternion spawnRotation = Quaternion.identity;
			Instantiate (brick, spawnPosition, spawnRotation);
			yield return new WaitForSeconds (spawnWait);

			if (gameOver){
				break;
			}
		}
	}

	public void AddScore(int NewScore){
		score += NewScore;
		DisplayScoreText ();
	}

	void DisplayScoreText(){
		ScoreText.text = "Score: " + score;
	}

	public void ShowAd()
	{
		if (Advertisement.IsReady())
		{
			Advertisement.Show();
		}
	}

	public void GameOver(){
		//Debug.Log ("Game is Over!");
		UserDataManager.GetInstance ().UpdatePlayerHighScore (score);
		gameOver = true;
		restartText.gameObject.SetActive (true);
		GameObject.FindObjectOfType<PlayerController> ().gameObject.transform.localScale = Vector3.zero;
	}

}
