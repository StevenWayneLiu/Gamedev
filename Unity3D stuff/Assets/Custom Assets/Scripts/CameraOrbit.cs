using UnityEngine;
using System.Collections;

public class CameraOrbit : MonoBehaviour {

    private Vector3 target;//current position of target transform
    private Vector3 camVel;//velocity of the camera

    private float YRot;//y rotation of camera
    private float XRot;//x rotation of camera

    private float minYRot = -90f;//maximum vertical rotation below horizontal
    private float maxYRot = 90f;//maximum vertical rotation above horizontal


    public Transform Target;//transform of camera target
    private float MouseSens = 10f;//mouse sensitivity multiplier

    private float distance = 10f;//distance of the camera from the target
    private float minDist = 3f;//minimum distance from target
    private float maxDist = 50;//maximum distance from target

    private float zoomSpeed = 5f;//speed at which the player can zoom the camera in or out

    private float camRotY = 0f;//vertical rotation of the camera

    private float smooth = 0.125f;//time it takes for camera to move to new position

	// Use this for initialization
	void Start () {
        
        target = Target.position;

        //camPos set behind the character by the cam offset
        transform.position = target - (Target.forward * distance);
	}

    void LateUpdate()
    {
        //get input from mouse movement
        XRot += Input.GetAxis("Mouse X") * MouseSens;
        YRot -= Input.GetAxis("Mouse Y") * MouseSens;

        YRot = ClampAngle(YRot, minYRot, maxYRot);

        Quaternion rotation = Quaternion.Euler(YRot, XRot, 0);

        distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * zoomSpeed, minDist, maxDist);

        transform.rotation = rotation;
        transform.position = target - (rotation * Vector3.forward * distance);

        RaycastHit hit;
        if( Physics.Linecast( target, transform.position, out hit, 1 << 9 ) )//if the linecast between the target and the camera hits terrain
        {
            transform.position = hit.point;//set the camera position to the point of collision
            distance = Vector3.Distance(target, transform.position);//sets distance to the distance from the raycast hit point to the camera target
        }

        camVel = Target.position - target;//determine change in camera target's position
        transform.position += camVel;
        target = Target.position;//check position of target transform

        //set camera to look at character
        //transform.LookAt(Target);
    }

    //clamp the angle between a min and max value
    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360f)
            angle += 360f;
        if (angle > 360f)
            angle -= 360f;
        return Mathf.Clamp(angle, min, max);
    }
}
