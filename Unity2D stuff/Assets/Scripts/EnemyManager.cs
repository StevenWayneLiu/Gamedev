using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {
    GameObject[] enemyList;
    public int capacity = 10;//number of enemies allowed in the scene at a time
    private Vector3 spawnPoint;

    private float startInterval = 3f;//number of seconds at the start of the game before the first wave spawns

    private float spawnInterval = 2f;//the interval of time between non-simultaneous enemy spawns
    private float waveInterval = 10f;//interval between spawning waves starting from the end of the previous wave
    private float waveTimer;//timer that keeps track of how long it's been since the last wave spawn
	// Use this for initialization
	void Start () {
        enemyList = new GameObject[capacity];
        spawnPoint = new Vector3(10f,0f,0f);
        waveTimer = startInterval;//pause and then start spawning wave
	}
    //spawn single enemy
	void Spawn(GameObject enemy)
    {
        spawnPoint = new Vector3(10f,0f,0f);//placeholder code
        enemyList[0] = (GameObject)GameObject.Instantiate(enemy, spawnPoint, gameObject.transform.rotation);
    }
    //spawn a group of enemies
    void spawnWave()
    {
        waveTimer = waveInterval;//reset the timer to count down again after a wave
    }
	// Update is called once per frame
	void Update () {
        waveTimer -= Time.deltaTime;//tick down wave timer
        if (waveTimer <= 0)//if wave timer has counted down to zero or less
            spawnWave();//spawn a wave
	}
}
