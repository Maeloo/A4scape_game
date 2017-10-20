using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Controller : MonoBehaviour
{

    public Vector2 LastInput { get { return m_lastInput; } }
    private Vector2 m_lastInput;

    void Update()
    {
        m_lastInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

}
