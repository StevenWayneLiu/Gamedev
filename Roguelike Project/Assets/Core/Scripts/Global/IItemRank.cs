using UnityEngine;
using System.Collections;

namespace CitrusCore.ItemSystem
{
    public interface IItemRank
    {

        string Name { get; set; }
        Sprite Icon { get; set; }

    }
}