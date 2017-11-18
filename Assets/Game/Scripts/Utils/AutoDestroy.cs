using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{

    public bool bDestroyUnderPosition_Y;
    public float Position_Y;

    void Update ()
    {
        if (bDestroyUnderPosition_Y && transform.position.y < Position_Y)
        {
            Destroy(gameObject);
        }
	}
}
