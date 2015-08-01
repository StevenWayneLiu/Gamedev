using UnityEngine;
using System.Collections;

public class RoomScript : MonoBehaviour
{

    public GameObject content;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (content)
        {
            if(content.tag == "Entity")
                content.GetComponent<FieldEnemy>().target = col.gameObject.transform;
        }
    }

    void OnTriggerExit2D()
    {
        //if the player leaves the room, return to room center
        if (content)
        {
            if (content.tag == "Entity")
                content.GetComponent<FieldEnemy>().target = gameObject.transform;
        }
    }
}
