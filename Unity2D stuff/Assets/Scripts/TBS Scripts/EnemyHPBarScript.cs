using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyHPBarScript : MonoBehaviour {

    public Image hpBar;
    public int characterIndex;//the index of a character in gamedata, from 0 to 3

	// Use this for initialization
	void Start () {
        hpBar = gameObject.GetComponent<Image>();//get the current button's image
	}
	
	// Update is called once per frame
	void Update () {
        hpBar.fillAmount = ( (CharacterBaseClass) TBBattleSystem.battleManager.enemies[characterIndex] ).HealthFract;
	}
}
