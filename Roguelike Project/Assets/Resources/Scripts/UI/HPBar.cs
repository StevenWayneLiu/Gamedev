using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HPBar : MonoBehaviour {

    Image hpBar;
    public Character character;

	// Use this for initialization
	void Start () {
        hpBar = gameObject.GetComponentInChildren<Image>();
	}
	
	// Update is called once per frame
	void Update () {
        if(hpBar != null)
            hpBar.fillAmount = character.RemHealth;
	}
}
