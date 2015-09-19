using UnityEngine;
using System.Collections;

[RequireComponent (typeof (CharSkills))]
public class CharacterManager : MonoBehaviour {

    public CharacterBaseClass charInfo;//character info associated with this character
    public NavMeshAgent agent;//navmeshagent attached to this character

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
        agent = GetComponent<NavMeshAgent>();//find the navmeshagent attached to this character

	}
	
	// Update is called once per frame
	void Update () {
        if (GameStateManager.stateManager.currentState.name != "Peace")
        {
            agent.SetDestination(agent.gameObject.transform.position);
            agent.velocity = Vector3.zero;
        }
	}

}
