using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour {

    public AudioClip[] lvlMusicArray;
    public AudioSource audioSource;

    void Awake(){
        DontDestroyOnLoad(gameObject);
        Debug.Log("Don't destroy on load " + name);
    }

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
	}

    private void OnLevelWasLoaded(int level){
        AudioClip thisLvlMusic = lvlMusicArray[level];
        Debug.Log("This lvls music" + thisLvlMusic);

        if(thisLvlMusic){
            // if there's music attached in array
            audioSource.clip = thisLvlMusic;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    public void SetVolume(float volume){
        audioSource.volume = volume;
    }
}
