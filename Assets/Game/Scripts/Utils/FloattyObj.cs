using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloattyObj : MonoBehaviour
{

    public float Amplitude;
    public float Speed;

    protected float baseY;

    protected float time = 0f;

    
    void Start ()
    {
	    baseY = transform.position.y;	
	}
	
	void Update ()
    {
        time += Speed * Time.deltaTime;

        Vector3 newPosition = transform.position;
        newPosition.y = baseY + Mathf.Sin(time) * Amplitude;

        transform.position = newPosition;

        if (time > Mathf.Infinity - 1f)
        {
            time = 0f;
        }
    }
}
