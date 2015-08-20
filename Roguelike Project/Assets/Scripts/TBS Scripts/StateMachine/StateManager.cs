/*
 * Context script for State Design Pattern
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StateManager : MonoBehaviour {

    public State currentState;//the currently active state
    public Dictionary<string, State> possibleStates = new Dictionary<string, State>();//state stack
    public Canvas UI;//UI canvas to refer to for UI controls and stuff
    

    //calls the state's update function
    public void Update()
    {
        if(currentState != null)
            currentState.Update();
        Debug.Log(currentState.name);
    }
    //change active state to an existing state in the list, should only be called by state
    public void ChangeState(string st)
    {
        currentState = possibleStates[st];
    }

    //add new active state and set to active state
    public void AddCurrentState(State newState)
    {
        possibleStates.Add(newState.name, newState);//add state to the list of current states

        if(currentState != null)
            currentState.Enter();//setup state

        ChangeState(newState.name);//change active state to new state
    }
    //remove the current state from the List. Switch current state to newState
    public void RemoveCurrentState(State newState)
    {
        if (currentState != null)
            currentState.Exit();//clean up current state
        possibleStates.Remove(currentState.name);//remove current state from the list
        currentState = newState;
        if(newState != null)
            newState.Enter();
    }

    public void UIButtonPress(int butNum)
    {
        currentState.UIButtonPress(butNum);
    }

}
