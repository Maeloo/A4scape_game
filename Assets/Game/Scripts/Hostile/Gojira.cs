using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gojira : MonoBehaviour
{
    [SerializeField]
    protected AudioSource _Sound;
    protected float SoundCD = 0f;

    public float Speed = 1f;
    public float Amplitude = 10f;

    protected float time = 0f;
    protected float BaseHeight = 0f;

    void Start()
    {
        BaseHeight = transform.localPosition.y;

        Cursor.visible = false;
    }

    public void OnCameraMovement(float Offset)
    {
        time += Speed * Offset;

        Vector3 newPosition = transform.localPosition;
        newPosition.y = BaseHeight + Mathf.Sin(time) * Amplitude;

        transform.localPosition = newPosition;

        SoundCD -= Time.deltaTime;
        if (SoundCD <= 0f && Mathf.Abs(transform.position.x - GameCore.Instance.Player.transform.position.x) < 5f)
        {
            SoundCD = 5f;
            Shout();
            Invoke("Shout", 2f);
        }
    }

    public void Shout()
    {
        _Sound.Play();
    }

}
