using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class Verify : MonoBehaviour
{

     void Start()
    {
        if (ScenesData.backToLobby == true)
        {
            GameObject.Find("MainMenu").SetActive(false);
            Resources.FindObjectsOfTypeAll<GameObject>()
                        .FirstOrDefault(g => g.name == "PlayAs")
                        .SetActive(true);
        }
        if (ScenesData.nicknameIntroduced == true)
        {
            GameObject.Find("MainMenu").SetActive(false);
            Resources.FindObjectsOfTypeAll<GameObject>()
                        .FirstOrDefault(g => g.name == "Lobby")
                        .SetActive(true);
        }
    }
    public void ChangeToPlayAs()
    {
        /*if (ScenesData.backToLobby == true)
        {
            SceneManager.LoadScene("Home");
            GameObject.Find("MainMenu").SetActive(false);
            Resources.FindObjectsOfTypeAll<GameObject>()
                        .FirstOrDefault(g => g.name == "PlayAs")
                        .SetActive(true);
        }*/
    }

    // Start is called before the first frame update
    public void ChangeToSceneIfNicknameNotNull()
    {
        string nickName = GameObject.Find("InputField").GetComponent<InputField>().text;
        Debug.Log("Nickname:" + nickName);
        if (nickName != null && nickName != "" && !nickName.Contains(" "))
        {
            Resources.FindObjectsOfTypeAll<GameObject>()
                     .FirstOrDefault(g => g.name == "PlayAs")
                     .SetActive(true);
            GameObject.Find("MainMenu").SetActive(false);
        }
        else
        {
            Resources.FindObjectsOfTypeAll<GameObject>()
                     .FirstOrDefault(g => g.name == "Pop-upNickname")
                     .SetActive(true);
            Debug.Log("Enter nickname!");
        }
    }
    /*public void ChangeToSceneIfRoomCodeCorrect()
    {
        string nickName = GameObject.Find("InputField").GetComponent<InputField>().text;
        Debug.Log("Roomcode: " + nickName);
        if (nickName != null && nickName != "" && !nickName.Contains(" "))
        {
            *//*Resources.FindObjectsOfTypeAll<GameObject>()
                     .FirstOrDefault(g => g.name == "PlayAs")
                     .SetActive(true);
            GameObject.Find("MainMenu").SetActive(false);*//*
            Debug.Log("Game starts");
        }
        else
        {
            Resources.FindObjectsOfTypeAll<GameObject>()
                     .FirstOrDefault(g => g.name == "Pop-upRoomCode")
                     .SetActive(true);
            Debug.Log("Enter code!");
        }
    }*/
}