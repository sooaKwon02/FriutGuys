using Photon.Realtime;
using Photon.Pun;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterCustom : MonoBehaviourPunCallbacks
{
    public string nickName;
    public PlayerItem body;

    public PlayerItem glove1;

    public PlayerItem glove2;

    public PlayerItem head;

    public PlayerItem tail;

    public PlayerItem item1;
    public PlayerItem item2;
    public int gameMoney;
    public int cashMoney;
    public int score;
    public bool goalIn;
    //�÷��̾� ����
    SaveLoad.PLAYER p;
    public PhotonView pv;

    private void Awake()
    {
        if (GetComponent<PhotonView>()) { pv = GetComponent<PhotonView>(); }
        nickName = FindObjectOfType<SaveLoad>().nickName;

    }
    private void Start()
    {
        if (FindObjectOfType<SaveLoad>())
        {
            p = FindObjectOfType<SaveLoad>().player;
        }    
        if(pv != null&&pv.IsMine)
        {
            pv.RPC("UserInfoSet", RpcTarget.AllBuffered, nickName);
        }
    }

  
    [PunRPC]
    void SetPlayerReady(string _nick)
    {
        foreach (UserInfo info in FindObjectsOfType<UserInfo>())
        {
            if (info.userName.text == _nick)
            {
                info.Ready();
            }
        }
    }
    [PunRPC]
    void UserInfoSet(string _nick)
    {
        if(FindObjectOfType<PlayerCon>())
        FindObjectOfType<PlayerCon>().UserInfoSet( _nick);
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
        body.transform.localRotation = new Quaternion(p.body_rotX, p.body_rotY, p.body_rotZ, 1);
        glove1.transform.localRotation = new Quaternion(p.glove1_rotX, p.glove1_rotY, p.glove1_rotZ, 1);
        glove2.transform.localRotation = new Quaternion(p.glove2_rotX, p.glove2_rotY, p.glove2_rotZ, 1);
        head.transform.localRotation = new Quaternion(p.head_rotX, p.head_rotY, p.head_rotZ, 1);
        tail.transform.localRotation = new Quaternion(p.tail_rotX, p.tail_rotY, p.tail_rotZ, 1);

        if (PhotonNetwork.IsConnected && PhotonNetwork.InRoom)
        {
            pv.RPC("CustomAtherPlayer", RpcTarget.OthersBuffered, pv.ViewID,
                p.body_name, p.glove1_name, p.glove2_name, p.head_name, p.tail_name,
                p.item1, p.item2,
                p.body_x, p.body_y, p.body_z,
                p.glove1_x, p.glove1_y, p.glove1_z,
                p.glove2_x, p.glove2_y, p.glove2_z,
                p.head_x, p.head_y, p.head_z,
                p.tail_x, p.tail_y, p.tail_z,
                p.body_rotX, p.body_rotY, p.body_rotZ,
                p.glove1_rotX, p.glove1_rotY, p.glove1_rotZ,
                p.glove2_rotX, p.glove2_rotY, p.glove2_rotZ,
                p.head_rotX, p.head_rotY, p.head_rotZ,
                p.tail_rotX, p.tail_rotY, p.tail_rotZ);
        }
        yield return null;
    }

    [PunRPC]
    public void CustomAtherPlayer(int viewID, string bodyName, string glove1Name, string glove2Name, string headName, string tailName, string _item1, string _item2, float bodyX, float bodyY, float bodyZ, float glove1X, float glove1Y, float glove1Z, float glove2X, float glove2Y, float glove2Z, float headX, float headY, float headZ, float tailX, float tailY, float tailZ, float bodyRotX, float bodyRotY, float bodyRotZ, float glove1RotX, float glove1RotY, float glove1RotZ, float glove2RotX, float glove2RotY, float glove2RotZ, float headRotX, float headRotY, float headRotZ, float tailRotX, float tailRotY, float tailRotZ)
    {
        PhotonView[] views = FindObjectsOfType<PhotonView>();
        foreach (PhotonView _pv in views)
            if (_pv.ViewID == viewID)
            {
                Debug.Log(viewID);
                _pv.GetComponent<CharacterCustom>().body.ItemSet(Resources.Load<Item>("Item/FashionItem/" + bodyName));
                _pv.GetComponent<CharacterCustom>().glove1.ItemSet(Resources.Load<Item>("Item/FashionItem/" + glove1Name));
                _pv.GetComponent<CharacterCustom>().glove2.ItemSet(Resources.Load<Item>("Item/FashionItem/" + glove2Name));
                _pv.GetComponent<CharacterCustom>().head.ItemSet(Resources.Load<Item>("Item/FashionItem/" + headName));
                _pv.GetComponent<CharacterCustom>().tail.ItemSet(Resources.Load<Item>("Item/FashionItem/" + tailName));
                _pv.GetComponent<CharacterCustom>().item1.ItemSet(Resources.Load<Item>("Item/UseItem/" + _item1));
                _pv.GetComponent<CharacterCustom>().item2.ItemSet(Resources.Load<Item>("Item/UseItem/" + _item2));
                _pv.GetComponent<CharacterCustom>().body.transform.localScale = new Vector3(bodyX, bodyY, bodyZ);
                _pv.GetComponent<CharacterCustom>().glove1.transform.localScale = new Vector3(glove1X, glove1Y, glove1Z);
                _pv.GetComponent<CharacterCustom>().glove2.transform.localScale = new Vector3(glove2X, glove2Y, glove2Z);
                _pv.GetComponent<CharacterCustom>().head.transform.localScale = new Vector3(headX, headY, headZ);
                _pv.GetComponent<CharacterCustom>().tail.transform.localScale = new Vector3(tailX, tailY, tailZ);
                _pv.GetComponent<CharacterCustom>().body.transform.localRotation = new Quaternion(bodyRotX, bodyRotY, bodyRotZ, 1);
                _pv.GetComponent<CharacterCustom>().glove1.transform.localRotation = new Quaternion(glove1RotX, glove1RotY, glove1RotZ, 1);
                _pv.GetComponent<CharacterCustom>().glove2.transform.localRotation = new Quaternion(glove2RotX, glove2RotY, glove2RotZ, 1);
                _pv.GetComponent<CharacterCustom>().head.transform.localRotation = new Quaternion(headRotX, headRotY, headRotZ, 1);
                _pv.GetComponent<CharacterCustom>().tail.transform.localRotation = new Quaternion(tailRotX, tailRotY, tailRotZ, 1);
            }
    }
}