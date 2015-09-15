using UnityEngine;
using System.Collections;

public class PlayerMoveState : State {

    NavMeshAgent agent;

    public PlayerMoveState(StateManager sm) : base(sm)
    {
        name = "PlayerMove";
        agent = GameData.data.Characters[0].manager.agent;
    }

    public override void Update()
    {
        base.Update();
        if (Input.GetButtonDown("Fire1"))
        {
            Move();
            stateMan.ChangeState(stateMan.possibleStates["PlayerChoose"]);//go back to player choice state
        }
        else if (Input.GetButtonDown("Cancel"))
        {
            stateMan.ChangeState(stateMan.possibleStates["PlayerChoose"]);//go back to player choice state
        }
    }

    //casts a ray and moves character to point of impact
    void Move()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit);//cast a ray where mouse clicked
        agent.Move(hit.point);
    }

}
