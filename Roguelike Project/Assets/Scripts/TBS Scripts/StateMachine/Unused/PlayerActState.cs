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

    public override void Enter()
    {
        base.Enter();
        //shapecast in surrounding area to find possible targets
        //add targets to target list
    }

    public override void Exit()
    {
        base.Exit();
        //remove hitbox
    }

    public override void Update()
    {
        base.Update();
        //check for button input
        if (Input.GetButtonDown("Fire1"))//clicking to select a target
        {
            //have target become selected character
            
        }
        else if (Input.GetButtonDown("Submit"))//pressing enter to confirm
        {

            Act();
        }
        else if (Input.GetButtonDown("Cancel"))//go back to player choice screen
        {
            stateMan.ChangeState(stateMan.possibleStates["PlayerChoose"]);//go back to player choice state
        }
    }

    public void Act()
    {
        //set target to current target in battle manager

        //apply attack to current target
        if (target != null)
        {
            character.Attack(target);
            stateMan.ChangeState(stateMan.possibleStates["PlayerChoose"]);//go back to player choice state
        }
        else//else prompt to select valid target
        {
            Debug.Log("select valid target first");
        }
        
    }

    
}
