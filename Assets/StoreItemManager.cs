using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class StoreItemManager : MonoBehaviour
{
    public Item[] items;
    public Item.MoneyType[] itemMoneys;

    //public void SaveStoreInfo()
    //{
    //    StartCoroutine(SaveStore());
    //}
    //IEnumerator SaveStore()
    //{
    //    //string url = "http://61.99.10.173/fruitsGuys/StoreItemSave.php";
    //    string url = "http://192.168.35.229/fruitsGuys/StoreItemSave.php";
    //    WWWForm form = new WWWForm();
    //    for (int i = 0; i < 16; i++)
    //    {
    //        form.AddField("no", i.ToString());
    //        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
    //        {
    //            yield return www.SendWebRequest();
    //        }
    //    }

    //}
    //public void LoadStoreInfo()
    //{
    //    StartCoroutine(LoadStore());
    //}
    //IEnumerator LoadStore()
    //{
    //    //string url = "http://61.99.10.173/fruitsGuys/StoreItemLoad.php";
    //    string url = "http://192.168.35.229/fruitsGuys/StoreItemLoad.php";
    //    WWWForm form = new WWWForm();
    //    using (UnityWebRequest www = UnityWebRequest.Post(url, form))
    //    {
    //        yield return www.SendWebRequest();
    //    }
    //}
}
