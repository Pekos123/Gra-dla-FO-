using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    public bool isPlayerDashing = false;
    float dashTime = 2;
    float dashSpeed = 10f;

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

}
