using UnityEngine;
using UnityEditor;
using System.Collections;

namespace CitrusCore.ItemSystem.Editor
{
    public partial class ItemRankDatabaseEditor 
    {
        
        //list database contents
        void ListView()
        {
            
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.ExpandHeight(true));
            DisplayContents();
            EditorGUILayout.EndScrollView();
        }

        void DisplayContents()
        {
            for (int i = 0; i < itemRankDatabase.Count; i++)
            {
                GUILayout.BeginHorizontal("Box");
                
                //icon button
                if (itemRankDatabase[i].Icon != null)
                    curTexture = itemRankDatabase[i].Icon.texture;
                else
                    curTexture = null;
                //select icon image
                if(GUILayout.Button(curTexture, GUILayout.Width(SPRITE_BUTTON_SIZE), GUILayout.Height(SPRITE_BUTTON_SIZE)))
                {
                    int controllerID = EditorGUIUtility.GetControlID(FocusType.Passive);
                    EditorGUIUtility.ShowObjectPicker<Sprite>(null, true, null, controllerID);
                    curIndex = i;
                }
                //update icon button image
                string commandName = Event.current.commandName;
                if (commandName == "ObjectSelectorUpdated")
                {
                    if (curIndex != -1)
                    {
                        itemRankDatabase[curIndex].Icon = EditorGUIUtility.GetObjectPickerObject() as Sprite;
                        //curIndex = -1;
                    }
                    Repaint();
                }

                GUILayout.BeginVertical();//attributes
                //name
                itemRankDatabase[i].Name = GUILayout.TextField(itemRankDatabase[i].Name);
                

                //delete button
                if (GUILayout.Button("X", GUILayout.Width(20)))
                {
                    if (EditorUtility.DisplayDialog("Delete", "Confirm Deletion?", "ok", "cancel"))
                    {
                        itemRankDatabase.Remove(i);
                    }
                }
                GUILayout.EndVertical();
                GUILayout.EndHorizontal();
            }
        }
    }
}
