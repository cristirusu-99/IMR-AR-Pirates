namespace Mapbox.Examples
{
    using UnityEngine;
    using Mapbox.Utils;
    using Mapbox.Unity.Map;
    using Mapbox.Unity.Utilities;
    using System.Collections.Generic;
    using UnityEngine.SceneManagement;


    public class SpawnOnMap : MonoBehaviour
    {
        [SerializeField]
        AbstractMap _map;

        [SerializeField]
        [Geocode]
        string[] _locationStrings;
        Vector2d[] _locations;

        [SerializeField]
        float _spawnScale = 100f;

        [SerializeField]
        GameObject[] markers;
        GameObject[] _spawnedObjects;
        public Camera _referenceCamera;

        void Start()
        {
            _locations = new Vector2d[markers.Length];
            _spawnedObjects = new GameObject[6];
            GameObject instance;
            for (int i = 0, j = 0; j < ScenesData.numberOfRiddles; i += 2, j++)
            {
                if(ScenesData.riddlesCoords[i] != 0)
                {
                    instance = Instantiate(markers[j]);
                    _locations[j] = new Vector2d(ScenesData.riddlesCoords[i], ScenesData.riddlesCoords[i + 1]);
                    _spawnedObjects[j] = instance;
                    
                }
            }

            if (ScenesData.treasureCoords[0] != 0)
            {
                 instance = Instantiate(markers[5]);
                _locations[5] = new Vector2d(ScenesData.treasureCoords[0], ScenesData.treasureCoords[1]);
                _spawnedObjects[5] = instance;
                
            }
        }

        private void Update()
        {
            
            if (Input.GetMouseButtonUp(1))
            {
                var mousePosScreen = Input.mousePosition;
                //assign distance of camera to ground plane to z, otherwise ScreenToWorldPoint() will always return the position of the camera
                //http://answers.unity3d.com/answers/599100/view.html
                mousePosScreen.z = _referenceCamera.transform.localPosition.y;
                var pos = _referenceCamera.ScreenToWorldPoint(mousePosScreen);
                var latlongDelta = _map.WorldToGeoPosition(pos);
                GameObject instance;
                if (ScenesData.treasForT == true)
                {
                    _locations[5] = latlongDelta;
                    ScenesData.AddNewTreasureCord(latlongDelta);
                    if(_spawnedObjects[5] == null)
                    {
                        instance = Instantiate(markers[5]);
                        _spawnedObjects[5] = instance;
                    }
                    
                   

                }
                else
                {
                    _locations[ScenesData.currentRiddle - 1] = latlongDelta;
                    ScenesData.AddNewRiddleCoords(latlongDelta);
                    if(_spawnedObjects[ScenesData.currentRiddle - 1] == null)
                    {
                        instance = Instantiate(markers[ScenesData.currentRiddle - 1]);
                        _spawnedObjects[ScenesData.currentRiddle - 1] = instance;
                    }  
                }
                if (ScenesData.treasForT == true)
                    SceneManager.LoadScene("Treasure");
                else
                    SceneManager.LoadScene("Riddle");
            }
            int count = _spawnedObjects.Length;
            for (int i = 0; i < count; i++)
            {
                if(_spawnedObjects[i] != null)
                {
                    var spawnedObject = _spawnedObjects[i];
                    var location = _locations[i];
                    spawnedObject.transform.localPosition = _map.GeoToWorldPosition(location, true);
                    spawnedObject.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
                }  
            }

        }
    }
}