using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BaseState : MonoBehaviour
{

    protected Character OwnerCharacter;
    protected StateMachine OwnerStateMachine;

    public void Init(StateMachine MachineToSet, Character OwnerToSet)
    {
        OwnerStateMachine = MachineToSet;
        OwnerCharacter = OwnerToSet;
    }

    public virtual EStateType GetStateType() { return EStateType.None; }

    public virtual bool CanEnter(EStateType PreviousState) { return true; }

    public virtual void OnEnter() { }
    public virtual void OnExit(EStateType NewState) { }

    public virtual void HandleMovementInput(Vector2 PlayerInput) { }

}
