using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 offset;

    private void Update()
    {
        transform.LookAt(player);
        transform.position = Vector3.Lerp(transform.position, player.position + offset, Time.deltaTime * 5f);
    }
}
