using System.Collections;
using System.Collections.Generic;
using Mapbox.Examples;
using UnityEngine;
using UnityEngine.UI;

public class GetRiddles : MonoBehaviour
{
    
    GameObject inputfield;
    int n = 5; 
    int i = 0 ;
    public string[] riddle = new string[5];
    public GameObject[] text_riddle = new GameObject[5];
    public GameObject[] coord_riddle = new GameObject[5];
    public void StoreRiddles()
    {   
        riddle[i] = inputfield.GetComponent<Text>().text;
        text_riddle[i] = new GameObject("Text_Riddle" + i.ToString() );
        text_riddle[i].transform.parent= this.gameObject.transform;
        text_riddle[i].AddComponent<Text>();
        text_riddle[i].GetComponent<Text>().text = riddle[i];
        coord_riddle[i] = new GameObject("Coord_Riddle" + i.ToString() );
        coord_riddle[i].transform.parent= text_riddle[i].gameObject.transform;
        coord_riddle[i].AddComponent<Text>();
        i++;
    }
    void Start() {
        inputfield = GameObject.Find("Riddletext");
    }
    
}
