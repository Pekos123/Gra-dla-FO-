using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    public bool isPlayerDashing = false;
    float dashTime = 0.5f;
    float dashSpeed = 30f;

    public int MaxDash = 1;
    int DashCount = 0;

    PlayerMovment player;

    private void Start()
    {
        player = transform.GetComponent<PlayerMovment>();
    }

    public IEnumerator DashCorutine()
    {
        float startTime = Time.time;
        isPlayerDashing = true;
        while (Time.time < startTime + dashTime)
        {
            Vector3 moveDirection = transform.forward * dashSpeed;
            player.controller.Move(moveDirection * Time.deltaTime);
            yield return null;
        }
        isPlayerDashing = false;
    }

    //funkcja ktora sprawdza czy moze dashowac
    bool CanDash()
    {
        if (!player.groundedPlayer && DashCount < MaxDash)
            return true;
        return false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && CanDash())
        {
            StartCoroutine(DashCorutine());
            DashCount += 1;
        }
        // jezli jest na podlodze i to mu sie zeruja
        if (player.groundedPlayer)
            DashCount = 0;
    }

}
