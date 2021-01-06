using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SceneKids : MonoBehaviour
{
    // Start is called before the first frame update
    
    void Start()
    {   
        int riddlenb = Int32.Parse(GameObject.Find("RiddleNumber").GetComponent<Text>().text);
        if( riddlenb > 1)
        {   GameObject.Find("MainMenu").SetActive(false);
            Resources.FindObjectsOfTypeAll<GameObject>()
                        .FirstOrDefault(g => g.name == "Lobby")
                        .SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
