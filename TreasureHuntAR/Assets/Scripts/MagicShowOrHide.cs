using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicShowOrHide : MonoBehaviour
{
    
    public GameObject makeitGONE;
    bool ok=true;
     public void ShowOrHide()
    {
        if (ok == true)
            Show();
        else
            Hide();
        
    }
    public void Hide()
    {
        makeitGONE.SetActive(false);
        ok = true;
    }
    public void Show()
    {
        makeitGONE.SetActive(true);
        if(makeitGONE.name == "RiddlesFound")
        {
            int[] foundRiddles = GameObject.Find("ARLocalization").GetComponent<ARLocalization>().foundRiddles;
            string[] riddlesTexts = GameObject.Find("ARLocalization").GetComponent<ARLocalization>().riddlesTexts;
            for (int i = 0; i < foundRiddles.Length; i++)
            {

                if (foundRiddles[i] == 1)
                {
                    GameObject.Find("Riddle" + (i + 1).ToString()).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = riddlesTexts[i];
                }
            }
        }
        ok = false;
    }
}
