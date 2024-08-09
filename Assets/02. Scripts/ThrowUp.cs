using Photon.Pun;
using UnityEngine;

public class ThrowUp : MonoBehaviour
{
    [HideInInspector]
    public Item item;
    public MeshFilter mesh;

    public Transform[] throwDir;
    Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void ItemSet(Item _item)
    {
        item = _item;
        mesh.sharedMesh = _item.mesh;   
    }
    public void Throw()
    {
        if (PhotonNetwork.IsConnected && PhotonNetwork.InRoom && GetComponentInParent<PhotonView>().IsMine)
        {
            GameObject obj = PhotonNetwork.Instantiate("Prefabs/" + item.name, ThrowDir().position, Quaternion.identity);
            obj.transform.localScale = new Vector3(5f, 5f, 5f);
            if (obj.GetComponent<Rigidbody>() == null)
            {
                Rigidbody rb = obj.AddComponent<Rigidbody>();
                rb.AddForce(ThrowDir().forward * item.speed);
            }
            mesh.sharedMesh = null;
        }
    }

    Transform ThrowDir()
    {
        if (item.useitemType == Item.ITEM_STYLE.Buff)       
            return throwDir[0];      
        else if(item.useitemType == Item.ITEM_STYLE.Debuff)
            return throwDir[1];
        else if (item.useitemType == Item.ITEM_STYLE.Trap)
            return throwDir[2];
        else 
            return throwDir[3];
    }
    public void ThrowAnim(Item.ITEM_STYLE item)
    {
        switch (item)
        {
            case Item.ITEM_STYLE.Buff:
                animator.SetTrigger("Buff");
                break;
            case Item.ITEM_STYLE.Debuff:
                animator.SetTrigger("Debuff");
                break;
            case Item.ITEM_STYLE.Trap:
                animator.SetTrigger("Trap");
                break;
            case Item.ITEM_STYLE.Bomb:
                animator.SetTrigger("Bomb");
                break;
        }
    }
}
