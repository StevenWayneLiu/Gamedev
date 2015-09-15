using UnityEngine;
using System.Collections;


//state where all entities are idle and don't move, waits on player input
public class IdleState : State {
    //constructor
    public IdleState(StateManager sm) : base(sm)
    {
        name = "Peace";
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
        
        //listen for menu input from player

        //listen for pause menun input from player
    }
    

}
