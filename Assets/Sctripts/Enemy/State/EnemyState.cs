using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : BaseState
{
    public PlayerStateMachine Player { get; private set; }

    public override void Enter(Rigidbody rigidbody, Animator animator, PlayerStateMachine player)
    {
        if (enabled == false)
        {
            Rigidbody = rigidbody;
            Animator = animator;
            Player = player;

            enabled = true;

            foreach (var transition in _transitions)
            {
                transition.enabled = true;
                transition.Init(Player);
            }
        }
    }
}
