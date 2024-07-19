using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Transform Player;
    public GameObject store;
    public GameObject inventoryPanel;
    public GameObject inventory;   
    public GameObject createRoom;
    public GameObject rankPanel;
    public GameObject rankbuttonPanel;
    public GameObject playButtonPanel;
    public GameObject menuPanel;
    public GameObject idPanel;
    public GameObject settingMenuPanel;
    public GameObject CustomPanel;
    public GameObject profilePanel;
    public GameObject settingPanel;
    public GameObject audioPanl;
    public GameObject keyboardPanel;
    public GameObject settingExit;




    private void Start()
    {
        Screen.SetResolution(1920, 1080, true);
        store.SetActive(false);
        inventoryPanel.SetActive(false);
        createRoom.SetActive(false);
        rankPanel.SetActive(false);
        CustomPanel.SetActive(false);
        profilePanel.SetActive(false);
        settingMenuPanel.SetActive(false);
    }
 
    void ActiveMenu(bool active)
    {
        rankbuttonPanel.SetActive(active);
        playButtonPanel.SetActive(active);
        menuPanel.SetActive(active);
        idPanel.SetActive(active);
    }
    public void InventoryOnOff(bool check)
    {
                
        
        if (!store.activeSelf&& !CustomPanel.activeSelf)
        {
            inventoryPanel.SetActive(check);
            ActiveMenu(!check);
            if (check)
                Player.position = new Vector2(2, 0);
            else
                Player.position = new Vector2(0, 0);
        }
      


    }      
    public void StoreOnOff(bool check)
    {
        store.SetActive(check);
        inventoryPanel.SetActive(check);
        ActiveMenu(!check);
        if(!check)
            Player.position = new Vector2(0,0);

    }
    public void SettingOnOff(bool check)
    {
        settingMenuPanel.SetActive(check);
        ActiveMenu(!check);
    }
    public void SetMenu(int num)
    {
        settingExit.SetActive(true);
        if (num == 0)
        {
            settingPanel.SetActive(true);
        }
        else if(num == 1)
        {
            audioPanl.SetActive(true);
        }
        else if (num == 2)
        {
            keyboardPanel.SetActive(true);
        }
        else
        {
            settingExit.SetActive(false);
            settingPanel.SetActive(false);
            audioPanl.SetActive(false);
            keyboardPanel.SetActive(false);
        }            
    }
   
    public void CreateRoomOnOff(bool check)
    {
        createRoom.SetActive(check);
        ActiveMenu(!check);
    }      
    public void RankPanelOnOff(bool check)
    {
        rankPanel.SetActive(check);
        ActiveMenu(!check);
    }
    public void CustomPanelOnOff(bool check)
    {
        CustomPanel.SetActive(check);
        inventoryPanel.SetActive(check);
        Player.GetComponent<CharacterCustom>().enabled = check;
        Player.GetComponentInChildren<Animator>().enabled = !check;
        Player.GetComponentInChildren<PlayerCtrl>().enabled = !check;    
        if (check)
        {
            Player.position = new Vector2(2, 0);
        }
        else
        {
            Player.position = new Vector2(0, 0);
        }                  
            ActiveMenu(!check);
    }
    public void ProfilePanelOnOff(bool check)
    {
        profilePanel.SetActive(check);
        ActiveMenu(!check);
    }  
    
    public void CreateRoom()
    {
        
    }
    public void CreateRandomRoom()
    {

    }

}
