using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour {

    //have a list of boolean flags
    //have a public list that holds all of the flags for a particular map
    //list that holds all the scenes for a particular map
    //bool for pausing scene, default true
    //list that holds the events for a loaded scene

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //check time against events in list, starting from the first one.

        //if timer not paused, iterate timer

        //while timer >= events[0].timestamp and events is not empty
            //activate event
            //pop event from stack
        //if events is empty, play end of sequence and wrap up.
	}

    //load in a sequence to be ready to play
    public void LoadSequence()
    {

    }

    public void PlaySequence(int flag)
    {
        //look up scene that corresponds to the flag in the list
        //start timer
        //display dialogue on the screen
    }

}
