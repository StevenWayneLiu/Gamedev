using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BattlerHPBar : MonoBehaviour {

    public Image hpBar;
    public Entity character;

	// Use this for initialization
	void Start () {
        hpBar = GetComponent<Image>();
        character = GetComponentInParent<CharacterManager>().charInfo;
	}
	
	// Update is called once per frame
	void Update () {
        hpBar.fillAmount = character.HealthFract;
	}
}
