using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public float BaseHeight = -.5f;
    public float Amplitude = 2.5f;
    public float HorizontalSpeed = 2.1f;
    public float VerticalSpeed = 1.4f;

    protected float time;
	
    void Update ()
    {
        time += VerticalSpeed * Time.deltaTime;

        Vector3 newPosition = transform.position;
        newPosition.y = BaseHeight + Mathf.Sin(time) * Amplitude;
        newPosition.x -= HorizontalSpeed * Time.deltaTime;

        transform.position = newPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }
}
