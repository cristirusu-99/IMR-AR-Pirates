using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GenerateRoomCode : MonoBehaviour
{
    Text roomCode;
    // Start is called before the first frame update
    void Start()
    {
        roomCode = GetComponent<Text>();
        roomCode.text = "Room code : " + RandomString(4);
}

    // Update is called once per frame
    void Update()
    {
        
    }

    public string RandomString(int length)
    {
        System.Random random = new System.Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
        .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}
