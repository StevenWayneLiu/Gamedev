using UnityEngine;
using System.Collections;

//set up stuff in the level
public class LevelSetup : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameData.data.Scores.Add("Kills", 0f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
