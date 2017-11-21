using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public float BaseHeight = -.5f;
    public float Amplitude = 2.5f;
    public float HorizontalSpeed = 2.1f;
    public float VerticalSpeed = 1.4f;
    public float TimeBeforeDestruction = 5f;

    protected float time = 0f;
    protected float offScreenTime = 0f;

    bool bAlive = true;
	
    void Update ()
    {
        if (bAlive)
        {
            time += VerticalSpeed * Time.deltaTime;

            Vector3 newPosition = transform.position;
            newPosition.y = BaseHeight + Mathf.Sin(time) * Amplitude;
            newPosition.x -= HorizontalSpeed * Time.deltaTime;

            transform.position = newPosition;
        }       

        if (GetComponent<SpriteRenderer>().isVisible)
        {
            offScreenTime = 0f;
        }
        else
        {
            offScreenTime += Time.deltaTime;
            if (offScreenTime >= TimeBeforeDestruction)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            bAlive = false;

            Vector3 impactDirection = transform.position - collision.transform.position;
            gameObject.AddComponent<Rigidbody2D>().AddForce(impactDirection * 100f);

            GameCore.Instance.OnBirdKilled();
        }        
    }
}
