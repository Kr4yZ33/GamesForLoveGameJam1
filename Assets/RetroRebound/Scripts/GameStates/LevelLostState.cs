using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLostState : BaseState
{
    private GameManager instance;
    public LevelLostState(GameManager instance) : base(instance.gameObject)
    {
        this.instance = instance;
    }
    public override void Tick_Enter()
    {
        Debug.Log("Level Lost");
    }

    public override void Tick_Execute()
    {
        //Possible cutscene of some kind put in here then change states
        if (instance.CurrentLives <= 0)
        {
            //If no more lives then reset level move to game over
            instance.ResetGame();
            instance.stateMachine.SwitchToNewState(new GameOverState(instance));
        }
        else
        {
            //If at least 1 life 
            //instance.ResetLevel();
            instance.stateMachine.SwitchToNewState(new MenuState(instance));
        }
        
        
    }

    public override void Tick_Exit()
    {
        Debug.Log("Exiting Level Lost state");
    }


}
