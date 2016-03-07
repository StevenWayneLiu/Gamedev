using UnityEngine;
using System.Collections;

public class SmoothCamera : MonoBehaviour {

    float smoothTime = 0.2f;
    public Vector3 moveDest;

	// Use this for initialization
	void Awake () {
        moveDest = transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	    //use smoothdamp
        if (transform.position != moveDest)
        {
            SmoothMove(moveDest);
        }
	}

    public void SmoothMove(Vector3 newPos){
        Vector3 curVelocity = new Vector3();
        newPos.y = transform.position.y;//preserve z axis position
        transform.position = Vector3.SmoothDamp(transform.position, newPos, ref curVelocity, smoothTime);
    }
}
