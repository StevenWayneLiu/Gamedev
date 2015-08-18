using UnityEngine;
using System.Collections;

public class CharacterManager : MonoBehaviour {

    public CharacterBaseClass charInfo;//character info associated with this character
    public NavMeshAgent nav;//navmeshagent attached ot this character

	// Use this for initialization
	void Start () {
        if (gameObject.tag == "Player")
        {
            GameData.data.Characters.Add( new CharacterBaseClass(CharacterBaseClass.Faction.Player,gameObject.GetComponent<CharacterManager>()) );
        }
        else
        {
            GameData.data.Characters.Add( new CharacterBaseClass(CharacterBaseClass.Faction.Enemy, gameObject.GetComponent<CharacterManager>()) );
        }

        nav = GetComponent<NavMeshAgent>();

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
