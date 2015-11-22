using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour {

    Text scoreboard;

	// Use this for initialization
	void Start () {
        scoreboard = gameObject.GetComponentInChildren<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        scoreboard.text = "Kills: " + GameData.data.Scores["Kills"];
	}
}
