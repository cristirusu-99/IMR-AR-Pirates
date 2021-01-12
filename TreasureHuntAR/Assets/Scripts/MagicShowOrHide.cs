using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MagicShowOrHide : MonoBehaviour
{
    
    public GameObject makeitGONE;
    public Camera cam1;
    public Camera cam2;
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
        if(makeitGONE.name == "LocationMap")
        {
            cam2.enabled = false;
            cam1.enabled = true;
            for(int i = 0; i < 5; i++)
            {
                GameObject obj = GameObject.Find("Player" + i + "Location");
                if(obj != null)
                {
                    obj.SetActive(false);
                }
            }
        }
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
        if(makeitGONE.name == "LocationMap")
        {
            cam1.enabled = false;
            cam2.enabled = true;
            for (int i = 0; i < 5; i++)
            {
                string objectToFind = "Player" + i + "Location";
                GameObject obj = Resources.FindObjectsOfTypeAll<GameObject>()
                     .FirstOrDefault(g => g.name == objectToFind);
                if (obj != null)
                {
                    obj.SetActive(true);
                }
            }
        }
        ok = false;
    }
}
