using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCustom : MonoBehaviour
{
    public MeshFilter[] bodyParts;
    public ItemData[] CustomInventory; 
    public float rotationSpeed;
    private void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        { transform.Rotate(new Vector3(0, 1, 0) * -rotationSpeed * Time.deltaTime); }
        if (Input.GetKey(KeyCode.LeftArrow))
        { transform.Rotate(new Vector3(0, 1, 0) * rotationSpeed * Time.deltaTime); }
    }
    public void CharSet()
    {
        for(int i=0;i<bodyParts.Length;i++)
        {
            if (CustomInventory[i].item)
            {
                bodyParts[i].mesh = CustomInventory[i].item.mesh;
            }
            else
            {
                bodyParts[i].mesh = null;
            }
                
        }
    }
    public void Hold()
    {
        if (GetComponentInChildren<PlayerCtrl>() && GetComponentInChildren<Rigidbody>())
        {
            GetComponentInChildren<PlayerCtrl>().enabled = false;
            GetComponentInChildren<Rigidbody>().isKinematic = true;
        }
    }
}
