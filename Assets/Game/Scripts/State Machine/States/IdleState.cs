using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IdleState : BaseState
{

    protected float WantedSpeed;
    protected float Acceleration;

    public override EStateType GetStateType() { return EStateType.Idle; }

    public override void OnEnter()
    {
        OwnerCharacter.CharacterAnimator.SetTrigger("StateChangedToIdle");
        WantedSpeed = 0f;
        Acceleration = 4f;
    }

    protected void Update()
    {
        if (OwnerCharacter.CharacterMovementComponent.Speed < WantedSpeed)
        {
            OwnerCharacter.CharacterMovementComponent.Speed += Time.deltaTime * Acceleration;
        }
        else if (OwnerCharacter.CharacterMovementComponent.Speed > WantedSpeed)
        {
            OwnerCharacter.CharacterMovementComponent.Speed -= Time.deltaTime * Acceleration;
        }

        OwnerCharacter.CharacterMovementComponent.Speed = Mathf.Clamp(OwnerCharacter.CharacterMovementComponent.Speed, 0f, WantedSpeed);

        // HACK FIX
        if (GetStateType() == EStateType.Idle)
        {
            OwnerCharacter.CharacterAnimator.SetTrigger("StateChangedToIdle");
        }

        float forwardDir = Mathf.Sign(OwnerCharacter.CharacterAnimator.transform.localScale.x);
        float velocitydDir = Mathf.Sign(OwnerCharacter.CharacterMovementComponent.LastVelocity.x);

        if (OwnerCharacter.CharacterMovementComponent.LastVelocity.x != 0f && 
            forwardDir != velocitydDir)
        {
            Vector3 newScale = OwnerCharacter.CharacterAnimator.transform.localScale;
            newScale.x *= -1f;

            OwnerCharacter.CharacterAnimator.transform.localScale = newScale;
            OwnerCharacter.CharacterMovementComponent.Direction = Mathf.Sign(newScale.x);
        }
    }

    public override void HandleMovementInput(Vector2 PlayerInput)
    {
        if (Mathf.Abs(PlayerInput.x) > .8f)
        {
            OwnerStateMachine.ChangeState(EStateType.Run);
        }
        else if (Mathf.Abs(PlayerInput.x) > 0f)
        {
            OwnerStateMachine.ChangeState(EStateType.Walk);
        }
    }

}
