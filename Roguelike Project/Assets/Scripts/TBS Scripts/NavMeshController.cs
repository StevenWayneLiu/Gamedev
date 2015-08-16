using UnityEngine;
using System.Collections;

public class NavMeshController : MonoBehaviour {

    NavMeshAgent agent;

	// Use this for initialization
	void Start () {
        agent = gameObject.GetComponent<NavMeshAgent>();

        if (gameObject.tag == "Player")//if this is the player object, associate it with the player data
        {
            gameObject.GetComponent<BattlerController>().charData = (CharacterBaseClass)GameData.data.Characters[0];
            gameObject.GetComponent<BattlerController>().charData.Battler = gameObject;
        }
	}
	
	// Update is called once per frame
	void Update () {
        //movement during peacetime
        if (Input.GetButton("Fire1") && GameStateManager.stateManager.state == GameStateManager.GameStates.Peace)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit);//cast a ray where mouse clicked
            agent.SetDestination(hit.point);//set destination to point of collision with map
            Debug.Log(hit.point);
        }
	}

    void FixedUpdate()
    {
        
    }

    //moves the character to the point on the screen where the mouse is
    public void Move()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit);//cast a ray where mouse clicked
        agent.SetDestination(hit.point);//set destination to point of collision with map
        Debug.Log(hit.point);
    }
        
}
