using UnityEngine;
using System.Collections;

public class PlayerChooseState : State {

    bool hasMoved;
    bool hasActed;

    public PlayerChooseState(StateManager sm) : base(sm)
    {
        name = "PlayerChoose";
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
        //turn off battle UI
        //stateMan.UI.enabled = false;
    }

    public override void Update()
    {
        base.Update();
        //listen for input from buttons
        
        //listen for input from escape key
        if (Input.GetButtonDown("Cancel"))//bring up the pause menu
        {
            
        }
    }

    public override void UIButtonPress(int butNum)
    {
        base.UIButtonPress(butNum);
        switch (butNum)
        {
            case (0)://wait button
                stateMan.ChangeState(new EnemyChooseState(stateMan));
                break;
            case(1)://attack button
                stateMan.ChangeState(new PlayerActState(stateMan));
                break;
            case(2)://move button
                stateMan.ChangeState(new PlayerMoveState(stateMan));
                break;
            default:
                break;
        }
    }
}
