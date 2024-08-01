using UnityEngine;

public class PlayerItem : MonoBehaviour
{
    [HideInInspector]
    public MeshFilter mesh;
    public enum SETTING { body,gloveL,gloveR,head,tail,item1,item2}
    public SETTING mine;
    CharacterCustom custom;
    Item item;
    private void Awake()
    {
        custom = FindObjectOfType<CharacterCustom>();
        mesh = GetComponent<MeshFilter>();
    }
    public void ItemSet(Item _item)
    {
        if (_item)
        {
            item = _item;
            mesh.sharedMesh = _item.mesh;
            CustomSet();

        }
        else
        {
            item = null;
            mesh.sharedMesh = null;
            CustomSet();
        }
    } 
    void CustomSet()
    {
        if (mine == SETTING.body)
        {
            custom.body = this;
        }
        else if (mine == SETTING.gloveL)
        {
            custom.glove1 = this;
        }
        else if (mine == SETTING.gloveR)
        {
            custom.glove2 = this;
        }
        else if (mine == SETTING.head)
        {
            custom.head = this;
        }
        else if (mine == SETTING.tail)
        {
            custom.tail = this;
        }
        else if (mine == SETTING.item1)
        {
            custom.item1 = this;
        }
        else if (mine == SETTING.item2)
        {
            custom.item2 = this;
        }
    }
}
