using UnityEngine;

public class PlayerItem : MonoBehaviour
{
    public MeshFilter mesh;
    public Item item;
    private void Awake()
    {
        mesh = GetComponent<MeshFilter>();
    }
    public void ItemSet(Item _item)
    {
        if (_item)
        {
            item = _item;
            mesh.sharedMesh = _item.mesh;

        }
        else
        {
            item = null;
            mesh.sharedMesh = null;
        }
    } 
   
}
