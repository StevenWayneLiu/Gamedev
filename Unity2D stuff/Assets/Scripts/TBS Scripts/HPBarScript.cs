using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HPBarScript : MonoBehaviour {

    private Image hpBar;

	// Use this for initialization
	void Start () {
        hpBar = gameObject.GetComponent<Image>();//get the current button's image
	}
	
	// Update is called once per frame
	void Update () {
        if(TBBattleSystem.battleManager.target != null)
            hpBar.fillAmount = TBBattleSystem.battleManager.target.HealthFract;
	}
}
