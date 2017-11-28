using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Meteor : MonoBehaviour
{

    [SerializeField]
    protected GameObject Marker;
    [SerializeField]
    protected GameObject MeteorObj;
    [SerializeField]
    protected GameObject PrefabExplosion;

    public float Speed;

    protected bool bFalling = false;

    public Vector3 _savedPosition;


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

    public void StopFall()
    {
        bFalling = false;
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
            if (distance < .1f)
            {
                Crash();
            }
        }        
	}

    void Crash()
    {
        MeteorObj.GetComponent<BoxCollider2D>().enabled = false;
        MeteorObj.GetComponent<SpriteRenderer>().DOFade(0f, .5f);

        Marker.GetComponent<Animator>().SetTrigger("Crash");
    }

    public void OnPlayerCollision(Vector3 ImpactPosition)
    {
        Instantiate(PrefabExplosion, ImpactPosition, Quaternion.identity, transform);

        MeteorObj.GetComponent<SpriteRenderer>().DOFade(0f, .15f);
        Marker.SetActive(false);
    }

}
