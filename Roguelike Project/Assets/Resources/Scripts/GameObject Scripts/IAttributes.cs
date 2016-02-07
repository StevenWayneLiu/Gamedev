using UnityEngine;
using System.Collections;


public interface IAttributes {

    string Name { get; set; }
    float MaxHealth { get; set; }
    float CurHealth { get; set; }
    float RemHealth { get; }
    float Attack { get; set; }
    float Defense { get; set; }

}
