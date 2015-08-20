using UnityEngine;
using System.Collections;

public class PlayerChooseState : State {

    bool hasMoved;
    bool hasActed;

    public PlayerChooseState(StateManager sm) : base(sm)
    {
        name = "PlayerChoose";
    }

    public override void Update()
    {
        base.Update();
        //listen for input from buttons
        
        //listen for input from escape key
        if (Input.GetButtonDown("Cancel"))//bring up the menu
        {

        }
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

    public override void UIButtonPress(int butNum)
    {
        base.UIButtonPress(butNum);
        switch (butNum)
        {
            case (0)://wait button
                stateMan.RemoveCurrentState(new EnemyChooseState(stateMan));
                break;
            case(1)://attack button
                stateMan.AddCurrentState(new PlayerActState(stateMan));
                break;
            case(2)://move button
                stateMan.AddCurrentState(new PlayerMoveState(stateMan));
                break;
            default:
                break;
        }
    }
}
