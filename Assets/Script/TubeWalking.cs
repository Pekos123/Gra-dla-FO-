using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.UI;

public class TubeWalking : MonoBehaviour
{
    PlayerMovment player;

    public GameObject CrossHair;
    public GameObject TextCanvas;
    Text text;
    public Camera camera;

    public bool IsOnTube;
    public float distance = 10f;

    public bool isOnEndOfTube = false;


    private void OnTriggerEnter(Collider col)
    {
        Debug.Log(col.tag);

        if (col.tag == "EndOfTube")
            isOnEndOfTube = true;
    }
    private void OnTriggerExit(Collider col)
    {
        Debug.Log(col.tag);

        if (col.tag == "EndOfTube")
            isOnEndOfTube = false;
    }

    void Start()
    {
        player = GetComponent<PlayerMovment>();
        text = TextCanvas.transform.GetComponent<Text>();
        TextCanvas.SetActive(false);
    }

    IEnumerator climb()
    {
        player.rb.isKinematic = true;
        IsOnTube = true;
        while (!Input.GetKeyUp(KeyCode.E) && !isOnEndOfTube)
        {

            switch (Input.inputString)
               {

                    case "w":
                        if(!isOnEndOfTube)
                            Debug.Log("w");
                            player.transform.position += Vector3.up / player.speed;
                        break;
                    case "s":
                        if(!player.groundedPlayer)
                            Debug.Log("s");
                            player.transform.position += Vector3.down / player.speed;
                        break;
                }

            yield return null;
        }

        player.rb.isKinematic = false;
        IsOnTube = false;
    }
    void Update()
    {
        
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            Transform objectHit = hit.transform;
            if (objectHit.tag == "tube" && hit.distance <= distance)
            {
                text.text = "Click [e] to use";
                TextCanvas.SetActive(true);
                CrossHair.SetActive(false);
                if(Input.GetKeyDown(KeyCode.E))
                {
                    StartCoroutine(climb());
                }
            }
            else
            {
                TextCanvas.SetActive(false);
                CrossHair.SetActive(true);
            }
        }
    }
}
