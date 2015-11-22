using UnityEngine;
using System.Collections;

//controls character game object and interfaces with character in game data
//base class only has a reference to stats in gamedata, with no functionality
public class CharacterManager : MonoBehaviour {

    public Character charInfo;//character info associated with this character
    public string className;//the name of the character class for a given character

    public void Start()
    {
        if (gameObject.tag == "Player")//if this is a player object
        {
            //create character data for this object
            charInfo = new Character(Character.Faction.Player, this);
            GameData.data.Characters.Add(charInfo);//add character data to the gamedata
        }
        else
        {
            //create new enemy data for this object
            charInfo = new Character(Character.Faction.Enemy, this);
            GameData.data.Enemies.Add(charInfo);//add character data to the gamedata
        }

    }

	
	// Update is called once per frame
	void Update () {


	}

}
