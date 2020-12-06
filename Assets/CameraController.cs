using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;

    private Vector3 _offset;


    public float minX, maxX;

    private void Start()
    {
        if (player != null)
        {
            _offset = transform.position - player.position;
        }
        else
        {
            Debug.LogError("Player Reference Not Found");
        }
    }

    private void LateUpdate()
    {
        if (player != null)
        {
            Vector3 camPos= player.position + _offset;
            camPos.y = 0;
            camPos.x = Mathf.Clamp(camPos.x, minX, maxX);
            transform.position = camPos;
        }
    }
}