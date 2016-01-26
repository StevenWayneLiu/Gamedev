using UnityEngine;
using System.Collections;
using UnityEditor;

public class GlobalItemListEditor : EditorWindow {

    Inventory<ItemData> inv;

    [MenuItem("CitrusCore/GameData/GlobalItemList")]
    public static void Init()
    {
        GlobalItemListEditor window = EditorWindow.GetWindow<GlobalItemListEditor>();
        window.minSize = new Vector2(400, 300);
        window.titleContent.text = "Global Item List";
        window.Show();
    }

    void OnEnable()
    {
    }


}
