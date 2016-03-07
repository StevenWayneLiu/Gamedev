using UnityEngine;
using System.Collections;

public class Tile : ScriptableObject
{
    Vector2 coords;//coordinates on the x-z plane that this tile is in
    Vector2 texture;//texture atlas for tile type
    int height;//how many tiles high this tile is, with 0 being ground level
    bool partial = false;//whether or not the collider for this mesh should take up the entire area of the tile
}
