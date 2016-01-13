using UnityEngine;
using System.Collections;

namespace CitrusCore.ItemSystem
{
    [System.Serializable]
    public class ItemRank : IItemRank
    {

        [SerializeField]
        string name;
        [SerializeField]
        Sprite icon;

        public ItemRank()
        {
            name = "";
            icon = new Sprite();
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public Sprite Icon
        {
            get { return icon; }
            set { icon = value; }
        }
    }
}