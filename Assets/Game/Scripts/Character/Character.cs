using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Character : MonoBehaviour
{

    [SerializeField]
    protected GameObject StateMachinePrefab;

    public float LeftBorderDistance = 150f;
    public float RightBorderDistance = 150f;

    protected StateMachine _CharacterStateMachine;
    protected Controller _CharacterController;
    protected Animator _CharacterAnimator;

    /* Components */
    protected MovementComponent _CharacterMovementComponent;
    protected ThrowComponent _ThrowComponent;

    /* Getters */
    public Animator CharacterAnimator { get { return _CharacterAnimator; } }
    public MovementComponent CharacterMovementComponent { get { return _CharacterMovementComponent; } }
    public ThrowComponent ThrowComponent { get { return _ThrowComponent; } }
    public Controller CharacterController { get { return _CharacterController; } }

    [HideInInspector]
    public float relativeVelocity;

    protected InteractableCharacter _InteractableNPC;
    protected InteractableItem _InteractableItem;

    protected Dictionary<EItemType, bool> _Inventory;


    // Begin Play
    void Start()
    {
        _CharacterController = GetComponent<Controller>();
        _CharacterMovementComponent = GetComponent<MovementComponent>();
        _CharacterAnimator = GetComponentInChildren<Animator>();
        _ThrowComponent = GetComponent<ThrowComponent>();

        _ThrowComponent.Initialise(this);

        _CharacterStateMachine = Instantiate<GameObject>(StateMachinePrefab).GetComponent<StateMachine>();
        _CharacterStateMachine.Init(this);

        _Inventory = new Dictionary<EItemType, bool>();
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
        if ((characterScreenPosition.x <= LeftBorderDistance && _CharacterMovementComponent.LastVelocity.x < 0f) ||
            (characterScreenPosition.x >= Screen.width - RightBorderDistance && _CharacterMovementComponent.LastVelocity.x > 0f))
        {
            ParallaxSystem.Instance.UpdateLayers(_CharacterMovementComponent.Direction);

            _CharacterMovementComponent.StopMovement();
        }
    }

    public Vector2 GetVelocity()
    {
        return _CharacterMovementComponent.LastVelocity;
    }

    public void TryInteract()
    {
        if (null != _InteractableNPC)
        {
            _InteractableNPC.StartInteracting();
        }
    }

    public void TryPickUp()
    {
        if (null != _InteractableItem)
        {
            _Inventory.Add(_InteractableItem.PickUp(), true);
        }
    }

    public void TryThrow()
    {
        if (_Inventory.ContainsKey(EItemType.Beer) && _Inventory[EItemType.Beer])
        {
            if (_ThrowComponent.StartThrow())
            {
                _CharacterStateMachine.RequestChangeState(EStateType.Throw);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _InteractableNPC = collision.GetComponent<InteractableCharacter>();
        _InteractableItem = collision.GetComponent<InteractableItem>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        InteractableCharacter npc = collision.GetComponent<InteractableCharacter>();
        if (npc == _InteractableNPC && null != _InteractableNPC)
        {
            _InteractableNPC.StopInteracting();
            _InteractableNPC = null;
            return;
        }

        InteractableItem item = collision.GetComponent<InteractableItem>();
        if (item == _InteractableItem && null != _InteractableItem)
        {
            _InteractableItem = null;
            return;
        }
    }

    public bool RequestState(EStateType NewState)
    {
        return _CharacterStateMachine.RequestChangeState(NewState);
    }

}
