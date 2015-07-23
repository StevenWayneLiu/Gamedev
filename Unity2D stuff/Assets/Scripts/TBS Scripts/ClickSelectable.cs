﻿using UnityEngine;
using System.Collections;

public class ClickSelectable : MonoBehaviour {

    public int character = 0;//int representing the battler that this gameobject corresponds to

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseDown()
    {
        //selects the character associated with this gameobject when it is clicked on
        TBBattleSystem.battleManager.target = (CharacterBaseClass)TBBattleSystem.battleManager.enemies[character];
    }
}
