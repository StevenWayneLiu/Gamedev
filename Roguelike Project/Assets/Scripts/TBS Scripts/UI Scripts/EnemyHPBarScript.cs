using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyHPBarScript : MonoBehaviour {

    public Image hpBar;
    public CharacterManager man;
    public CharacterStats stats;

	// Use this for initialization
	void Start () {
        hpBar = gameObject.GetComponentInChildren<Image>();//get the current button's image
        man = gameObject.GetComponentInParent<CharacterManager>();
        stats = man.charInfo.stats;
    }
	
	// Update is called once per frame
	void Update () {
        hpBar.fillAmount = stats.curHealth / stats.maxHealth;
        Debug.Log(stats.curHealth);
	}
}
