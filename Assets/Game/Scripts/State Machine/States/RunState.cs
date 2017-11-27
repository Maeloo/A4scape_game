using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RunState : IdleState
{
    public float RunSpeed = 3.5f;

    public override EStateType GetStateType() { return EStateType.Run; }

    public override void OnEnter()
    {
        OwnerCharacter.CharacterAnimator.SetTrigger("StateChangedToRun");
        OwnerCharacter.RunSound.Play();

        WantedSpeed = RunSpeed;
        Acceleration = 4f;
    }

    public override void HandleMovementInput(Vector2 PlayerInput)
    {
        if (Mathf.Abs(PlayerInput.x) == 0f)
        {
            OwnerStateMachine.ChangeState(EStateType.Idle);
        } else if (Mathf.Abs(PlayerInput.x) < .8f)
        {
            OwnerStateMachine.ChangeState(EStateType.Walk);
        }
    }

}
