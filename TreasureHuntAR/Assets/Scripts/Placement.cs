using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class Placement : MonoBehaviour
{

    private ARRaycastManager rayManager;
    private GameObject visual;

    void Start()
    {
        if (Input.location.isEnabledByUser)
        {
            Input.location.Start();
        }

        // get the component
        rayManager = FindObjectOfType<ARRaycastManager>();
        visual = transform.GetChild(0).gameObject;

        // hide the placement visual

        visual.SetActive(false);

    }

    void Update()
    {
        //if(GetCurrentLocation().x > 47.167063443549914 && GetCurrentLocation().x < 47.16724126519584 && GetCurrentLocation().y > 27.543318048354156 && GetCurrentLocation().y < 27.54311071772677)
        // shoot a raycast from the center of the screen
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        rayManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.PlaneEstimated);

        // if we hit AR Plane, Update the postion and rotation

        if (hits.Count > 0)
        {
            transform.position = hits[0].pose.position;
            transform.rotation = hits[0].pose.rotation;

            if (!visual.activeInHierarchy)
                visual.SetActive(true);
        }
    }

    Vector2 GetCurrentLocation()
    {
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            print("Unable to determine device location");
            return new Vector2(0, 0);
        } else
        {
            return new Vector2(Input.location.lastData.latitude, Input.location.lastData.longitude);
        }
    }

}
