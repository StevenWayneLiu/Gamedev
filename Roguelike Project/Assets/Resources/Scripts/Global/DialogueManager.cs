using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour {

    //have a public list that holds all of the flags for a particular map
    //list that holds all the scenes for a particular map
    public List<Dialogue> dialogues;
    public bool progress = false;
    //list that holds the events for a loaded scene
    //Manager states
    enum states { Play, Wait };//Manager is either playing a scene, or waiting for input
    states curState = states.Play;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if(curState == states.Wait)
        {
            //check input, if recieved then switch flags
            if(progress)
            {
                curState = states.Play;
            }
        }
        if(curState == states.Play)
        {
            //load text
            LoadDialogue();
            //display text
            DisplayDialogue();
            //set state to wait
            curState = states.Wait;
        }
        
	}

    //load in a sequence to be ready to play
    public void LoadDialogue()
    {

    }

    public void DisplayDialogue()
    {

        //switch to wait mode
    }



    //move a character to a target location based on dialogue data
    public void ScriptedMove(Character targ)
    {

    }
    //display a speech bubble over a character
    public void SpeechBubble(Character targ)
    {

    }
}
