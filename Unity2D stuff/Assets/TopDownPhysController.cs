using UnityEngine;
using System.Collections;

public class TopDownPhysController : MonoBehaviour {
    public float maxSpeed = 5f;
    public float direction;
    public float yVel;
    public float xVel;
    private bool fire = false;
    public GameObject bulletPrefab;
    // Use this for initialization
    void Start()
    {
        xVel = 0f;
        yVel = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        fire = Input.GetButtonDown("Fire1");
        xVel = Mathf.Clamp(Input.GetAxis("Horizontal") * maxSpeed,-1 * Mathf.Sqrt(maxSpeed), Mathf.Sqrt(maxSpeed));
        yVel = Mathf.Clamp(Input.GetAxis("Vertical") * maxSpeed, -1 * Mathf.Sqrt(maxSpeed), Mathf.Sqrt(maxSpeed));

        if (fire)
            GameObject.Instantiate(bulletPrefab, gameObject.transform.position + new Vector3(1f, 0f, 0f), gameObject.transform.rotation);
    }
    void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(xVel, yVel);
    }
}
