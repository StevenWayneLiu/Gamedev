using UnityEngine;
using System.Collections;

public class ObjectPool : MonoBehaviour {
    public int poolSize = 20;
    public GameObject prefab;//prefabs to be spawned
    public GameObject[] pool = null;//array for holding pooled objects

    //cooldown and fire rate
    public float cooldown = 0.1f;//delay in spawning object
    public float cooldownTimer = 0f;

	// Use this for initialization
	void Start () {
	    //create pool from prefabs if no list of prefabs are given
        if(pool == null)
        {
            pool = new GameObject[poolSize];
            for (int i = 0; i < poolSize; i++)
            {
                GameObject pref = (GameObject)Instantiate(prefab);
                pool[i] = pref;
                pool[i].SetActive(false);//set inactive
            }
        }
	}
	public void Spawn(float ang)
    {
        if(cooldownTimer <= 0)//if cooldown is over
        {
            for (int i = 0; i < poolSize - 1; i++)//loop through pool looking for an inactive bullet
            {
                if (!pool[i].activeInHierarchy)
                {
                    //set bullet to location of the pool user
                    pool[i].transform.position = transform.position;
                    pool[i].transform.rotation = transform.rotation;
                    pool[i].transform.Rotate(new Vector3(0,0,ang));

                    pool[i].GetComponent<SkillObject>().skill = new Skill();//assign temp skill

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
