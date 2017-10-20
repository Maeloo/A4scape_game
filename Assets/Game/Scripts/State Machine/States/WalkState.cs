using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WalkState : IdleState
{

    public float WalkSpeed = 2f;

    public override EStateType GetStateType() { return EStateType.Walk; }

    public override void OnEnter()
    {
        OwnerCharacter.CharacterAnimator.SetTrigger("StateChangedToWalk");
        OwnerCharacter.CharacterMovementComponent.Speed = WalkSpeed;
    }

    public override void HandleMovementInput(Vector2 PlayerInput)
    {
        if (Mathf.Abs(PlayerInput.x) > .8f)
        {
            OwnerStateMachine.ChangeState(EStateType.Run);
        }
        else if (Mathf.Abs(PlayerInput.x) == 0f)
        {
            OwnerStateMachine.ChangeState(EStateType.Idle);
        }
    }

}
