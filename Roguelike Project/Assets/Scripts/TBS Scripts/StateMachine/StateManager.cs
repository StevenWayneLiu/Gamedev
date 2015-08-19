/*
 * Context script for State Design Pattern
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StateManager : MonoBehaviour {

    public State currentState;//the currently active state
    List<State> stateStack = new List<State>();//state stack

    //calls the state's update function
    public void Update()
    {
        currentState.Update();
    }
    //change active state to an existing state in the list, should only be called by state
    public void ChangeState(State newState)
    {
        currentState = newState;
    }

    //add new active state and set to active state
    public void AddActiveState(State newState)
    {
        stateStack.Add(newState);//add state to the list of current states
        currentState.Enter();//setup state
        ChangeState(newState);//change active state to new state
    }
    //remove the current state from the List. New current state set to first state in list
    public void RemoveActiveState()
    {
        currentState.Exit();//clean up current state
        stateStack.Remove(currentState);//remove current state from the list
        currentState = stateStack[0];
    }

}
