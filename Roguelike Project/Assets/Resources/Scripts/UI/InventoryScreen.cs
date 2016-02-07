using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class InventoryScreen : MonoBehaviour {

    public GameObject buttonPrefab;
    public ScrollRect scrollRect;
    public List<InventoryButton> buttons;
    public Character owner;

	// Use this for initialization
	void Start () {
        scrollRect = gameObject.GetComponent<ScrollRect>();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void AddButton(ItemData data)
    {
        GameObject newButton = Instantiate(buttonPrefab) as GameObject;
        InventoryButton newItem = newButton.GetComponent<InventoryButton>();
        newItem.itemData = data;
        if(data.Name != "" && data.Name != null)
            newItem.Text.text = data.Name;
        buttons.Add(newItem);
        //hook up button to content panel
        newButton.transform.SetParent(scrollRect.content.transform);
    }
    //clear all buttons
    public void Clear()
    {
        foreach (InventoryButton b in buttons)
        {
            Destroy(b.gameObject);
        }
    }
    //load in entries from a list and make buttons for them
    public void Refresh(Character c, List<ItemData> inv)
    {
        owner = c;
        if(inv.Count == 0)
        {
            Debug.Log("Clear");
            Clear();
            return;
        }
        //remove buttons that have no corresponding entry
        foreach (InventoryButton b in buttons)
        {
            if (!inv.Contains(b.itemData))
            {
                Destroy(b.gameObject);
            }
                
        }
        //add buttons for entries without them
        foreach (ItemData entry in inv)
        {
            if(!HasButton(entry))
            {
                AddButton(entry);
            }
        }
    }
    //returns true if a particular item has a corresponding button
    public bool HasButton(ItemData idata)
    {
        bool hasButton = false;
        foreach (InventoryButton b in buttons)
        {
            if (b.itemData == idata)
                hasButton = true;
        }
        return hasButton;
    }

    public Character Owner
    {
        get { return owner; }
    }
}
