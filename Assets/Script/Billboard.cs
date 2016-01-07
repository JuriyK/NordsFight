using UnityEngine;
using System.Collections;

public class Billboard : MonoBehaviour
{
    private GameObject cameraToLookAt;
    void Start()
    {
        cameraToLookAt = GameObject.Find("Camera");
    }

    void Update()
    {
        Vector3 v = cameraToLookAt.transform.position - transform.position;
        v.x = v.z = 0.0f;
        transform.LookAt(cameraToLookAt.transform.position - v);
    }
}