// Created by team AdaptEvolve for the 
// GamesForLove Game Jam 1 April 2020
// Primary contributers: 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance { get { return instance; }  }
    private AudioSource source;
  
    [SerializeField]
    private AudioClip[] bgm = new AudioClip[5];
    // Start is called before the first frame update
    private void Awake()
    {
        //create instance for other sccripts to access
        if (AudioManager.instance != null)
        { 
            Destroy(gameObject);
            return;
        }
        else
            instance = this;

        source = GetComponent<AudioSource>();
    }
    void Start()
    {
        //Intialize bgm and play
        if (GameManager.Instance != null)
        { 
            UpdateBGM(GameManager.Instance.currentLevel);
            GameManager.Instance.OnLevelChange += UpdateBGM;
            Debug.Log("Music function registered to event");
        }
    }
        

    public void PlayMusic()
    {
        source.Play();
    }
    public void ToggleMusic()
    {
        if (source.isPlaying)
            source.Pause();
        else
            source.UnPause();
    }
    public void UpdateBGM(int currentLevel)
    {
        source.clip = bgm[GameManager.Instance.currentLevel];
    }
}
