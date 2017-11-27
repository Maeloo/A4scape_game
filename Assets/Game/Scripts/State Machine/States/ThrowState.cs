using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ThrowState : BaseState
{

    public override EStateType GetStateType() { return EStateType.Throw; }

    public override void OnEnter()
    {
        OwnerCharacter.RunSound.Stop();
        OwnerCharacter.CharacterAnimator.SetTrigger("StateChangedToThrow");
        OwnerCharacter.CharacterController.SetIgnoreMove(true);
    }

}
