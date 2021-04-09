using System;
using UnityEngine;

public class PlayState : BaseState
{
    private GameManager instance;
    
        
    public PlayState(GameManager instance) : base(instance.gameObject)
    {
        this.instance = instance;
    }
    
    public override void Tick_Enter()
    {
        //Start Audio for particular level;
        AudioManager.Instance.PlayMusic();
    }

    public override void Tick_Execute()
    {
        instance.time += Time.deltaTime;
        //Pause Game while in this state;
        if (Input.GetKeyDown("e"))
        { 
            instance.PauseGame();
        }

        //Check for Level Change or Game Over conditions 
        if (BallsManager.Instance.BallsInPlay.Count <= 0 )
        {
            instance.DecrementLife();

            instance.GameOver();
        }
        
        if (!instance.CurrentLevel.HasActiveBlocks())
        {
            instance.NextLevel(); 
        }

    }

    public override void Tick_Exit()
    {
        //Clear playfield for next state either game over or to the next level
        Debug.Log("Exiting Play state");
        BallsManager.Instance.ClearBalls();
        PowerupManager.Instance.ClearAllPowerups();
        
    }
}
