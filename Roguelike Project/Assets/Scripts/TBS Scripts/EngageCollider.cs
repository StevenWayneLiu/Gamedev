using UnityEngine;
using System.Collections;

public class EngageCollider : MonoBehaviour {

    Transform target;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider col)
    {
        //start battle phase if run into a player
        if(col.tag == "Player" && GameStateManager.stateManager.state == GameStateManager.GameStates.Peace)
            GameStateManager.stateManager.StartBattle();
    }
}
