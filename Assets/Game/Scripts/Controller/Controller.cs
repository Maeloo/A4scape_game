using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Controller : MonoBehaviour
{

    protected Character OwnerCharacter;

    public Vector2 LastInput { get { return m_lastInput; } }
    private Vector2 m_lastInput;

    void Start()
    {
        OwnerCharacter = GetComponent<Character>();
    }

    void Update()
    {
        m_lastInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Up_Pressed();
        }
    }


    void Up_Pressed()
    {
        OwnerCharacter.TryInteract();
    }

}
