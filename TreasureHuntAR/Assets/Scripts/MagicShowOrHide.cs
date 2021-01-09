using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicShowOrHide : MonoBehaviour
{
    
    public GameObject makeitGONE;
    bool ok=true;
     public void ShowOrHide()
    {
        if (ok == false)
            Show();
        else
            Hide();
        
    }
    public void Show()
    {
        makeitGONE.SetActive(false);
        ok = true;
    }
    public void Hide()
    {
        makeitGONE.SetActive(true);
        ok = false;
    }
}
