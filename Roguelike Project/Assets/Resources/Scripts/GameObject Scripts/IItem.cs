using UnityEngine;
using System.Collections;


public interface IItem
{

    //money value
    int Price { get; set; }
    //icon
    Sprite Icon { get; set; }
    //weight
    float Weight { get; set; }
    //size
    float Size { get; set; }
    //quality rank
    //ItemRank ItemRank { get; set; }
    //prefab
    //equip
    //durability
    int Durability { get; set; }
}
