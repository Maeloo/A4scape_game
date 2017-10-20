using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TurnState : BaseState
{

    public override EStateType GetStateType() { return EStateType.Turn; }

    public override void OnEnter()
    {
        OwnerCharacter.CharacterAnimator.SetTrigger("StateChangedToTurn");
    }

}
