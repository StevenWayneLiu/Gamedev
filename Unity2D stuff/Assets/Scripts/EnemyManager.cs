using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {
    GameObject[] enemyList;
    GameObject enemy;
    public Sprite sprite;
    public int capacity = 10;//number of enemies allowed in the scene at a time
    private Vector3 spawnPoint;

    private float startInterval = 3f;//number of seconds at the start of the game before the first wave spawns

    private float spawnInterval = 2f;//the interval of time between non-simultaneous enemy spawns
    private float waveInterval = 10f;//interval between spawning waves starting from the end of the previous wave
    private float waveTimer;//timer that keeps track of how long it's been since the last wave spawn

    private bool isSpawning = false;//determines when 

	// Use this for initialization
	void Start () {
        enemyList = new GameObject[capacity];
        spawnPoint = new Vector3(10f,0f,0f);
        waveTimer = startInterval;//pause and then start spawning wave
	}
    //spawn single enemy at a specified point
	void Spawn(GameObject enemy, Vector3 pos)
    {
        enemyList[0] = (GameObject)GameObject.Instantiate(enemy, pos, gameObject.transform.rotation);
    }
    //spawn a group of enemies
    void spawnWave()
    {
        enemy = Resources.Load("Enemy") as GameObject;//pull an enemy prefab from resources
        enemy.GetComponent<SpriteRenderer>().sprite = sprite;//set the sprite to something, will be fixed later.
        Vector3 spawnPoint = gameObject.transform.position;

        Spawn(enemy, spawnPoint);//spawn the enemy
        spawnPoint.y += .3f;
        Spawn(enemy, spawnPoint);//spawn the enemy
        spawnPoint.y += .3f;
        Spawn(enemy, spawnPoint);//spawn the enemy

        waveTimer = waveInterval;//reset the timer to count down again after a wave
    }
	// Update is called once per frame
	void Update () {
        waveTimer -= Time.deltaTime;//tick down wave timer if timer isn't spent
        if (waveTimer <= 0)
            spawnWave();//spawn a wave every so often
	}
}
