using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Advertisements;

public class MainMenu : MonoBehaviour {
	private bool ShowMenu;

	private int buttonWidth = 200;
	private int buttonHeight = 50;
	private int groupWidth = 200;
	private int groupHeight = 170;

	private GameController gameController;

	void Start () {
		GameObject GameControllerObject = GameObject.FindWithTag ("GameController");
		if (GameControllerObject != null) {
			gameController = GameControllerObject.GetComponent<GameController>();
		}
		if (gameController == null) {
			Debug.Log ("Cannot find 'GameController' object");
		}

		ShowMenu = true;
		//Screen.lockCursor = true;
		Time.timeScale = 1;
	}

	void OnGUI(){
		if (ShowMenu) {
			GUI.BeginGroup(new Rect(((Screen.width/2) - (groupWidth/2)), ((Screen.height/2) - (groupHeight/2)), groupWidth, groupHeight));
			if(GUI.Button(new Rect(0, 0, buttonWidth, buttonHeight), "Start Game")){
				RestartGame ();
			}
			if(GUI.Button(new Rect(0, 60, buttonWidth, buttonHeight), "Quit Game")){
				Application.Quit();
			}

			GUI.EndGroup ();
		}
	}
	void Update () {
		ShowMenu = gameController.getGameOverValue ();
	}

	public void RestartGame(){
		UserDataManager.GetInstance ().UpdateAdCount (UserDataManager.GetInstance().GetAdCount());
		if (UserDataManager.GetInstance ().GetAdCount() == 6) {
			ShowAd ();
			PlayerPrefs.SetInt ("Ad Count", 0);
		}
		Application.LoadLevel (Application.loadedLevel);
	}

	public void ShowAd()
	{
		if (Advertisement.IsReady())
		{
			Advertisement.Show();
		}
	}
}
