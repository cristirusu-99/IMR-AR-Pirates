using System.Collections.Generic;
using UnityEngine;
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
            if(GameObject.Find("ARLocalization").GetComponent<ARLocalization>().currentRiddleText != "")
            {
                PlaceObject();
            }
            
        }
    }

    private void PlaceObject()
    {
            Vector3 objectPosition = new Vector3(placementPose.position.x, placementPose.position.y + 1, placementPose.position.z);
            Vector3 objectRotation = new Vector3(1, 1, 1);
            Instantiate(objectToPlace, objectPosition, Quaternion.Euler(objectRotation));
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