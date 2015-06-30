using UnityEngine;
using System.Collections;

public class GoalScript : MonoBehaviour {

    public int levelNum = 1;//the number of the level to send the player to

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter(Collider obj)
    {
        if(obj.tag == "Player")
        {
            Application.LoadLevel(levelNum);
        }
    }
}
