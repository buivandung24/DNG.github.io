using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraFollow : MonoBehaviour
{
    public GameObject playerCamera;
    void Update()
    {
        transform.position = playerCamera.transform.position + new Vector3(0,1,-15);
    }
}
