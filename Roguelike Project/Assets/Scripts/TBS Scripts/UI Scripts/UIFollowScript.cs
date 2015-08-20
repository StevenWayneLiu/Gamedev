using UnityEngine;
using System.Collections;

public class UIFollowScript : MonoBehaviour {

    GameObject target;
    public Vector3 offSet;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = target.transform.position + offSet;
	}
}
