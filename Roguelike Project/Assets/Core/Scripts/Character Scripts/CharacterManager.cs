using UnityEngine;
using System.Collections;

//controls character game object and interfaces with character in game data
//base class only has a reference to stats in gamedata, with no functionality
public class CharacterManager : MonoBehaviour , IEntity {

    Character charInfo;//character info associated with this character
    public string className;//the name of the character class for a given character
    public SkillManager skillMan;

    public void Start()
    {
        if (gameObject.tag == "Player")//if this is a player object
        {
            //create character data for this object
            charInfo = new Character(Character.Faction.Player, this);
            GameData.data.Characters.Add(charInfo);//add character data to the gamedata
        }
        else if(gameObject.tag == "Enemy")
        {
            //create new enemy data for this object
            charInfo = new Character(Character.Faction.Enemy, this);
            GameData.data.Enemies.Add(charInfo);//add character data to the gamedata
        }
        else if (gameObject.tag == "NPC")
        {
            //create new enemy data for this object
            charInfo = new Character(Character.Faction.NPC, this);
            GameData.data.Characters.Add(charInfo);//add character data to the gamedata
            gameObject.AddComponent<NPCInteraction>();
        }
        gameObject.AddComponent<HPBar>();
    }

	
	// Update is called once per frame
	void Update () 
    {
        if(charInfo.stats.curHealth <= 0)
        {
            Death();
        }

	}

    public string Name
    {
        get { return charInfo.Name; }
        set { charInfo.Name = value; }
    }

    public float MaxHealth
    {
        get { return charInfo.MaxHealth; }
        set { charInfo.MaxHealth = value; }
    }

    public float CurHealth
    {
        get { return charInfo.CurHealth; }
        set { charInfo.CurHealth = value; }
    }

    public float RemHealth
    {
        get { return charInfo.RemHealth; }
    }

    public float Attack
    {
        get { return charInfo.Defense; }
        set { charInfo.Attack = value; }
    }

    public float Defense
    {
        get { return charInfo.Defense; }
        set { charInfo.Defense = value; }
    }

    public void Death()
    {
        Destroy(gameObject);
    }

    public Inventory Items
    {
        get { return charInfo.items; }
        set { charInfo.items = value; }
    }
}
