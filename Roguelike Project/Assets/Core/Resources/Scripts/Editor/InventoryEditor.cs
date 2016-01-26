using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Inventory<>))]
public class InventoryEditor : Editor {

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        DrawDefaultInspector();
    }
}
