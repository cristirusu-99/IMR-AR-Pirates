using System.Collections;
using System.Collections.Generic;
using Mapbox.Unity.Utilities;
using UnityEngine;

public class Badumts : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!GameObject.Find("RiddleScripts"))
        {
            GameObject obj = new GameObject("RiddleScripts");
            obj.AddComponent<GetRiddles>();
            obj.AddComponent<DontDestroyOnLoad>();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

}
