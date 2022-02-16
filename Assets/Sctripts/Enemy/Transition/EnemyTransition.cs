using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyTransition : BaseTransition
{
    protected PlayerStateMachine Player { get; private set; }

    public override void Init(PlayerStateMachine player)
    {
        Player = player;
    }
}
