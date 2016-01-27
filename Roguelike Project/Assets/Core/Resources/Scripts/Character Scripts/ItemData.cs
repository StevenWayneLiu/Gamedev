using UnityEngine;
using System.Collections;


[System.Serializable]
public class ItemData 
{
    [SerializeField]string name;
    [SerializeField]int price;
    [SerializeField]Sprite icon; 
    [SerializeField]float weight;
    [SerializeField]float size;
    //[SerializeField]ItemRank itemRank;
    [SerializeField]float maxHealth;
    [SerializeField]float curHealth;
    bool equippable;

    public ItemData()
    {
        name = "";
        price = 0;
        icon = null;
        size = 0;
        maxHealth = 0;
        curHealth = 0;
    }
    public ItemData(ItemData orig)
    {
        name = orig.name;
        price = orig.price;
        icon = orig.icon;
        size = orig.size;
        maxHealth = orig.maxHealth;
        curHealth = orig.curHealth;
    }

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    float MaxDurability
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }

    float CurDurability
    {
        get { return curHealth; }
        set { curHealth = value; }
    }

    float RemDurability
    {
        get { return curHealth / maxHealth; }
    }
    void Death()
    {
        throw new System.NotImplementedException();
    }

    int Price
    {
        get { return price; }
        set { price = value; }
    }

    Sprite Icon
    {
        get { return icon; }
        set { icon = value; }
    }

    float Weight
    {
        get { return weight; }
        set { weight = value; }
    }

    float Size
    {
        get { return size; }
        set { size = value; }
    }
    /*
    ItemRank ItemRank
    {
        get { return itemRank; }
        set { itemRank = value; }
    }
    */

    public bool Equippable
    {
        get { return equippable; }
    }
}
