using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class SpawnRiddle : MonoBehaviour
{
    public GameObject objectToPlace;
    public GameObject treasure;
    public GameObject placementIndicator;
    public GameObject treasureObject;
    private float timeToAppear = 3f;
    private float timeWhenDisappear = 0;
    private ARRaycastManager aRRaycastManager;
    private Pose placementPose;
    private bool placementPoseIsValid = false;

    void Start()
    {
        aRRaycastManager = FindObjectOfType<ARRaycastManager>();
    }

    void Update()
    {
        UpdatePlacementPose();
        UpdatePlacementIndicator();

        if (placementPoseIsValid)
        {
            string currentRiddleText = GameObject.Find("ARLocalization").GetComponent<ARLocalization>().currentRiddleText;
            int currentRiddleNumber = GameObject.Find("ARLocalization").GetComponent<ARLocalization>().currentRiddleNumber;
            int[] foundRiddles = GameObject.Find("ARLocalization").GetComponent<ARLocalization>().foundRiddles;
            bool treasureFound = GameObject.Find("ARLocalization").GetComponent<ARLocalization>().treasureFound;
            if (currentRiddleText != null && currentRiddleText != "" && currentRiddleNumber != -1)
            {
                if(GameObject.Find("DebugText1").GetComponent<Text>().text == "")
                {
                    GameObject.Find("DebugText1").GetComponent<Text>().text = currentRiddleText;
                    GameObject.Find("DebugText2").GetComponent<Text>().text = foundRiddles[0].ToString();
                }
                if(GameObject.Find("RiddleObject" + currentRiddleNumber) == null)
                {
                    PlaceObject(currentRiddleNumber);
                }        
            }
            if(treasureFound)
            {
                if (GameObject.Find("Treasure") == null)
                {
                    timeWhenDisappear = Time.time + timeToAppear;
                    treasureObject.SetActive(true);
                    PlaceTreasure();
                }
                else
                {
                    if(Time.time >= timeWhenDisappear)
                    {
                        treasureObject.SetActive(false);
                    }
                }
            }
        }
    }

    private void PlaceTreasure()
    {
        Vector3 objectPosition = new Vector3(placementPose.position.x, placementPose.position.y, placementPose.position.z);
        Vector3 objectRotation = new Vector3(1, 1, 1);
        GameObject instance = Instantiate(treasure, objectPosition, Quaternion.identity);
        instance.name = "Treasure";
    }

    private void PlaceObject(int currentRiddleNumber)
    {
            Vector3 objectPosition = new Vector3(placementPose.position.x, placementPose.position.y + 1, placementPose.position.z);
            Vector3 objectRotation = new Vector3(1, 1, 1);
            GameObject instance = Instantiate(objectToPlace, objectPosition, Quaternion.identity);
            instance.name = "RiddleObject" + currentRiddleNumber;

            /*Instantiate(objectToPlace, placementPose.position, placementPose.rotation);*/
    }

    private void UpdatePlacementIndicator()
    {
        if (placementPoseIsValid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else
        {
            placementIndicator.SetActive(false);
        }
    }

    private void UpdatePlacementPose()
    {
        //var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(Screen.width / 2, Screen.height / 2);
        var hits = new List<ARRaycastHit>();
        aRRaycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.Planes);

        placementPoseIsValid = hits.Count > 0;
        if (placementPoseIsValid)
        {
            placementPose = hits[0].pose;

            var cameraForward = Camera.current.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
            placementPose.rotation = Quaternion.LookRotation(cameraBearing);
        }
    }
}