using UnityEngine;
using System.Collections;

//controls character game object and interfaces with character in game data
[RequireComponent (typeof (CharSkills))]
public class CharacterManager : MonoBehaviour {

    public Entity charInfo;//character info associated with this character

    void Awake()
    {
        if (gameObject.tag == "Player")//if this is a player object
        {
            //create character data for this object
            charInfo = new Character(Character.Faction.Player, this);
            GameData.data.Characters.Add(charInfo);//add character data to the gamedata
            //add a character controller to this game object if there's not one already
            if(gameObject.GetComponent<RPGCharController>() == null)
                gameObject.AddComponent<RPGCharController>();
        }
        else
        {
            //create new enemy data for this object
            charInfo = new Character(Character.Faction.Enemy, this);
            GameData.data.Enemies.Add(charInfo);//add character data to the gamedata
        }

    }

	// Use this for initialization
	void Start () {

        
	}
	
	// Update is called once per frame
	void Update () {


	}

}
