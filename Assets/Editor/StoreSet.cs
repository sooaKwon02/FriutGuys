using UnityEditor;
using UnityEngine;

public class StoreSet : EditorWindow
{
    public enum MyEnum { Option1, Option2, Option3 }
    private Object[,] item = new Object[4, 4];
    [MenuItem("StoreSet/StoreSet")]
    public static void StoreSetting()
    {
        StoreSet storeSet = GetWindow<StoreSet>("StoreSet");
        storeSet.Show();
    }
    private void OnGUI()
    {
        if (GUILayout.Button("RadomSet"))
        {

        }
        EditorGUILayout.BeginVertical();
        for (int y = 0; y < 4; y++)
        {
            EditorGUILayout.BeginHorizontal();
            for(int x = 0; x < 4; x++)
            {
                float xSize = position.width / 4.1f;
                float ySize = position.height / 8;

                Rect rect = new Rect(x * xSize, y * ySize, xSize, ySize);

                GUIStyle style=new GUIStyle(GUI.skin.box);
                style.fixedWidth = xSize;
                style.fixedHeight = ySize;  

                EditorGUILayout.BeginVertical(style,GUILayout.Width(xSize),GUILayout.Height(ySize));               
                //intField = EditorGUILayout.IntField("Int Field", intField);
                //floatField = EditorGUILayout.FloatField("Float Field", floatField);
                //textField = EditorGUILayout.TextField("Text Field", textField);
                //toggleField = EditorGUILayout.Toggle("Toggle Field", toggleField);
                //colorField = EditorGUILayout.ColorField("Color Field", colorField);
                //vector2Field = EditorGUILayout.Vector2Field("Vector2 Field", vector2Field);
                //vector3Field = EditorGUILayout.Vector3Field("Vector3 Field", vector3Field);
                //vector4Field = EditorGUILayout.Vector4Field("Vector4 Field", vector4Field);
                //enumField = (MyEnum)EditorGUILayout.EnumPopup("Enum Popup", enumField);
                //popupIndex = EditorGUILayout.Popup("Popup", popupIndex, popupOptions);
                EditorGUILayout.HelpBox("This is a help message.", MessageType.Info);
                item[y, x] = EditorGUILayout.ObjectField(item[y, x], typeof(Object), false);
                item[y, x] = EditorGUILayout.ObjectField(item[y, x], typeof(Object), false);
                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndVertical();

        if (GUILayout.Button("Setting"))
        {

        }
    }
}

    

