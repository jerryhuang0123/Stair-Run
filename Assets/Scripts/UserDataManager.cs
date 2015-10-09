using UnityEngine;
using System.Collections;

public class UserDataManager : MonoBehaviour {

	private static UserDataManager mInstance = null;
	//private int AdCount = 0;
	public static UserDataManager GetInstance(){
		return mInstance;
	}

	private void Awake(){


		if (mInstance == null)
			mInstance = this;
	}

	public bool UpdatePlayerHighScore(int score){
		if (GetPlayerHighScore () < score) {
			PlayerPrefs.SetInt ("High Score", score);
			return true;
		}return false;
	}
	public int GetPlayerHighScore(){
		return PlayerPrefs.GetInt ("High Score", 0);
	}

	public int GetAdCount(){
		return PlayerPrefs.GetInt ("Ad Count", 0);
	}

	public void UpdateAdCount(int AdCount){
		AdCount++;
		PlayerPrefs.SetInt ("Ad Count", AdCount);
	}
	

}
