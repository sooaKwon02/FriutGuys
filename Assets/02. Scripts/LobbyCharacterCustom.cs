using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyCharacterCustom : MonoBehaviour
{
    public string nickName;
    public PlayerItem body;

    public PlayerItem glove1;

    public PlayerItem glove2;

    public PlayerItem head;

    public PlayerItem tail;

    public PlayerItem item1;
    public PlayerItem item2;

    SaveLoad.PLAYER p;

    private void Awake()
    {
        if (FindObjectOfType<SaveLoad>())
        {
            p = FindObjectOfType<SaveLoad>().player;
            nickName = FindObjectOfType<SaveLoad>().nickName;
        }
    }
    private void Start()
    {
        StartCoroutine(CustomPlayer());
    }
    IEnumerator CustomPlayer()
    {
        body.ItemSet(Resources.Load<Item>("Item/FashionItem/" + p.body_name));
        glove1.ItemSet(Resources.Load<Item>("Item/FashionItem/" + p.glove1_name));
        glove2.ItemSet(Resources.Load<Item>("Item/FashionItem/" + p.glove2_name));
        head.ItemSet(Resources.Load<Item>("Item/FashionItem/" + p.head_name));
        tail.ItemSet(Resources.Load<Item>("Item/FashionItem/" + p.tail_name));
        item1.ItemSet(Resources.Load<Item>("Item/UseItem/" + p.item1));
        item2.ItemSet(Resources.Load<Item>("Item/UseItem/" + p.item2));
        body.transform.localScale = new Vector3(p.body_x, p.body_y, p.body_z);
        glove1.transform.localScale = new Vector3(p.glove1_x, p.glove1_y, p.glove1_z);
        glove2.transform.localScale = new Vector3(p.glove2_x, p.glove2_y, p.glove2_z);
        head.transform.localScale = new Vector3(p.head_x, p.head_y, p.head_z);
        tail.transform.localScale = new Vector3(p.tail_x, p.tail_y, p.tail_z);
        body.transform.localRotation =  Quaternion.Euler(p.body_rotX, p.body_rotY, p.body_rotZ);
        glove1.transform.localRotation = Quaternion.Euler(p.glove1_rotX, p.glove1_rotY, p.glove1_rotZ);
        glove2.transform.localRotation = Quaternion.Euler(p.glove2_rotX, p.glove2_rotY, p.glove2_rotZ);
        head.transform.localRotation = Quaternion.Euler(p.head_rotX, p.head_rotY, p.head_rotZ);
        tail.transform.localRotation = Quaternion.Euler(p.tail_rotX, p.tail_rotY, p.tail_rotZ);

       yield return null;
    }
}
