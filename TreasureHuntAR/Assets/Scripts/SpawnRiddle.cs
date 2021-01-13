using System;
using System.Collections.Generic;
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
    public GameObject riddleMessage;
    private float timeToAppearTreasure = 4f;
    private float timeWhenDisappearTreasure = 0;
    private float timeToAppearRiddle = 4f;
    private float timeWhenDisappearRiddle = 0;
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
        if (Time.time >= timeWhenDisappearRiddle)
        {
            riddleMessage.SetActive(false);
        }
        if (Time.time >= timeWhenDisappearRiddle)
        {
            riddleMessage.SetActive(false);
        }
        if (placementPoseIsValid)
        {
            string currentRiddleText = GameObject.Find("ARLocalization").GetComponent<ARLocalization>().currentRiddleText;
            int currentRiddleNumber = GameObject.Find("ARLocalization").GetComponent<ARLocalization>().currentRiddleNumber;
            double[] riddlesCoords = GameObject.Find("ARLocalization").GetComponent<ARLocalization>().riddlesCoords;
            int[] foundRiddles = GameObject.Find("ARLocalization").GetComponent<ARLocalization>().foundRiddles;
            bool treasureFound = GameObject.Find("ARLocalization").GetComponent<ARLocalization>().treasureFound;
            if (currentRiddleText != null && currentRiddleText != "")
            {
                double[] currentLocation = GameObject.Find("ARLocalization").GetComponent<ARLocalization>().GetCurrentLocation();
                float bearing = (float)GameObject.Find("ARLocalization").GetComponent<ARLocalization>().CalculateBearingInDegreesBetweenTwoCoords(currentLocation[0], currentLocation[1], riddlesCoords[2 * currentRiddleNumber], riddlesCoords[2 * currentRiddleNumber + 1]);
                float degrees = GameObject.Find("ARLocalization").GetComponent<CompassBehaviour>().degrees;
                /*if (GameObject.Find("DebugText1").GetComponent<Text>().text == "")
                {
                    GameObject.Find("DebugText1").GetComponent<Text>().text = currentRiddleText;
                   
                }*/
                if (bearing > degrees)
                {
                    
                }
                else
                {
                    //GameObject.Find("DebugText2").GetComponent<Text>().text = Math.Min(degrees - bearing, 360 - degrees + bearing).ToString();
                }
                if (bearing > degrees)
                {
                    if (Math.Min(bearing - degrees, 360 - bearing + degrees) < 70)
                    {
                        //GameObject.Find("DebugText2").GetComponent<Text>().text = Math.Min(bearing - degrees, 360 - bearing + degrees).ToString();
                        

                        if (GameObject.Find("RiddleObject" + currentRiddleNumber) == null)
                        {
                            timeWhenDisappearRiddle = Time.time + timeToAppearRiddle;
                            riddleMessage.SetActive(true);
                            foundRiddles[currentRiddleNumber] = 1;
                            //GameObject.Find("DebugText2").GetComponent<Text>().text = currentRiddleNumber.ToString();
                            PlaceObject(currentRiddleNumber);
                        }
                        else
                        {
                            
                        }

                    }
                }
                else
                {
                    if (Math.Min(degrees - bearing, 360 - degrees + bearing) < 70)
                    {

 
                        if (GameObject.Find("RiddleObject" + currentRiddleNumber) == null)
                        {
                            timeWhenDisappearRiddle = Time.time + timeToAppearRiddle;
                            riddleMessage.SetActive(true);
                            foundRiddles[currentRiddleNumber] = 1;
                            //GameObject.Find("DebugText2").GetComponent<Text>().text = currentRiddleNumber.ToString();
                            PlaceObject(currentRiddleNumber);
                        }
                        else
                        {
                            
                        }

                    }
                }
               
            }
            if(treasureFound)
            {
                if (GameObject.Find("Treasure") == null)
                {
                    timeWhenDisappearTreasure = Time.time + timeToAppearTreasure;
                    treasureObject.SetActive(true);
                    PlaceTreasure();
                }
                else
                {
                    if(Time.time >= timeWhenDisappearTreasure)
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
            //placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else
        {
            //placementIndicator.SetActive(false);
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