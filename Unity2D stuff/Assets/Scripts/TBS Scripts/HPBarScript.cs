using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HPBarScript : MonoBehaviour {

    private Image hpBar;
    public int characterIndex;//the index of a character in gamedata, from 0 to 3

	// Use this for initialization
	void Start () {
        hpBar = gameObject.GetComponent<Image>();//get the current button's image
	}
	
	// Update is called once per frame
	void Update () {
        hpBar.fillAmount = GameData.data.Characters[characterIndex].HealthFract;
	}
}
