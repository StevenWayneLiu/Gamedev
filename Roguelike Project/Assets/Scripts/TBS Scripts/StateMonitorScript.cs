using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StateMonitorScript : MonoBehaviour {

    Text stateText;
    GameStateManager.GameStates stateInt;

	// Use this for initialization
	void Start () {
        stateText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        stateInt = GameStateManager.stateManager.state;
        switch (stateInt)
        {
            case(GameStateManager.GameStates.Peace):
                stateText.text = "Peace";
                break;
            case (GameStateManager.GameStates.PlayerTurn):
                stateText.text = "Player's turn";
                break;
            case (GameStateManager.GameStates.PlayerAct):
                stateText.text = "choose action";
                break;
            default:
                break;
        }
	}
}
