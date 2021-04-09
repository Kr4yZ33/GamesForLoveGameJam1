using UnityEngine;
using System;

public abstract class BaseState 
{
    public BaseState(GameObject gameObject)
    {
        this.gameObject = gameObject;
        
    }
    protected GameObject gameObject;

    public abstract  void Tick_Enter();
    public abstract void Tick_Execute();
    public abstract void Tick_Exit();


}
