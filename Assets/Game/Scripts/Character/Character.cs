using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Character : MonoBehaviour
{

    [SerializeField]
    protected GameObject StateMachinePrefab;

    [SerializeField]
    protected AudioSource _RunSound;
    public AudioSource RunSound { get { return _RunSound; } }

    public float LeftBorderDistance = 150f;
    public float RightBorderDistance = 150f;

    protected SpriteRenderer _Renderer;
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
    public SpriteRenderer Renderer { get { return _Renderer; } }

    [HideInInspector]
    public float relativeVelocity;

    protected InteractableCharacter _InteractableNPC;
    protected InteractableItem _InteractableItem;

    protected Dictionary<EItemType, bool> _Inventory;

    protected bool bInvincible;


    // Begin Play
    void Start()
    {
        _CharacterController = GetComponent<Controller>();
        _CharacterMovementComponent = GetComponent<MovementComponent>();
        _CharacterAnimator = GetComponentInChildren<Animator>();
        _ThrowComponent = GetComponent<ThrowComponent>();
        _Renderer = GetComponentInChildren<SpriteRenderer>();

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

    public bool bBlockLeftMovement = false;

    // Physic Tick
    private void FixedUpdate()
    {
        Vector3 characterScreenPosition = GameCore.Instance.GameCamera.WorldToScreenPoint(transform.position);
        if ((characterScreenPosition.x <= LeftBorderDistance && _CharacterMovementComponent.LastVelocity.x < 0f) ||
            (characterScreenPosition.x >= Screen.width - RightBorderDistance && _CharacterMovementComponent.LastVelocity.x > 0f) &&
            _CharacterStateMachine.CurrentState == EStateType.Run)
        {
            if(!(_CharacterMovementComponent.LastVelocity.x < 0f && bBlockLeftMovement))
            {
                ParallaxSystem.Instance.UpdateLayers(_CharacterMovementComponent.Direction);
            }
            
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
            int count;
            GameObject prefab;
            EItemType item = _InteractableItem.PickUp(out count, out prefab);

            if(!_Inventory.ContainsKey(item))
            {
                _Inventory.Add(item, true);

                if(item == EItemType.Beer)
                {
                    UIManager.Instance.DisplayBeerCount(true);
                    UIManager.Instance.ObjectiveText.text = DialogueData.Objective_4;
                    UIManager.Instance.DisplayPopup(true);
                }
            }            

            ThrowComponent.Reload(count, prefab);
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

    public void TryAction()
    {
        if (null != _InteractableNPC)
        {
            _InteractableNPC.Interact();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        InteractableCharacter npc = collision.GetComponent<InteractableCharacter>();
        if (null != npc)
        {
            _InteractableNPC = npc;
            _InteractableNPC.OnPlayerTriggered(true);
        }

        InteractableItem item = collision.GetComponent<InteractableItem>();
        if (null != item)
        {
            _InteractableItem = item;
            _InteractableItem.OnPlayerTriggered(true);
        }        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!bInvincible && collision.CompareTag("Meteor"))
        {
            //Debug.Log("Hit by a meteor");
            collision.GetComponentInParent<Meteor>().OnPlayerCollision((collision.transform.position + transform.position) * .5f);

            Vector3 newPosition = transform.position;
            newPosition.x -= 1f * _CharacterMovementComponent.Direction;

            _CharacterController.SetIgnoreInput(true);
            _CharacterController.SetIgnoreMove(true);

            bInvincible = true;
            _Renderer.DOFade(.7f, .3f).SetEase(Ease.InOutQuad).SetLoops(5).OnComplete(() =>
            {
                bInvincible = false;
                _Renderer.DOFade(1f, .0f);
            });

            transform.DOMoveX(newPosition.x, .5f).SetEase(Ease.OutQuad).OnComplete(() =>
            {
                _CharacterController.SetIgnoreInput(false);
                _CharacterController.SetIgnoreMove(false);
            });
        }
    }

    public void StopInterracting()
    {
        if (null != _InteractableNPC)
        {
            _InteractableNPC.StopInteracting();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        InteractableCharacter npc = collision.GetComponent<InteractableCharacter>();
        if (npc == _InteractableNPC && null != _InteractableNPC)
        {
            _InteractableNPC.StopInteracting();
            _InteractableNPC.OnPlayerTriggered(false);
            _InteractableNPC = null;
            return;
        }

        InteractableItem item = collision.GetComponent<InteractableItem>();
        if (item == _InteractableItem && null != _InteractableItem)
        {
            _InteractableItem.OnPlayerTriggered(false);
            _InteractableItem = null;
            return;
        }
    }

    public bool RequestState(EStateType NewState)
    {
        return _CharacterStateMachine.RequestChangeState(NewState);
    }

}
