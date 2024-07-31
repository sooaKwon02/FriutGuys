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
            bodyParts[i].mesh = CustomInventory[i].item != null ? CustomInventory[i].item.mesh : null;
        }
    }  
}
