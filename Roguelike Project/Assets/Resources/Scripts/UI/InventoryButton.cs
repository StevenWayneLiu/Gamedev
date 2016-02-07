using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InventoryButton : MonoBehaviour {

    Text text;
    Button button;
    InventoryScreen invScr;
    public ItemData itemData;

    // Use this for initialization
    void Awake()
    {
        text = gameObject.GetComponentInChildren<Text>();
        button = GetComponent<Button>();
        invScr = GetComponentInParent<InventoryScreen>();
    }

    public Text Text
    {
        get { return text; }
        set { text = value; }
    }

    

}
