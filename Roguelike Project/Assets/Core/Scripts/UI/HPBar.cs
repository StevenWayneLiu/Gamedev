using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HPBar : MonoBehaviour {

    Image hpBar;
    float hp;

	// Use this for initialization
	void Start () {
        hpBar = gameObject.GetComponentInChildren<Image>();
        hp = gameObject.GetComponent<CharacterManager>().RemHealth;
	}
	
	// Update is called once per frame
	void Update () {
        if(hpBar != null)
            hpBar.fillAmount = hp;
	}
}
