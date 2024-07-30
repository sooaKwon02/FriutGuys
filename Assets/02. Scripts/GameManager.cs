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
    public GameObject rankPanel;
    public GameObject rankbuttonPanel;
    public GameObject menuPanel;
    public GameObject idPanel;
    public GameObject settingMenuPanel;
    public GameObject CustomPanel;
    public GameObject profilePanel;
    public GameObject settingPanel;
    public GameObject audioPanl;
    public GameObject keyboardPanel;
    public GameObject settingExit;


    public GameObject roomListPanel;
    public GameObject playButtonPanel;
    public GameObject SearchRoomPanel;
    public GameObject createRoomPanel;




    private void Start()
    {
        store.SetActive(false);
        inventoryPanel.SetActive(false);
        SearchRoomPanel.SetActive(false);
        createRoomPanel.SetActive(false);
        rankPanel.SetActive(false);
        CustomPanel.SetActive(false);
        profilePanel.SetActive(false);
        settingMenuPanel.SetActive(false);
        ActiveMenu(true);
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
   
    public void CreateRoomOnOff(int num)
    {
        if (num == 0)
        {
            SearchRoomPanel.SetActive(true);
            ActiveMenu(false);
        }
        else if (num == 1)
        {
            createRoomPanel.SetActive(true);
            ActiveMenu(false);
        }
        else if (num == 2)
        {
            roomListPanel.SetActive(true);
            ActiveMenu(false);
        }      
        else
        {
            ActiveMenu(true);
            roomListPanel.SetActive(false);
            createRoomPanel.SetActive(false);
            SearchRoomPanel.SetActive(false);
        }
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
}
