
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SaveRiddlesText : MonoBehaviour
{

    void Start()
    {
        ScenesData.treasForT = false;
        ScenesData.nicknameIntroduced = true;
        Debug.Log("Current riddle :  " + ScenesData.currentRiddle);
        GameObject originalRiddle = GameObject.Find("SetRiddles");
        originalRiddle.gameObject.transform.GetChild(1).gameObject.GetComponent<Text>().text = "Riddle 1";
        originalRiddle.gameObject.transform.GetChild(0).gameObject.GetComponent<InputField>().text = ScenesData.riddlesText[1];

        for (int i = 2; i < 6; i++)
        {
            Debug.Log("ScenesRiddleText : " + ScenesData.riddlesText[i]);
            if(ScenesData.riddlesText[i] != null && ScenesData.riddlesText[i] != "")
            {
                if(i == 5)
                {
                    GameObject.Find("New riddle").SetActive(false);
                }
                GameObject newRiddle = Instantiate(originalRiddle, originalRiddle.transform.parent);
                newRiddle.gameObject.name = "SetRiddles" + i;
                newRiddle.gameObject.transform.GetChild(1).gameObject.GetComponent<Text>().text = "Riddle " + i;
                newRiddle.gameObject.transform.GetChild(0).gameObject.GetComponent<InputField>().text = ScenesData.riddlesText[i];
                
                string objectToFind = "Riddle" + i + "Button";
                Resources.FindObjectsOfTypeAll<GameObject>()
                         .FirstOrDefault(g => g.name == objectToFind)
                         .SetActive(true);
            }
        }
        if(ScenesData.currentRiddle == 1)
        {
            GameObject[] riddles = GameObject.FindGameObjectsWithTag("SetRiddle");
            foreach (GameObject riddle in riddles)
            {
                if (riddle.activeSelf)
                {
                    if(riddle.name != "SetRiddles")
                    {
                        riddle.SetActive(false);
                    }
                }
            }
        } 
        else
        {
            GameObject[] riddles = GameObject.FindGameObjectsWithTag("SetRiddle");
            string objectToFind = "SetRiddles" + ScenesData.currentRiddle;
            foreach (GameObject riddle in riddles)
            {
                if (riddle.activeSelf)
                {
                    if (riddle.name != objectToFind)
                    {
                        riddle.SetActive(false);
                    }
                }
            }
        }
    }

    public void AddRiddles()
    {
        GameObject[] riddles = GameObject.FindGameObjectsWithTag("SetRiddle");
        GameObject activeRiddle = null;
        foreach(GameObject riddle in riddles)
        {
            if (riddle.activeSelf)
            {
                activeRiddle = riddle;
                break;
            }
           
        }
        if(ScenesData.currentRiddle > 5)
        {
            return;
        } 
        else
        {
            ScenesData.numberOfRiddles++;
            ScenesData.currentRiddle = ScenesData.numberOfRiddles;
            GameObject newRiddle = Instantiate(activeRiddle, activeRiddle.transform.parent);
            newRiddle.gameObject.name = "SetRiddles" + ScenesData.numberOfRiddles;
            newRiddle.gameObject.transform.GetChild(1).gameObject.GetComponent<Text>().text = "Riddle " + ScenesData.currentRiddle;
            newRiddle.gameObject.transform.GetChild(0).gameObject.GetComponent<InputField>().text = "Enter your riddle here";
            ScenesData.riddlesText[ScenesData.currentRiddle] = "Enter your riddle here";
            string objectToFind = "Riddle" + ScenesData.currentRiddle + "Button";
            Resources.FindObjectsOfTypeAll<GameObject>()
                     .FirstOrDefault(g => g.name == objectToFind)
                     .SetActive(true);
            activeRiddle.SetActive(false);
            if (ScenesData.numberOfRiddles > 4)
            {
                GameObject.Find("New riddle").SetActive(false);
                return;
            }
            
            
        }
       
    }

    public void ChangeRiddle1()
    {
        if(ScenesData.currentRiddle == 1)
        {
            return;
        }
        ScenesData.currentRiddle = 1;
        GameObject[] riddles = GameObject.FindGameObjectsWithTag("SetRiddle");
        foreach (GameObject riddle in riddles)
        {
            if (riddle.activeSelf)
            {
                riddle.SetActive(false);
                break;
            }
        }
        Resources.FindObjectsOfTypeAll<GameObject>()
                     .FirstOrDefault(g => g.name == "SetRiddles")
                     .SetActive(true);
    }

    public void ChangeRiddle2()
    {
        if (ScenesData.currentRiddle == 2)
        {
            return;
        }
        ScenesData.currentRiddle = 2;
        GameObject[] riddles = GameObject.FindGameObjectsWithTag("SetRiddle");
        foreach (GameObject riddle in riddles)
        {
            if (riddle.activeSelf)
            {
                riddle.SetActive(false);
                break;
            }
        }
        Resources.FindObjectsOfTypeAll<GameObject>()
                     .FirstOrDefault(g => g.name == "SetRiddles2")
                     .SetActive(true);
    }

    public void ChangeRiddle3()
    {
        if (ScenesData.currentRiddle == 3)
        {
            return;
        }

        ScenesData.currentRiddle = 3;
        GameObject[] riddles = GameObject.FindGameObjectsWithTag("SetRiddle");
        foreach (GameObject riddle in riddles)
        {
            if (riddle.activeSelf)
            {
                riddle.SetActive(false);
                break;
            }
        }
        Resources.FindObjectsOfTypeAll<GameObject>()
                     .FirstOrDefault(g => g.name == "SetRiddles3")
                     .SetActive(true);
    }

    public void ChangeRiddle4()
    {
        if (ScenesData.currentRiddle == 4)
        {
            return;
        }

        ScenesData.currentRiddle = 4;
        GameObject[] riddles = GameObject.FindGameObjectsWithTag("SetRiddle");
        foreach (GameObject riddle in riddles)
        {
            if (riddle.activeSelf)
            {
                riddle.SetActive(false);
                break;
            }
        }
        Resources.FindObjectsOfTypeAll<GameObject>()
                     .FirstOrDefault(g => g.name == "SetRiddles4")
                     .SetActive(true);
    }

    public void ChangeRiddle5()
    {
        if (ScenesData.currentRiddle == 5)
        {
            return;
        }

        ScenesData.currentRiddle = 5;
        GameObject[] riddles = GameObject.FindGameObjectsWithTag("SetRiddle");
        foreach (GameObject riddle in riddles)
        {
            if (riddle.activeSelf)
            {
                riddle.SetActive(false);
                break;
            }
        }
        Resources.FindObjectsOfTypeAll<GameObject>()
                     .FirstOrDefault(g => g.name == "SetRiddles5")
                     .SetActive(true);
    }

    public void SaveRiddlesTextInput(InputField input)
    {
        if (input.text.Length > 0)
        {
            Debug.Log(input.text);
        }
        else if (input.text.Length == 0)
        {
            Debug.Log("Main Input Empty");
        }
        ScenesData.riddlesText[ScenesData.currentRiddle] = input.text;
        foreach(string riddleText in ScenesData.riddlesText)
        {
            Debug.Log("Riddle text: " + riddleText);
        }
    }
}
