using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	public float autoNextLevel;

	// Use this for initialization
	void Start () {
		if (autoNextLevel <= 0) {
			Debug.Log ("Level auto load disabled, use a positive number in seconds");
		} else {
			Invoke ("LoadNextLevel", autoNextLevel);
		}
	}
	
	// Update is called once per frame
	public void LoadLevel (string name){
		SceneManager.LoadScene (name);
	}

	public void QuitRequest (){
		Debug.Log ("Quit Requested");
	}

	public void LoadNextLevel(){
		SceneManager.LoadScene ((SceneManager.GetActiveScene ().buildIndex + 1));
	}
}
