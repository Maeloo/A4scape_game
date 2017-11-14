using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Camera))]
public class FollowCamera : MonoBehaviour
{

    public GameObject FollowAxisLocation;
    public float Speed = 1.0f;

    [HideInInspector]
    public float lastVelocity;

    protected Camera m_camComp;

    private void Start()
    {
        m_camComp = gameObject.GetComponent<Camera>();
    }

    void Update ()
    {
        float prevX = gameObject.transform.position.x;

        if (GameCore.Instance.Player != null)
        {
            Vector2 playerScreenPos = m_camComp.WorldToScreenPoint(GameCore.Instance.Player.transform.position);
            Vector2 triggerScreenPos = m_camComp.WorldToScreenPoint(FollowAxisLocation.transform.position);

            float deltaAxisX = triggerScreenPos.x - playerScreenPos.x;
            float realDeltaX = FollowAxisLocation.transform.position.x - GameCore.Instance.Player.transform.position.x;
            if (deltaAxisX < 0f)
            {
                Vector3 wantedCamPos = gameObject.transform.position;
                wantedCamPos.x -= realDeltaX;
                gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, wantedCamPos, Time.deltaTime * Speed);
            }
        }

        float newX = gameObject.transform.position.x;

        lastVelocity = newX - prevX;
    }
}
