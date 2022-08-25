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


    // jezli wchodzi w objekt to nie wpinasz sie podrabinie
    private void OnTriggerEnter(Collider col)
    {

        if (col.tag == "EndOfTube")
            isOnEndOfTube = true;
    }

    // zeruje wartoœæ
    private void OnTriggerExit(Collider col)
    {

        if (col.tag == "EndOfTube")
            isOnEndOfTube = false;
    }

    void Start()
    {
        player = GetComponent<PlayerMovment>();
        text = TextCanvas.transform.GetComponent<Text>();

        // ustawia text "click [e] to use" na niewidzialnyu

        TextCanvas.SetActive(false);
    }

    //wspina sie
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
                            //wspina sie do gory
                            player.transform.position += Vector3.up / player.speed;
                        break;
                    case "s":
                        if(!player.groundedPlayer)
                            //wpsina sie w dó³
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
        // bierze sirodek ekaranu 
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Transform objectHit = hit.transform;
            // sprawdza czy obkiet na który nakieruje jest obiekem o tagu #tube i znajduje siê w zasiengu
            if (objectHit.tag == "tube" && hit.distance <= distance)
            {
                // robie napis
                text.text = "Click [e] to use";
                TextCanvas.SetActive(true);
                // wylaczam celownik 
                CrossHair.SetActive(false);
                if(Input.GetKeyDown(KeyCode.E))
                {
                    // wspina sie 
                    StartCoroutine(climb());
                }
            }
            else
            {
                // wylacza napis i wcaza celownik
                TextCanvas.SetActive(false);
                CrossHair.SetActive(true);
            }
        }
    }
}
