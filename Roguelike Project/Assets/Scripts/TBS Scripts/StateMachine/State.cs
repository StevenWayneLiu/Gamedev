using UnityEngine;
using System.Collections;

public abstract class State {

    public string name;//name of the state
    public StateManager stateMan;

    public State(StateManager sm)
    {
        stateMan = sm;
    }

    public virtual void Update()//call to update this state
    { 

    }
    public virtual void Enter() //call upon entering this state
    { 

    }
    public virtual void Exit() //call upon exiting this state
    { 

    }
}
