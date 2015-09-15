using UnityEngine;
using System.Collections;


//state where all entities are idle and don't move, waits on player input
public class IdleState : State {
    //constructor
    public IdleState(StateManager sm) : base(sm)
    {
        name = "Idle";
    }

    public override void Enter()
    {
        base.Enter();
        //turn on battle UI
        stateMan.UI.enabled = true;
    }

    public override void Exit()
    {
        base.Exit();
        //remove hitbox
    }

    public override void Update()
    {
        base.Update();
        //listen for movement input from player
        if ((Input.GetAxisRaw("Horizontal") != 0) || (Input.GetAxisRaw("Vertical") != 0))
            stateMan.ChangeState(new ActionState(stateMan));//if any movement is done, move to action mode
        else if(Input.GetButton("Submit"))
            stateMan.ChangeState(new ActionState(stateMan));//if player presses execute button, go to action state
        //listen for menu input from player

        //listen for pause menun input from player
    }
    

}
