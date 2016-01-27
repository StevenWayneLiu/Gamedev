using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Database<>))]
public class InventoryEditor : Editor {

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        DrawDefaultInspector();
    }
}
