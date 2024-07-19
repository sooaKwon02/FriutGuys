using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;


public class ScreenSetting : MonoBehaviour
{
    public int screen;
    public Text screenXT;
    public Text screenYT;
    bool on=true;
    public Text screenModeText;

    
       
    
 
    public void InputScreenSize()
    {       
        screen = 0;
        char[] ch1 = screenXT.text.ToCharArray();
        int screenX = screenSize(ch1);
        screen = 0;
        char[] ch2 = screenYT.text.ToCharArray();
        int screenY = screenSize(ch2);
        if (screen ==0)
        {
            return;
        }
        RectSizeChange(screenX, screenY, on);


    }
    int screenSize(char[] ch)
    {
        for (int i = 0; i < ch.Length; i++)
        {
            if (ch[i] >= 48 && ch[i] <= 57)
            {
                if (ch[0] != 48)
                {
                    screen =screen*10+(ch[i]-48);                   
                }
            }           
        }
        if(screen>400&&screen<2500)
        {
            return screen;
        }
        else
            return 0;
    }    
  
    public void OnSize()
    {
        on = !on;
        if(on)
        screenModeText.text = "전체 화면";
        else
        screenModeText.text = "창 화면";
        InputScreenSize();
    }
    void RectSizeChange(int screenX, int screenY, bool _on)
    {
        GameObject canvas = GameObject.FindGameObjectWithTag("Canvas");
        Camera.main.aspect = screenX / screenY;
        Camera.main.ScreenToViewportPoint(new Vector2(screenX, screenY));
        Screen.SetResolution(screenX, screenY, _on);



    }
}
