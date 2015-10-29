using UnityEngine;
using System.Collections;

//controls character game object and interfaces with character in game data
//base class only has a reference to stats in gamedata, with no functionality
public class CharacterManager : MonoBehaviour {

    public Character charInfo;//character info associated with this character

    public void Start()
    {
        if (gameObject.tag == "Player")//if this is a player object
        {
            //create character data for this object
            charInfo = new Character(Character.Faction.Player, this);
            GameData.data.Characters.Add(charInfo);//add character data to the gamedata
            //add a character controller to this game object if there's not one already
            if (gameObject.GetComponent<FlyerController2D>() == null)
                gameObject.AddComponent<FlyerController2D>();
        }
        else
        {
            //create new enemy data for this object
            charInfo = new Character(Character.Faction.Enemy, this);
            GameData.data.Enemies.Add(charInfo);//add character data to the gamedata
            //add AI script
        }

    }

	
	// Update is called once per frame
	void Update () {


	}

}
