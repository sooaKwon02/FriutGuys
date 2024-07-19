using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCustom : MonoBehaviour
{
    public MeshFilter[] bodyParts;
    public SkinnedMeshRenderer[] idleParts;
    public ItemData[] CustomInventory; 
    public float rotationSpeed;
    public int customCheck;
    private void Awake()
    {        
        for(int i=0;i< bodyParts.Length;i++)
        {
            if (bodyParts[i].name == bodyParts[i].mesh.name)
            {
                idleParts[i].enabled = true;
            }
            else
            {
                idleParts[i].enabled = false;
            }                
        }
    }
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
                idleParts[i].enabled = false;
            }
            else 
            {
                bodyParts[i].mesh = null;
                idleParts[i].enabled = true;
            }
                
        }
    }

}
