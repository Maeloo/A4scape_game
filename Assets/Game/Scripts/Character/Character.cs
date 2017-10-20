using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Character : MonoBehaviour
{

    [SerializeField]
    protected GameObject StateMachinePrefab;

    public float MinLeftBorderDistance = 30f;

    protected StateMachine _CharacterStateMachine;
    protected Controller _CharacterController;
    protected Animator _CharacterAnimator;

    /* Components */
    protected MovementComponent _CharacterMovementComponent;

    /* Getters */
    public Animator CharacterAnimator { get { return _CharacterAnimator; } }
    public MovementComponent CharacterMovementComponent { get { return _CharacterMovementComponent; } }


    // Begin Play
    void Start()
    {
        _CharacterController = GetComponent<Controller>();
        _CharacterMovementComponent = GetComponent<MovementComponent>();
        _CharacterAnimator = GetComponentInChildren<Animator>();

        _CharacterStateMachine = Instantiate<GameObject>(StateMachinePrefab).GetComponent<StateMachine>();
        _CharacterStateMachine.Init(this);
    }

    public void HandleMovementInput(Vector2 Input)
    {
        // TODO: Pre-process movement input by state
        _CharacterMovementComponent.ProcessMovement(Input);
        _CharacterStateMachine.HandleMovementInput(Input);
    }

    // Tick
    void Update()
    {
        HandleMovementInput(_CharacterController.LastInput);
    }

    // Physic Tick
    private void FixedUpdate()
    {
        Vector3 characterScreenPosition = GameCore.Instance.GameCamera.WorldToScreenPoint(transform.position);
        if (characterScreenPosition.x < MinLeftBorderDistance && _CharacterMovementComponent.LastVelocity.x < 0f)
        {
            _CharacterMovementComponent.StopMovement();
        }
    }

    public Vector2 GetVelocity()
    {
        return _CharacterMovementComponent.LastVelocity;
    }

}
