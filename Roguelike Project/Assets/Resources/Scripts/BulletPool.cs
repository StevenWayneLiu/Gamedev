using UnityEngine;
using System.Collections;

public class BulletPool : MonoBehaviour {
    public int poolSize = 20;
    public GameObject bullet;//bullet prefab to be fired
    GameObject[] pool;//array for holding enemy bullets

    //cooldown and fire rate
    public float cooldown = 0.1f;//time it takes to be able to fire a bullet again when button is held down
    public float cooldownTimer = 0f;

	// Use this for initialization
	void Start () {
	    //create enemies
        pool = new GameObject[poolSize];
        for(int i = 0; i < poolSize; i++)//fill pool with bullets
        {
            GameObject bull = (GameObject)Instantiate(bullet);
            pool[i] = bull;//store bullet
            pool[i].SetActive(false);//set inactive
        }
	}
	public void Fire(float ang)
    {
        if(cooldownTimer <= 0)//if cooldown is over
        {
            for (int i = 0; i < poolSize - 1; i++)//loop through pool looking for an inactive bullet
            {
                if (!pool[i].activeInHierarchy)
                {
                    //set bullet to location of the pool
                    pool[i].transform.position = transform.position;
                    pool[i].transform.rotation = transform.rotation;
                    pool[i].transform.Rotate(new Vector3(0,0,ang));
                    pool[i].SetActive(true);

                    cooldownTimer = cooldown;//reset cooldown timer

                    break;//break to only activate one
                }
            }
        }
    }
	// Update is called once per frame
	void Update () {
        cooldownTimer -= Time.deltaTime;//subtract elapsed time from cooldown timer to count down
	}
}
