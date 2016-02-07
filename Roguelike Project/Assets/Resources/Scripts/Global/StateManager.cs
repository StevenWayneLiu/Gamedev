/*
 * Context script for State Design Pattern
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StateManager : MonoBehaviour {

    public List<State> states = new List<State>();
    public Canvas UI;//UI canvas to refer to for UI controls and stuff
    
    //calls the state's update function
    public void Update()
    {
        if(states.Count > 0)
            states[states.Count-1].Update();
        Debug.Log(states[states.Count-1].name);
    }
    //add new state, don't pop current state
    public void AddState(State newState)
    {
        states.Add(newState);
        newState.Enter();
    }
    public void RemoveState()
    {
        if (states.Count > 0)
        {
            states[states.Count - 1].Exit();//clean up state
            states.RemoveAt(states.Count-1);
        }
    }
    //remove the current state from the List. Add new state to list
    public void ChangeState(State newState)
    {
        RemoveState();
        AddState(newState);
    }

    public void UIButtonPress(int butNum)
    {
        states[states.Count].UIButtonPress(butNum);
    }

}
