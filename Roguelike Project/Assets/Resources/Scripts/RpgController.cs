using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//character controller for player, handles mouse and keyboard input
public class RpgController : MonoBehaviour {

    public Camera cam;
    Character character;

    //bullet buffer stuff
    BulletPool bullets;//bullet buffer array, holds bullets inside

    // Use this for initialization
    void Start()
    {
        bullets = gameObject.GetComponent<BulletPool>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        character = gameObject.GetComponent<Character>();
    }

    
    void FixedUpdate()
    {
        //move character
        character.Move(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        //attack button
        if (Input.GetButton("Fire1"))
        {
            bullets.Fire(MouseDir());
        }

        //interact with npcs
        if (Input.GetButtonDown("Jump"))
        {
            IInteractable targ = character.FindNearest();
            if(targ != null)
                targ.Interact(character);
        }
        if (Input.GetKeyDown("i"))
        {
            character.ToggleInventory();
        }
        if (Input.GetKeyDown("1"))
        {
            character.ToggleInventory();
        }
        if (Input.GetKeyDown("2"))
        {
            character.ToggleInventory();
        }
        if (Input.GetKeyDown("3"))
        {
            character.ToggleInventory();
        }
        if (Input.GetKeyDown("4"))
        {
            character.ToggleInventory();
        }
    }

    float MouseDir()
    {
        Vector2 mousePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        Vector2 curPos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
        Vector2 moveVec = mousePos - curPos;
        float angle = Mathf.Atan2(moveVec.y, moveVec.x) * Mathf.Rad2Deg;
        return angle;
    }

    
}
