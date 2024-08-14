using Photon.Pun;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    public SaveLoad.PLAYER p;
    public PhotonView pv;
    public Image[] useItem;
    public ThrowUp throwUp;
    public GameObject UseItemImagePanel;
    public Text coolTimeText;
    bool coolTime;

    private void Awake()
    {
        if (GetComponent<PhotonView>())
        { 
            pv = GetComponent<PhotonView>();
        }
        p = SaveLoad.instance.player;
        nickName = SaveLoad.instance.nickName;
        if (pv.IsMine)
        {
            UseItemImagePanel.SetActive(true);
        }

    }   
    private void Start()
    {
        if (pv.IsMine&& PhotonNetwork.IsConnected && PhotonNetwork.InRoom)
        {
            coolTime = false;
            StartCoroutine(CustomPlayer());
            pv.RPC("UserInfoSet", RpcTarget.AllBuffered, nickName);
            ItemImageSet(); 
        }       
    }
   public void UseItem()
   {
        if(pv.IsMine&&!coolTime)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                ItemSwap();
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                if (item1.item != null)
                {
                    Animator animator = GetComponentInChildren<Animator>();

                    throwUp.ItemSet(Resources.Load<Item>("Item/UseItem/" + item1.item.name));
                    throwUp.ThrowAnim(item1.item.useitemType);
                    item1.ItemSet(null);   
                    p.item1 = "";
                    ItemSwap(); 
                    if (item1.item != null || item2.item != null)
                    {
                        StartCoroutine(CoolTimeCheck());
                    }
                }
                else
                {

                }
            }
        }       
   }
    IEnumerator CoolTimeCheck()
    {
        coolTime = true;
        useItem[0].gameObject.SetActive(false);
        useItem[1].gameObject.SetActive(false);
        coolTimeText.gameObject.SetActive(true);
        for (int i = 10; i > 0; i--)
        {
            coolTimeText.text = i.ToString();
            yield return new WaitForSeconds(1f); // 1초 대기
        }
        coolTime = false;
        coolTimeText.gameObject.SetActive(false);       
        useItem[0].gameObject.SetActive(true);
        useItem[1].gameObject.SetActive(true);
    }
    void ItemSwap()
    {
        Item item = item1.item;
        item1.ItemSet(item2.item);
        item2.ItemSet(item);
        string itemName = p.item1;
        p.item1 = p.item2;
        p.item2 = itemName;
        ItemImageSet();
    }
    void ItemImageSet()
    {
        if (item1.item != null) { useItem[0].enabled = true; useItem[0].sprite = item1.item.sprite; }
        else { useItem[0].enabled = false; }
        if (item2.item != null) { useItem[1].enabled = true; useItem[1].sprite = item2.item.sprite; }
        else { useItem[1].enabled = false; }
    }

    //Lobby 
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

    [PunRPC]
    void DestroyUserInfo(string _nick)
    {
        UserInfo[] userInfos = FindObjectsOfType<UserInfo>();
        foreach (UserInfo userInfo in userInfos)
        {
            if (userInfo.userName.text == _nick)
            {
                Destroy(userInfo.gameObject);
                break;
            }
        }
    }

    //마스터 클라이언트에서 처리
    [PunRPC]
    void KickPlayerRPC(string _nick)
    {
        //_nick과 현재 클라이언트의 이름이 같을 경우
        if(PhotonNetwork.NickName == _nick)
        {
            PlayerCon playerCon = FindObjectOfType<PlayerCon>();
            if (PhotonNetwork.InRoom)
            {
                PhotonNetwork.LeaveRoom();
            }
        }
    }
    IEnumerator CustomPlayer()
    {
        pv.RPC("CustomAtherPlayer", RpcTarget.AllBuffered,
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
        yield return null;
    }

    [PunRPC]
    public void CustomAtherPlayer(string bodyName, string glove1Name, string glove2Name, string headName, string tailName, string _item1, string _item2, float bodyX, float bodyY, float bodyZ, float glove1X, float glove1Y, float glove1Z, float glove2X, float glove2Y, float glove2Z, float headX, float headY, float headZ, float tailX, float tailY, float tailZ, float bodyRotX, float bodyRotY, float bodyRotZ, float glove1RotX, float glove1RotY, float glove1RotZ, float glove2RotX, float glove2RotY, float glove2RotZ, float headRotX, float headRotY, float headRotZ, float tailRotX, float tailRotY, float tailRotZ)
    {
        body.ItemSet(Resources.Load<Item>("Item/FashionItem/" + bodyName));
        glove1.ItemSet(Resources.Load<Item>("Item/FashionItem/" + glove1Name));
        glove2.ItemSet(Resources.Load<Item>("Item/FashionItem/" + glove2Name));
        head.ItemSet(Resources.Load<Item>("Item/FashionItem/" + headName));
        tail.ItemSet(Resources.Load<Item>("Item/FashionItem/" + tailName));
        item1.ItemSet(Resources.Load<Item>("Item/UseItem/" + _item1));
        item2.ItemSet(Resources.Load<Item>("Item/UseItem/" + _item2));
        body.transform.localScale = new Vector3(bodyX, bodyY, bodyZ);
        glove1.transform.localScale = new Vector3(glove1X, glove1Y, glove1Z);
        glove2.transform.localScale = new Vector3(glove2X, glove2Y, glove2Z);
        head.transform.localScale = new Vector3(headX, headY, headZ);
        tail.transform.localScale = new Vector3(tailX, tailY, tailZ);
        body.transform.localRotation = Quaternion.Euler(p.body_rotX, p.body_rotY, p.body_rotZ);
        glove1.transform.localRotation = Quaternion.Euler(p.glove1_rotX, p.glove1_rotY, p.glove1_rotZ);
        glove2.transform.localRotation = Quaternion.Euler(p.glove2_rotX, p.glove2_rotY, p.glove2_rotZ);
        head.transform.localRotation = Quaternion.Euler(p.head_rotX, p.head_rotY, p.head_rotZ);
        tail.transform.localRotation = Quaternion.Euler(p.tail_rotX, p.tail_rotY, p.tail_rotZ);
    }
}