using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelState : BaseState
{
    private GameManager instance;
    public NextLevelState(GameManager instance) : base(instance.gameObject)
    {
        this.instance = instance;
    }
    public override void Tick_Enter()
    {
        //Increment level and switch prefab to match next level
        instance.currentLevel += 1;
        instance.ChangeLevel(instance.currentLevel);
        instance.stateMachine.SwitchToNewState(new MenuState(instance));
    }

    public override void Tick_Execute()
    {
        //Possible cutscene between levels and then go to next state
    }

    public override void Tick_Exit()
    {
        Debug.Log("Exiting Next Level state");
        
    }

   
}
