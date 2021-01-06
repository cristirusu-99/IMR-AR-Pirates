namespace Mapbox.Examples
{
	using UnityEngine;
	using Mapbox.Utils;
	using Mapbox.Unity.Map;
	using Mapbox.Unity.MeshGeneration.Factories;
	using Mapbox.Unity.Utilities;
	using System.Collections.Generic;
    using UnityEngine.SceneManagement;
    using UnityEngine.UI;

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
		List<GameObject> _spawnedObjects;
		public Camera _referenceCamera;
		static int curentpos = 0;
		
		void Start()
		{
			_locations = new Vector2d[markers.Length];
			_spawnedObjects = new List<GameObject>();
			
		}

		private void Update()
		{
			
			
			if (Input.GetMouseButtonUp(1))
			{	
				if ( SpawnOnMap.curentpos != _spawnedObjects.Count)
					return ;
				else
				{
					var mousePosScreen = Input.mousePosition;
					//assign distance of camera to ground plane to z, otherwise ScreenToWorldPoint() will always return the position of the camera
					//http://answers.unity3d.com/answers/599100/view.html
					mousePosScreen.z = _referenceCamera.transform.localPosition.y;
					var pos = _referenceCamera.ScreenToWorldPoint(mousePosScreen);
					var latlongDelta = _map.WorldToGeoPosition(pos);
					_locations[SpawnOnMap.curentpos] = latlongDelta;
					//GameObject.Find("Coord_Riddle" + i.ToString() ).GetComponent<Text>().text=latlongDelta.x.ToString() + " " + latlongDelta.y.ToString();
					var instance = Instantiate(markers[SpawnOnMap.curentpos]);
					_spawnedObjects.Add(instance);
					SpawnOnMap.curentpos++;
					
				}

				SceneManager.LoadScene("Home");
				
			}
			int count = _spawnedObjects.Count;
			for (int i = 0; i < count; i++)
			{
				var spawnedObject = _spawnedObjects[i];
				var location = _locations[i];
				spawnedObject.transform.localPosition = _map.GeoToWorldPosition(location, true);
				spawnedObject.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
			}
		}
	}
}