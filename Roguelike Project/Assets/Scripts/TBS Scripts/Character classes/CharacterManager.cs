﻿using UnityEngine;
using System.Collections;

public class CharacterManager : MonoBehaviour {

    public CharacterBaseClass charInfo;//character info associated with this character
    public NavMeshAgent nav;//navmeshagent attached ot this character

	// Use this for initialization
	void Start () {

        if (gameObject.tag == "Player")
        {
            //create character data for this object
            charInfo = new CharacterBaseClass(CharacterBaseClass.Faction.Player, this);
            GameData.data.Characters.Add(charInfo);//add character data to the gamedata
        }
        else
        {
            //if a new enemy object is created, add its data to the gamedata
            charInfo = new CharacterBaseClass(CharacterBaseClass.Faction.Enemy, this);
            GameData.data.Enemies.Add(charInfo);//add character data to the gamedata
        }
        nav = GetComponent<NavMeshAgent>();//find the navmeshagent attached to this character

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
