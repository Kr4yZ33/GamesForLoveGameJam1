
using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    //Actions to initalize settings for level changes
    public event Action<int> OnLevelChange;
    public StateMachine stateMachine => GetComponent<StateMachine>();
    public int Lives = 1;
    public int CurrentLives;
    public float time = 0f;

    
    //Game Menu UI to switch on and off
    [Header("Game UI")]
    [SerializeField]
    public GameObject MainMenu;
    [SerializeField]
    private GameObject PauseMenu;
    [SerializeField]
    private GameObject[] LifeSprites;
    public GameObject GameOverUI;

    public bool Pause {get; private set; } 


    //Level prefabs to set in inspector 
    [Header("Level setup")]
    [SerializeField]
    private Level[] Levels;
    public int currentLevel;
    [SerializeField]
    float gameStartDelay = 2f;

    private void Awake()
    {
       
        if (GameManager.Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        else
            instance = this;

        IntializeStateMachine();
        currentLevel = 0;

        Pause = false;
    }
    private void Start()
    {
        CurrentLives = Lives;
        ChangeLevel(currentLevel);
    }

    public void ChangeLevel(int newLevel)
    {
        AudioManager.Instance.UpdateBGM(newLevel);
        CurrentLevel.gameObject.SetActive(false);
        currentLevel = newLevel;
        CurrentLevel.gameObject.SetActive(true);
        OnLevelChange(CurrentLevelNumber);
    }

    public void Begin()
    {
        StartCoroutine(StartLevel());
    }
    public IEnumerator StartLevel()
    {
        //Put game in play state
        //Logic in PlayState class(Music/Ball start make paddle movement enabled)
        var state = new PlayState(instance);
        yield return new WaitForSeconds(gameStartDelay);
        stateMachine.SwitchToNewState(state);
    }

    public void DecrementLife()
    {
        if (LifeSprites[CurrentLives - 1] != null)
        {
            LifeSprites[CurrentLives - 1].SetActive(false);
        }
        CurrentLives--;
    }
    public void ResetLevel()
    {
        CurrentLevel.ResetLevel();
       
    }

    public void EndLevel()
    {
        AudioManager.Instance.ToggleMusic();
    }
    public void NextLevel()
    {
        stateMachine.SwitchToNewState(new NextLevelState(Instance));
    }
    public void GameOver()
    {
        stateMachine.SwitchToNewState(new LevelLostState(Instance));
    }
    public void PauseGame()
    {
        Pause = !Pause;
        if (Pause)
        {
            //Pause Ball velocity
            AudioManager.Instance.ToggleMusic();
        }
        else
        { 
            //Unpause Ball velocity
            AudioManager.Instance.PlayMusic();
        }
        

    }
  
    public void ResetGame()
    {
        CurrentLives = Lives;
        foreach (GameObject sprite in LifeSprites)
        {
            sprite.SetActive(true);
        }
        ChangeLevel(0);
    }

   
    private void IntializeStateMachine()
    {
        var states = new Dictionary<Type, BaseState>()
        {
            { typeof(MenuState), new MenuState(this)},
            { typeof(PlayState), new PlayState(this)}
        };
        stateMachine.SetState(states);
    }
    
    public Level CurrentLevel
    {
        get
        {
            return Levels[currentLevel % Levels.Length];
        }
    }

    public int LevelIteration
    {
        get
        {
            return currentLevel / Levels.Length;
        }
    }

    public int CurrentLevelNumber
    {
        get
        {
            return currentLevel % Levels.Length;
        }
    }
}
