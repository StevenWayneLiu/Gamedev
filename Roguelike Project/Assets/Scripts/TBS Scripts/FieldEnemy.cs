using UnityEngine;
using System.Collections;

public class FieldEnemy : MonoBehaviour {

    public Transform target;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        Application.LoadLevel(2);
    }
}
