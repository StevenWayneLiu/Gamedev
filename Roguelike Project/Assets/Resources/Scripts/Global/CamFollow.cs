using UnityEngine;
using System.Collections;

public class CamFollow : MonoBehaviour {

    public Transform targ;
    Camera cam;
    //pixels on screen = scaleFactor * pixels on camera
    public float scaleFactor = 2f;
    //speed that camera accelerates
    public float smoothTime = 1f;
    private Vector3 vel = Vector3.zero;

	// Use this for initialization
	void Start () {
        cam = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        //half-size in unity units = half of screen height / (pixels per unity unit) / scale factor
        cam.orthographicSize = ( (Screen.height/2) / 100f) / scaleFactor;

        if(targ)
        {
            Vector3 newPos = targ.position;
            newPos.z = transform.position.z;
            transform.position = Vector3.SmoothDamp(transform.position, newPos, ref vel, smoothTime);
        }
	}
}
