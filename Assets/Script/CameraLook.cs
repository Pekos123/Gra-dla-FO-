using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    public float MouseSensivity = 50f;

    [SerializeField]
    private Transform Player;

    private float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    
    void Update()
    {
        //zmienne które bêd¹ nam potrzebne do ruszanai kamer¹
        float mouseX = Input.GetAxis("Mouse X") * MouseSensivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * MouseSensivity * Time.deltaTime;

        // zmienna która pozwoli ruszaæ kamer¹
        xRotation -= mouseY;

        // zeby kamera nie obraca³a sie azbardzo do góry lub w dó³

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //obracanie kamera góra-dó³
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        // obracanie kamer¹ lewo-prawo
        Player.Rotate(Vector3.up * mouseX);
    }
}
