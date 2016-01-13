using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

namespace CitrusCore.ItemSystem
{
    public class ItemRankDatabase : ScriptableObject
    {
        [SerializeField]List<ItemRank> ranks = new List<ItemRank>();

        public void Add(ItemRank ir)
        {
            ranks.Add(ir);
            EditorUtility.SetDirty(this);
        }
        public void Insert(int index, ItemRank ir)
        {
            ranks.Insert(index, ir);
            EditorUtility.SetDirty(this);
        }
        public void Remove(int index)
        {
            ranks.RemoveAt(index);
            EditorUtility.SetDirty(this);
        }
        public void Remove(ItemRank ir)
        {
            ranks.Remove(ir);
            EditorUtility.SetDirty(this);
        }
        public int Count
        {
            get { return ranks.Count; }
        }
        //square bracket access to entities list
        public ItemRank this[int index]
        {
            get { return ranks[index]; }
            set { ranks[index] = value; EditorUtility.SetDirty(this); }
        }
    }
}
