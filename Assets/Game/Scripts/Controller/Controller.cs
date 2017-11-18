using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Controller : MonoBehaviour
{

    protected int _IgnoreMoves;
    public bool IsMoveIgnored { get { return _IgnoreMoves > 0; } }

    protected Character OwnerCharacter;

    public Vector2 LastInput { get { return m_lastInput; } }
    private Vector2 m_lastInput;

    void Start()
    {
        OwnerCharacter = GetComponent<Character>();
    }

    void Update()
    {
        m_lastInput = IsMoveIgnored ? Vector2.zero : new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Interact_Pressed();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            PickUp_Pressed();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Action_Pressed();
        }
    }

    public void SetIgnoreMove(bool bIgnore)
    {
        if (bIgnore)
        {
            _IgnoreMoves++;
        }
        else
        {
            _IgnoreMoves--;
            _IgnoreMoves = Mathf.Max(0, _IgnoreMoves);
        }
    }

    public void ResetIgnoreMove()
    {
        _IgnoreMoves = 0;
    }

    void Interact_Pressed()
    {
        OwnerCharacter.TryInteract();
    }

    void PickUp_Pressed()
    {
        OwnerCharacter.TryPickUp();
    }

    void Action_Pressed()
    {
        OwnerCharacter.TryThrow();
    }

}
