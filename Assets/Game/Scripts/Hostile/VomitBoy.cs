using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VomitBoy : MonoBehaviour
{

    [SerializeField]
    protected AudioSource _Sound;
    protected float SoundCD = 0f;

    private void Update()
    {
        SoundCD -= Time.deltaTime;
        if (SoundCD <= 0f && Mathf.Abs(transform.position.x - GameCore.Instance.Player.transform.position.x) < .1f)
        {
            SoundCD = 15f;
            _Sound.Play();
        }
    }
}
