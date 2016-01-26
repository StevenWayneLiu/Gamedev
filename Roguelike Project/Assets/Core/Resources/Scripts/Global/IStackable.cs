using UnityEngine;
using System.Collections;

namespace CitrusCore.ItemSystem
{
    public interface IStackable
    {
        int Stack(int amount);
        int MaxStack { get; set; }

    }
}