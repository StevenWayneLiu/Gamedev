using UnityEngine;
using System.Collections;

public class PlayerActState : State {

    CharacterBaseClass character;
    CharacterBaseClass target;

    public PlayerActState(StateManager sm): base(sm)
    {
        name = "PlayerAct";
        character = GameData.data.Characters[0].manager.charInfo;
    }

    public override void Update()
    {
        base.Update();
        if (Input.GetButtonDown("Fire1"))
        {
            Act();
            
        }
        else if (Input.GetButtonDown("Cancel"))
        {
            stateMan.RemoveCurrentState(stateMan.possibleStates["PlayerChoose"]);//go back to player choice state
        }
    }

    public override void Enter()
    {
        base.Enter();
        //create hitbox to target with
    }

    public override void Exit()
    {
        base.Exit();
        //remove hitbox
    }

    public void Act()
    {
        //find targets intersecting hitbox



        //apply attack
        if (target != null)
        {
            character.Attack(target);
            stateMan.RemoveCurrentState(stateMan.possibleStates["PlayerChoose"]);//go back to player choice state
        }
        else
        {
            Debug.Log("select target first");
        }
        
    }

    
}
