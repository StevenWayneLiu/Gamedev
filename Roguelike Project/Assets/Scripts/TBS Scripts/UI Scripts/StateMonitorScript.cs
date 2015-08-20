using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StateMonitorScript : MonoBehaviour {

    Text stateText;

	// Use this for initialization
	void Start () {
        stateText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        stateText.text = GameStateManager.stateManager.currentState.name;
	}
}
