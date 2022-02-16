using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerTransition : BaseTransition
{
    protected override void OnEnable()
    {
        base.OnEnable();
        Enable();
    }

    public abstract void Enable();

    public override void Init(PlayerStateMachine player)
    {       
    }
}
