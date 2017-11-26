using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Controller : MonoBehaviour
{

    protected int _IgnoreMoves;
    public bool IsMoveIgnored { get { return _IgnoreMoves > 0; } }

    protected int _IgnoreInput;
    public bool IgnoreInput { get { return _IgnoreInput > 0; } }

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

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetButtonDown("Y")) // Y
        {
            Interact_Pressed();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetButtonDown("B")) // B
        {
            PickUp_Pressed();
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("X"))  // X
        {
            Attack_Pressed();
        }

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("A")) // A
        {
            Action_Pressed();
        }

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Start")) // Start
        {
            GameCore.Instance.TogglePause();
        }

        if ((Input.GetKeyDown(KeyCode.Delete) || Input.GetButtonDown("Select")) && GameCore.Instance.GamePaused) // Select
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(0);
        }
    }

    public void SetIgnoreInput(bool bIgnore)
    {
        if (bIgnore)
        {
            _IgnoreInput++;
        }
        else
        {
            _IgnoreInput--;
            _IgnoreInput = Mathf.Max(0, _IgnoreMoves);
        }
    }

    public void ResetIgnoreInput()
    {
        _IgnoreInput = 0;
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

    void Attack_Pressed()
    {
        OwnerCharacter.TryThrow();
    }

    void Action_Pressed()
    {
        OwnerCharacter.TryAction();   
    }

}
