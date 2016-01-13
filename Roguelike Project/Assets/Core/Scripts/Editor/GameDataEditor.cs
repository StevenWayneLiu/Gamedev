using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(GameData))]
public class GameDataEditor : Editor {

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        GameData targ = (GameData)target;

        //show number of characters
        EditorGUILayout.LabelField("Characters", targ.Characters.Count.ToString());
        EditorGUILayout.LabelField("Enemies", targ.Enemies.Count.ToString());
        EditorGUILayout.LabelField("Items", targ.AllItems.Count.ToString());
        EditorGUILayout.LabelField("Skills", targ.AllSkills.Count.ToString());
        EditorGUILayout.LabelField("Ground Items", targ.WorldItems.Count.ToString());
    }

}
