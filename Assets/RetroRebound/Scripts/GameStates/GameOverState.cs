using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverState : BaseState
{
    private GameManager instance;
    public GameOverState(GameManager instance): base(instance.gameObject)
    {
        this.instance = instance;
    }
    public override void Tick_Enter()
    {
        Debug.Log("Game Over");
        instance.GameOverUI.SetActive(true);
    }

    public override void Tick_Execute()
    {
        /*
        //Possible cutscene of some kind put in here then change states
        if (instance.CurrentLives <= 0)
        {
            //If no more lives then reset level to very first
            instance.ResetGame();
        }
        else
        {
            //If at leave 1 life then reset current level 
              instance.ResetLevel();
            
        }
        */
        Debug.Log("Waiting for input");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            instance.stateMachine.SwitchToNewState(new MenuState(instance));        
        }
    }

    public override void Tick_Exit()
    {
        instance.GameOverUI.SetActive(false);
        Debug.Log("Exiting Game over state");
    }

   
}
