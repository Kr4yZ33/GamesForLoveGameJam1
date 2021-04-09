using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuState : BaseState
{
    private GameManager instance;
    public MenuState(GameManager instance) : base(instance.gameObject)
    {
        this.instance = instance;
    }
    public override void Tick_Enter()
    {
        //Enable Menu screen 
        instance.MainMenu.SetActive(true);
        instance.ChangeLevel(instance.currentLevel);
        AudioManager.Instance.ToggleMusic();
        Debug.Log("Enter Menu");
    }

    public override void Tick_Execute()
    {
        Debug.Log("Waiting for start");
    }

    public override void Tick_Exit()
    {
        //Disable menu and exit;
        instance.MainMenu.SetActive(false);
    }

    
}
