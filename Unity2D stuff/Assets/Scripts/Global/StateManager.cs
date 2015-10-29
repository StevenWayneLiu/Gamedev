/*
 * Context script for State Design Pattern
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StateManager : MonoBehaviour {

    public State currentState;//the currently active state
    public State prevState;//previously active state
    public Dictionary<string, State> possibleStates = new Dictionary<string, State>();//state stack
    public Canvas UI;//UI canvas to refer to for UI controls and stuff
    

    //calls the state's update function
    public void Update()
    {
        if(currentState != null)
            currentState.Update();
        Debug.Log(currentState.name);
    }
    //change active state to new state, set current state to prev state
    public void SwapState(State newState)
    {
        prevState = currentState;//set current state to previous state
        if (possibleStates.ContainsValue(newState))
        {
            currentState = newState;//if new state is in dictionary, make current state
        }
        else//if new state isn't in dictionary
        {
            possibleStates.Add(newState.name, newState);//add state to the list of states
            currentState = newState;//change to new state
            if (currentState != null)
                currentState.Enter();//setup state
        }
    }

    //remove the current state from the List. Switch to new state
    public void ChangeState(State newState)
    {
        if (currentState != null)
            currentState.Exit();//clean up current state
        possibleStates.Remove(currentState.name);//remove current state from the list
        if (possibleStates.ContainsValue(newState))
        {
            currentState = newState;//if new state is in dictionary, make current state
        }
        else//if new state isn't in dictionary
        {
            possibleStates.Add(newState.name, newState);//add state to the list of states
            currentState = newState;//change to new state
            if (currentState != null)
                currentState.Enter();//setup state
        }
    }

    public void UIButtonPress(int butNum)
    {
        currentState.UIButtonPress(butNum);
    }

}
