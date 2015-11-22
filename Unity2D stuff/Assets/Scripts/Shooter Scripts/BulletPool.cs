using UnityEngine;
using System.Collections;

public class BulletPool : MonoBehaviour {

    public static BulletPool bullets;//static reference to this object

    public int poolSize = 20;
    public GameObject bullet;//bullet prefab to be fired
    public GameObject[] pool;//array for holding enemy bullets

    //cooldown and fire rate
    public float cooldown = 0.1f;//time it takes to be able to fire a bullet again when button is held down
    public float cooldownTimer = 0f;

	// Use this for initialization
	void Start () {
        bullets = this;

        //fill pool with bullets
        pool = new GameObject[poolSize];
        for(int i = 0; i < poolSize; i++)
        {
            GameObject bull = (GameObject)Instantiate(bullet);
            pool[i] = bull;//store bullet
            pool[i].SetActive(false);//set inactive
        }
	}
    //called when a character uses a bullet
	public void Fire(Transform user)
    {
        if (cooldownTimer <= 0)//if cooldown is over
        {
            for (int i = 0; i < poolSize - 1; i++)//loop through pool looking for an inactive bullet
            {
                if (!pool[i].activeInHierarchy)
                {
                    pool[i].tag = user.gameObject.tag;
                    //set bullet to location of the pool
                    pool[i].transform.position = user.position;
                    pool[i].transform.rotation = user.rotation;
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
