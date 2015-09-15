using UnityEngine;
using System.Collections;

//state in which queued actions are executed and time is advanced
public class ActionState : State {

	public ActionState(StateManager sm) : base(sm)
    {
        name = "Action";
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

        //switch back to idle state if not moving or not pressing execution button
        if((Input.GetAxisRaw("Horizontal") == 0) && (Input.GetAxisRaw("Vertical") == 0))
        {
            if(!Input.GetButton("Submit"))
            {
                stateMan.ChangeState(new IdleState(stateMan));
            }
        }
    }
}
