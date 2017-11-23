using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{

    [SerializeField]
    protected GameObject Marker;
    [SerializeField]
    protected GameObject MeteorObj;

    public float Speed;

    protected bool bFalling = false;


    private void Start()
    {
        TriggerFall();
    }

    public void TriggerFall()
    {
        Marker.SetActive(true);
        MeteorObj.SetActive(true);

        bFalling = true;
    }

    void Update ()
    {
        if (bFalling)
        {
            Vector3 direction = Marker.transform.position - MeteorObj.transform.position;
            direction.Normalize();

            Vector3 newPosition = MeteorObj.transform.position;
            newPosition += direction * Speed * Time.deltaTime;

            MeteorObj.transform.position = newPosition;

            float distance = Vector3.Distance(Marker.transform.position, newPosition);
            if (distance < .01f)
            {
                Destroy(gameObject);
            }
        }        
	}

}
