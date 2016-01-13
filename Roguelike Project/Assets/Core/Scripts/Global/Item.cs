using UnityEngine;
using System.Collections;


namespace CitrusCore.ItemSystem
{
    public class Item : IEntity, IItem
    {

        [SerializeField]string name;
        [SerializeField]int price;
        [SerializeField]Sprite icon; float weight;
        [SerializeField]float size;
        [SerializeField]ItemRank itemRank;
        [SerializeField]float maxHealth;
        [SerializeField]float curHealth;
        [SerializeField]float attack;
        [SerializeField]float defense;
        string IEntity.Name
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

        int IItem.Price
        {
            get { return price; }
            set { price = value; }
        }

        Sprite IItem.Icon
        {
            get { return icon; }
            set { icon = value; }
        }

        float IItem.Weight
        {
            get { return weight; }
            set { weight = value; }
        }

        float IItem.Size
        {
            get { return size; }
            set { size = value; }
        }

        ItemRank IItem.ItemRank
        {
            get { return itemRank; }
            set { itemRank = value; }
        }
    }
}