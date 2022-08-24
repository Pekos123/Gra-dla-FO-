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
        //zmienne kt�re b�d� nam potrzebne do ruszanai kamer�
        float mouseX = Input.GetAxis("Mouse X") * MouseSensivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * MouseSensivity * Time.deltaTime;

        // zmienna kt�ra pozwoli rusza� kamer�
        xRotation -= mouseY;

        // zeby kamera nie obraca�a sie azbardzo do g�ry lub w d�

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //obracanie kamera g�ra-d�
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        // obracanie kamer� lewo-prawo
        Player.Rotate(Vector3.up * mouseX);
    }
}
