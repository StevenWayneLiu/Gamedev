using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(GameManager))]
public class GameDataEditor : Editor {

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        GameManager targ = (GameManager)target;

        //show number of characters
        EditorGUILayout.LabelField("Characters", targ.characters.Count.ToString());
        EditorGUILayout.LabelField("Enemies", targ.enemies.Count.ToString());
        EditorGUILayout.LabelField("Items", targ.itemDatabase.Count.ToString());
        EditorGUILayout.LabelField("Skills", targ.skillDatabase.Count.ToString());
        EditorGUILayout.LabelField("Ground Items", targ.worldItems.Count.ToString());
    }

}
