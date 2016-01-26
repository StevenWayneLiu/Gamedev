using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InventoryScreen : MonoBehaviour {

    public GameObject buttonPrefab;
    public ScrollRect scrollRect;

	// Use this for initialization
	void Start () {
        scrollRect = gameObject.GetComponent<ScrollRect>();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void AddButton(string name)
    {
        GameObject newButton = Instantiate(buttonPrefab) as GameObject;
        InventoryButton newItem = newButton.GetComponent<InventoryButton>();
        if(name != "")
            newItem.Text.text = name;

        //hook up button to content panel
        newButton.transform.SetParent(scrollRect.content.transform);
    }
}
