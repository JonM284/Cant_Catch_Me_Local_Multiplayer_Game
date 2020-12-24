using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tester : MonoBehaviour
{

    public Transform tester_Spot, tester_Crosshair;
    public Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    private void LateUpdate()
    {
        Ray ray = cam.ScreenPointToRay(tester_Crosshair.GetComponent<Image>().rectTransform.position);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Debug.DrawRay(ray.origin, ray.direction, Color.red);
            if (hit.collider.tag == "Player")
            {
                Debug.Log($"Shooting Player at: {hit.point}");
            }
        }
        
        
    }
}
