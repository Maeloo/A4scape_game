using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MovementComponent : MonoBehaviour
{

    [HideInInspector]
    public float Speed;
    [HideInInspector]
    public float Direction;

    public bool AuthXMove = true;
    public bool AuthYMove = false;

    public Vector2 LastVelocity { get { return m_lastVelocity; } }
    private Vector2 m_lastVelocity;

    public float GetRelativeVelocity() { return Speed * Direction; }


    private void Start()
    {
        Direction = 1f;
    }

    public void ProcessMovement(Vector2 MovementDirection)
    {
        Vector2 oldVelocity = gameObject.GetComponent<Rigidbody2D>().velocity;

        Vector2 newVelocity = MovementDirection.normalized * Speed;
        newVelocity.x = AuthXMove ? newVelocity.x : oldVelocity.x;
        newVelocity.y = AuthYMove ? newVelocity.y : oldVelocity.y;

        m_lastVelocity = gameObject.GetComponent<Rigidbody2D>().velocity = newVelocity;
    }

    public void StopMovement()
    {
        m_lastVelocity = gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
	
}
