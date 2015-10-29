using UnityEngine;
using System.Collections;

//template for character data class in game data
public class Character : Entity {

    public enum Faction { Player, Enemy };//whether the character belongs to player or enemy
    public Faction fac;

    public CharacterManager manager;//used for accessing character in scene

	//default constructor
    public Character(CharacterManager man) : base()
    {
        fac = Faction.Enemy;
        stats = new CharacterStats();
        manager = man;
    }

    public Character(Faction faction, CharacterManager man)
        : base()
    {
        fac = faction;
        stats = new CharacterStats();
        manager = man;
    }

    public CharacterManager Manager
    {
        get { return manager; }
        set { manager = value; }
    }

	// Update is called once per frame
	void Update () {
	
	}
}
