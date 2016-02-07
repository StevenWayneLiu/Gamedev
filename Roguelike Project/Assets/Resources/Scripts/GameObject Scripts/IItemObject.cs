using UnityEngine;
using System.Collections;

namespace CitrusCore.ItemSystem
{
    public interface IItemObject
    {
        GameObject Prefab { get; set; }
        
    }
}