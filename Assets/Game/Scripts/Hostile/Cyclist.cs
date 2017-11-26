using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cyclist : MonoBehaviour
{

    public float Speed;

    private void Update()
    {
        Vector3 newPosition = transform.position;
        newPosition.x -= Speed * Time.deltaTime;
        transform.position = newPosition;

        if (transform.position.x < -10f)
        {
            Destroy(gameObject);
        }
    }

}
