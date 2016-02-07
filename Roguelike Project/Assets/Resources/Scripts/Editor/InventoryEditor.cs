using UnityEngine;
using System.Collections;
using UnityEditor;


public class InventoryEditor : EditorWindow {

    Inventory inventory;
    ItemData curItem;
    Texture2D curTexture;
    int curIndex = -1;

    //ListView
    Vector2 scrollPos;

    const int SPRITE_BUTTON_SIZE = 46;
    const string DATABASE_PATH = @"Assets/Resources/Scripts/Database/Inventory.asset";
    [MenuItem("CitrusCore/Database/Item Editor")]
    public static void Init()
    {
        InventoryEditor window = EditorWindow.GetWindow<InventoryEditor>();
        window.minSize = new Vector2(400, 300);
        window.titleContent.text = "Item Database";
        window.Show();
    }
    
    void OnEnable()
    {
        inventory = AssetDatabase.LoadAssetAtPath(DATABASE_PATH, typeof(Inventory)) as Inventory;
        if (inventory == null)
        {
            if (!AssetDatabase.IsValidFolder("Assets/Resources/Scripts/Database"))
            {
                AssetDatabase.CreateFolder("Assets/Resources", "Scripts/Database");
            }
            inventory = ScriptableObject.CreateInstance<Inventory>();
            AssetDatabase.CreateAsset(inventory, DATABASE_PATH);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        curItem = new ItemData();
    }

    void OnGUI()
    {
        GUILayout.Label("custom label");
        //ListView();
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.ExpandHeight(true));
        for (int i = 0; i < inventory.Count; i++)
        {
            GUILayout.BeginHorizontal("Box");

            IconButton(i);

            GUILayout.BeginVertical();//attributes
            //name
            inventory[i].Name = GUILayout.TextField(inventory[i].Name);


            //delete button
            if (GUILayout.Button("X", GUILayout.Width(20)))
            {
                if (EditorUtility.DisplayDialog("Delete", "Confirm Deletion?", "ok", "cancel"))
                {
                    inventory.Remove(i);
                }
            }
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
        }
        EditorGUILayout.EndScrollView();

        GUILayout.BeginHorizontal("Box", GUILayout.ExpandWidth(true));
        BottomBar();
        GUILayout.EndHorizontal();
    }
    
    //UI Elements
    void BottomBar()
    {
        //count
        GUILayout.Label("Entries: " + inventory.Count);
        //add button
        if (GUILayout.Button("Add"))
        {
            inventory.Add(new ItemData());
        }
    }
    //create icon button
    void IconButton(int i)
    {
        //set icon button texture
        if (inventory[i].Icon != null)
            curTexture = inventory[i].Icon.texture;
        else
            curTexture = null;
        //if clicking button, bring up object picker to select sprite
        if (GUILayout.Button(curTexture, GUILayout.Width(SPRITE_BUTTON_SIZE), GUILayout.Height(SPRITE_BUTTON_SIZE)))
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
                inventory[curIndex].Icon = EditorGUIUtility.GetObjectPickerObject() as Sprite;
            }
            Repaint();
        }
    }

}
