using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class SpawnRiddle : MonoBehaviour
{
    public GameObject objectToPlace;
    public GameObject placementIndicator;
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
            if (currentRiddleText != null && currentRiddleText != "" && currentRiddleNumber != 0)
            {
                if(GameObject.Find("DebugText1").GetComponent<Text>().text == "")
                {
                    GameObject.Find("DebugText1").GetComponent<Text>().text = currentRiddleText;
                }
                if(GameObject.Find("RiddleObject" + currentRiddleNumber) == null)
                {
                    PlaceObject(currentRiddleNumber);
                }        
            }     
        }
    }

    private void PlaceObject(int currentRiddleNumber)
    {
            Vector3 objectPosition = new Vector3(placementPose.position.x, placementPose.position.y + 1, placementPose.position.z);
            Vector3 objectRotation = new Vector3(1, 1, 1);
            GameObject instance = Instantiate(objectToPlace, objectPosition, Quaternion.Euler(objectRotation));
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