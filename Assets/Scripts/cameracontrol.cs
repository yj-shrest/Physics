using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameracontrol : MonoBehaviour
{
    float RotationX = 0f;
    float speed = 2f;
    public Transform FPP;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        RotationX = Mathf.Clamp(RotationX, 0f, 40f);
        float MouseX = Input.GetAxis("Mouse X");
        float MouseY = Input.GetAxis("Mouse Y");
        RotationX -= MouseY * speed;
        transform.localRotation = Quaternion.Euler(RotationX, 0f, 0f);
        FPP.Rotate(Vector3.up * MouseX);
    }
}

