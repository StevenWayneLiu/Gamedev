using UnityEngine;
using System.Collections;


[System.Serializable]
public class ItemData : IEntity, IItem
{

    [SerializeField]string name;
    [SerializeField]int price;
    [SerializeField]Sprite icon; float weight;
    [SerializeField]float size;
    //[SerializeField]ItemRank itemRank;
    [SerializeField]float maxHealth;
    [SerializeField]float curHealth;
    [SerializeField]float attack;
    [SerializeField]float defense;
    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    float IEntity.MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }

    float IEntity.CurHealth
    {
        get { return curHealth; }
        set { curHealth = value; }
    }

    float IEntity.RemHealth
    {
        get { return curHealth / maxHealth; }
    }

    float IEntity.Attack
    {
        get { return attack; }
        set { attack = value; }
    }

    float IEntity.Defense
    {
        get { return defense; }
        set { defense = value; }
    }

    void IEntity.Death()
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

    int Durability
    {
        get
        {
            throw new System.NotImplementedException();
        }
        set
        {
            throw new System.NotImplementedException();
        }
    }

    int IItem.Price
    {
        get
        {
            throw new System.NotImplementedException();
        }
        set
        {
            throw new System.NotImplementedException();
        }
    }

    Sprite IItem.Icon
    {
        get
        {
            throw new System.NotImplementedException();
        }
        set
        {
            throw new System.NotImplementedException();
        }
    }

    float IItem.Weight
    {
        get
        {
            throw new System.NotImplementedException();
        }
        set
        {
            throw new System.NotImplementedException();
        }
    }

    float IItem.Size
    {
        get
        {
            throw new System.NotImplementedException();
        }
        set
        {
            throw new System.NotImplementedException();
        }
    }

    int IItem.Durability
    {
        get
        {
            throw new System.NotImplementedException();
        }
        set
        {
            throw new System.NotImplementedException();
        }
    }
}
