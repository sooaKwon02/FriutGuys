using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
public class StoreSet : EditorWindow
{
    public string storeItems = "StorePanel";
    private Object[,] item = new Object[4, 4];
    public GameObject[] obj;
    int[] num;
    Item[] items ;
    Item.MoneyType[] money;
    int count;
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
            RandomSetting();
        }
        if (items != null) 
        {
            EditorGUILayout.BeginVertical();
            for (int y = 0; y < 4; y++)
            {
                EditorGUILayout.BeginHorizontal();
                for (int x = 0; x < 4; x++)
                {
                    float xSize = position.width / 8.4f;
                    float ySize = position.height / 5;

                    Rect rect = new Rect(x * xSize, y * ySize, xSize, ySize);
                    Rect imageRect = new Rect(rect.x + (rect.width - xSize / 4) / 2, // X 중앙 정렬
                                              rect.y + (rect.height - ySize / 4) / 1, // Y 중앙 정렬
                                              xSize / 4, ySize / 4);
                    GUIStyle style = new GUIStyle(GUI.skin.box);
                    style.fixedWidth = xSize;
                    style.fixedHeight = ySize;
                    GUILayout.Label(items[count].sprite.texture, GUILayout.Width(xSize), GUILayout.Height(ySize));
                    EditorGUILayout.BeginVertical(style, GUILayout.Width(xSize), GUILayout.Height(ySize));
                    EditorGUILayout.HelpBox(items[count].itemType.ToString(), MessageType.Info);
                    item[y, x] = EditorGUILayout.ObjectField(item[y, x], typeof(Item), false); 
                    if (GUILayout.Button(items[count].moneyType.ToString()))
                    {
                        if (items[count].moneyType == Item.MoneyType.Cash)
                        {
                            items[count].moneyType = Item.MoneyType.GameMoney;
                        }
                        else
                            items[count].moneyType = Item.MoneyType.Cash;
                    }                    
                    EditorGUILayout.LabelField(items[count].moneyType.ToString());
                   
                    EditorGUILayout.EndVertical();
                    count++;



                }
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndVertical();
            count = 0; 
            GameObject go = GameObject.Find(storeItems);
            money= new Item.MoneyType[16];
            if (GUILayout.Button("Setting"))
            {
                for(int i=0;i<16;i++)
                {
                    money[i] = items[i].moneyType;
                }
                go.GetComponent<StoreItemManager>().items = items;
                go.GetComponent<StoreItemManager>().itemMoneys = money;
                go.GetComponent<StoreItemManager>().StoreSet();
            }
        }    
    }
    void RandomSetting()
    {
        List<Item> _item = new List<Item>();
        _item.AddRange(Resources.LoadAll<Item>("Item/FashionItem"));
        _item.AddRange(Resources.LoadAll<Item>("Item/UseItem"));
        items = _item.ToArray();
        List<int> check = new List<int>();
        for (int i = 0; i < items.Length; i++)
        {
            check.Add(i);
        }
        Shuffle(check);

        
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (count < check.Count)
                {
                    item[i, j] = items[check[count]];
                    count++;
                }
            }
        }
        count = 0;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                items[count] = (Item)item[i,j];
                count++;
            }
        }
        count = 0;
    }
    void Shuffle(List<int> list)
    {
        int n = list.Count; // 리스트의 항목 수를 변수 n에 저장합니다.
        System.Random rng = new System.Random(); // 무작위 수 생성기를 초기화합니다.

        while (n > 1) // n이 1보다 큰 동안 반복합니다.
        {
            n--; // n 값을 1 줄입니다.
            int k = rng.Next(n + 1); // 0부터 n까지의 범위에서 무작위 정수 k를 생성합니다.
            int value = list[k]; // 리스트의 k번째 항목을 저장합니다.
            list[k] = list[n]; // 리스트의 n번째 항목을 k번째 항목으로 이동합니다.
            list[n] = value; // 저장한 k번째 항목을 리스트의 n번째 항목으로 이동합니다.
        }
    }
}

    

