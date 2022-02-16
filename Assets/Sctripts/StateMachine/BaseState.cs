using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseState : MonoBehaviour
{
    [SerializeField] protected BaseTransition[] _transitions;

    public Rigidbody Rigidbody { get; protected set; }
    public Animator Animator { get; protected set; }

    public virtual void Enter(Rigidbody rigidbody, Animator animator, PlayerStateMachine player = null)
    {
        if (enabled == false)
        {
            Rigidbody = rigidbody;
            Animator = animator;

            enabled = true;

            foreach (var transition in _transitions)
            {
                transition.enabled = true;
            }
        }
    }

    public void Exit()
    {
        if (enabled == true)
        {
            foreach (var transition in _transitions)
            {
                transition.enabled = false;
            }
            enabled = false;
        }
    }

    public BaseState GetNextState()
    {
        foreach (var transition in _transitions)
        {
            if (transition.NeedTransit)
            {
                return transition.TargetState;
            }
        }

        return null;
    }
}
