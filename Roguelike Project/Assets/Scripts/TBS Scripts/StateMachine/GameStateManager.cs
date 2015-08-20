using UnityEngine;
using System.Collections;

public class GameStateManager : StateManager
{
    public static GameStateManager stateManager;
    //variables
    public int actionIndex = 0;//the index of the action currently selected

    public ArrayList aggro = new ArrayList();//holds all the enemies that the player is currently aggroing

    void Start()
    {
        stateManager = this;
        //add initial state to stateMachine
        AddCurrentState(new PeaceState(this));
        //set reference to UI canvas
        UI = GetComponentInChildren<Canvas>();
        Debug.Log("Setup complete");
    }

    


    public void Lose()
    {
        Application.LoadLevel(3);//go to death screen
    }


}