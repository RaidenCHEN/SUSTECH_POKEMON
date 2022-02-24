using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    Transform player;
    private float xRotation;
    private float yRotation;
    private float MouseSensitivity=500;
    

    void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {     
        

        float mouseX = MouseSensitivity*Time.deltaTime * Input.GetAxis("Mouse X");
        xRotation += mouseX;
        float mouseY = MouseSensitivity*Time.deltaTime * Input.GetAxis("Mouse Y");
        yRotation -= mouseY;
        yRotation = Mathf.Clamp(yRotation, -70, 70);


        player.Rotate(Vector3.up * mouseX);
        transform.localRotation = Quaternion.Euler(yRotation, 0, 0);
    }
}
