﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gojira : MonoBehaviour
{

    public float Speed = 1f;
    public float Amplitude = 10f;

    protected float time = 0f;
    protected float BaseHeight = 0f;

    void Start()
    {
        BaseHeight = transform.localPosition.y;
    }

    public void OnCameraMovement(float Offset)
    {
        time += Speed * Offset;

        Vector3 newPosition = transform.localPosition;
        newPosition.y = BaseHeight + Mathf.Sin(time) * Amplitude;

        transform.localPosition = newPosition;
    }

}