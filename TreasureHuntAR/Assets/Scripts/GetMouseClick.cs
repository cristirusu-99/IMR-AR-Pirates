using UnityEngine;
using System.Collections;

public class GetMouseClick : MonoBehaviour
{
    void Update()
    {
        Vector3 position = new Vector3(); 
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            transform.position = hit.point;
            Debug.Log("The ray hit at: " + hit.point);
            Debug.Log("Transform position: " + transform.position);
        }
    }
}