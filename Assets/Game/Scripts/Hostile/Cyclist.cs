using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cyclist : MonoBehaviour
{

    [SerializeField]
    protected AudioSource _Sound;
    protected bool bPlayed = false;

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

        if (!bPlayed && Vector3.Distance(transform.position, GameCore.Instance.Player.transform.position) < 1.1f)
        {
            _Sound.Play();

            bPlayed = true;
        }
    }

}
