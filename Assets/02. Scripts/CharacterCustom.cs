using Photon.Realtime;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using static SaveLoad;

public class CharacterCustom : MonoBehaviour
{
    public MeshFilter[] bodyParts;
    public float rotationSpeed;

    

    public GameObject body;
    float body_x;
    float body_y;
    float body_z;
    float body_rotX;
    float body_rotY;
    float body_rotZ;

    public GameObject glove1;
    float glove1_x;
    float glove1_y;
    float glove1_z;
    float glove1_rotX;
    float glove1_rotY;
    float glove1_rotZ;

    public GameObject glove2;
    float glove2_x;
    float glove2_y;
    float glove2_z;
    float glove2_rotX;
    float glove2_rotY;
    float glove2_rotZ;

    public GameObject head;
    float head_x;
    float head_y;
    float head_z;
    float head_rotX;
    float head_rotY;
    float head_rotZ;

    public GameObject tail;
    float tail_x;
    float tail_y;
    float tail_z;
    float tail_rotX;
    float tail_rotY;
    float tail_rotZ;

    string item1;
    string item2;
    int gameMoney;
    int cashMoney;
    int score;
    //플레이어 정보
    SaveLoad.PLAYER p;
    private void Awake()
    {
    }
    private void Start()
    {
        if (FindObjectOfType<SaveLoad>())
        {
            p = FindObjectOfType<SaveLoad>().player;
        }
        StartCoroutine(CustomPlayer());
        StartCoroutine(PlayerInfo());
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        { transform.Rotate(new Vector3(0, 1, 0) * -rotationSpeed * Time.deltaTime); }
        if (Input.GetKey(KeyCode.LeftArrow))
        { transform.Rotate(new Vector3(0, 1, 0) * rotationSpeed * Time.deltaTime); }
    }
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindObjectOfType<SaveLoad>().OnDataChanged(p.ID);
    }

    IEnumerator CustomPlayer()
    {
            body.GetComponent<MeshFilter>().mesh = Resources.Load<Item>("Item/FashionItem"+p.body_name).mesh;
            glove1.GetComponent<MeshFilter>().mesh = Resources.Load<Item>("Item/FashionItem" + p.glove1_name).mesh;
            glove2.GetComponent<MeshFilter>().mesh = Resources.Load<Item>("Item/FashionItem" + p.glove2_name).mesh;
            head.GetComponent<MeshFilter>().mesh = Resources.Load<Item>("Item/FashionItem" + p.head_name).mesh;
            tail.GetComponent<MeshFilter>().mesh = Resources.Load<Item>("Item/FashionItem" + p.tail_name).mesh;
        body.transform.localScale = new Vector3(p.body_x, p.body_y, p.body_z);
        glove1.transform.localScale = new Vector3(p.glove1_x, p.glove1_y, p.glove1_z);
        glove2.transform.localScale = new Vector3(p.glove2_x, p.glove2_y, p.glove2_z);
        head.transform.localScale = new Vector3(p.head_x, p.head_y, p.head_z);
        tail.transform.localScale = new Vector3(p.tail_x, p.tail_y, p.tail_z);
        body.transform.localRotation = new Quaternion(p.body_rotX, p.body_rotY, p.body_rotZ, 1);
        glove1.transform.localRotation = new Quaternion(p.glove1_rotX, p.glove1_rotY, p.glove1_rotZ, 1);
        glove2.transform.localRotation = new Quaternion(p.glove2_rotX, p.glove2_rotY, p.glove2_rotZ, 1);
        head.transform.localRotation = new Quaternion(p.head_rotX, p.head_rotY, p.head_rotZ, 1);
        tail.transform.localRotation = new Quaternion(p.tail_rotX, p.tail_rotY, p.tail_rotZ, 1);
        yield return null;
    }
    IEnumerator PlayerInfo()
    {
        yield return null;  
    }

}
