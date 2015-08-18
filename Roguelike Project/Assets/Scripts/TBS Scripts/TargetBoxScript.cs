using UnityEngine;
using System.Collections;

public class TargetBoxScript : MonoBehaviour {

    Collider col;

	// Use this for initialization
	void Start () {
        col = gameObject.GetComponent<Collider>();
	}
	
	// Update is called once per frame
	void Update () {
        if (GameStateManager.stateManager.state == GameStateManager.GameStates.PlayerAct)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit);//cast a ray where mouse clicked
            gameObject.transform.LookAt(hit.point);//rotate around hitbox to center on area
        }
	}

    void OnTriggerStay(Collider col)
    {
        if (GameStateManager.stateManager.state == GameStateManager.GameStates.PlayerAct)
        {
            if (Input.GetButton("Fire1") && GameStateManager.stateManager.actionIndex == 2)//if clicking to confirm an attack
            {

                col.gameObject.GetComponentInParent<CharacterManager>().charInfo.CurHealth -= 10;


            }
        }
    }
}
