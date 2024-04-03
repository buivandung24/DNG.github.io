using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

public class Parallax : MonoBehaviour
{
    public float valueController;
    // public bool checkPlayer;
    private Transform cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.transform;
    }

    private void LateUpdate() {
        transform.position = new Vector3(cam.position.x, cam.position.y + valueController , transform.position.z);
    }
}
