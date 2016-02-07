using UnityEngine;
using System.Collections;
using UnityEditor;

namespace CitrusCore.ItemSystem.Editor
{
    public partial class ItemRankDatabaseEditor : EditorWindow
    {
        ItemRankDatabase itemRankDatabase;
        ItemRank curItem;
        Texture2D curTexture;
        int curIndex = -1;

        //ListView
        Vector2 scrollPos;
        
        const int SPRITE_BUTTON_SIZE = 46;
        const string DATABASE_PATH = @"Assets/Resources/Scripts/ItemRankDatabase.asset";

        [MenuItem("CitrusCore/Database/Item Rank Editor")]
        public static void Init()
        {
            ItemRankDatabaseEditor window = EditorWindow.GetWindow<ItemRankDatabaseEditor>();
            window.minSize = new Vector2(400, 300);
            window.titleContent.text = "Item Rank Database";
            window.Show();
        }

        void OnEnable()
        {
            itemRankDatabase = AssetDatabase.LoadAssetAtPath(DATABASE_PATH, typeof(ItemRankDatabase)) as ItemRankDatabase;
            if (itemRankDatabase == null)
            {
                if (!AssetDatabase.IsValidFolder("Assets/Resources/Scripts"))
                {
                    AssetDatabase.CreateFolder("Assets/Resources", "Scripts");
                }
                itemRankDatabase = ScriptableObject.CreateInstance<ItemRankDatabase>();
                AssetDatabase.CreateAsset(itemRankDatabase, DATABASE_PATH);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }

            curItem = new ItemRank();
        }

        void OnGUI()
        {
            GUILayout.Label("custom label");
            ListView();
            //AddQuality();

            GUILayout.BeginHorizontal("Box", GUILayout.ExpandWidth(true));
            BottomBar();
            GUILayout.EndHorizontal();
        }

        void BottomBar()
        {
            //count
            GUILayout.Label("Entries: " + itemRankDatabase.Count);
            //add button
            if (GUILayout.Button("Add"))
            {
                itemRankDatabase.Add(new ItemRank());
            }
        }

        void AddQuality()
        {
            //name
            curItem.Name = EditorGUILayout.TextField("Name: ", curItem.Name);
            //sprite
            if (curItem.Icon)
                curTexture = curItem.Icon.texture;
            else
                curTexture = null;
            //select sprite button
            if(GUILayout.Button(curTexture, GUILayout.Width(SPRITE_BUTTON_SIZE), GUILayout.Height(SPRITE_BUTTON_SIZE)))
            {
                int controllerID = EditorGUIUtility.GetControlID(FocusType.Passive);
                EditorGUIUtility.ShowObjectPicker<Sprite>(null, true, null, controllerID);
            }
            //update sprite button image
            string commandName = Event.current.commandName;
            if(commandName == "ObjectSelectorUpdated")
            {
                curItem.Icon = EditorGUIUtility.GetObjectPickerObject() as Sprite;
                Repaint();
            }
            //GUI.SetNextControlName("Save");   //names the following control "Save", a button's text is not its control name
            if(GUILayout.Button("Save"))
            {
                
                if (curItem == null || curItem.Name == "")
                    return;
                itemRankDatabase.Add(curItem);

                curItem = new ItemRank();
                GUI.FocusControl("Save");

            }
        }

    }
}