using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Animator))]
public abstract class BaseStateMachine : MonoBehaviour
{
    [SerializeField] protected BaseState _firstState;

    private BaseState _currentState;
    private Rigidbody _rigidbody;
    private Animator _animator;

    public BaseState CurrentState { get; protected set; }
    public Rigidbody Rigidbody { get; protected set; }
    public Animator Animator { get; protected set; }

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        CurrentState = _currentState;
        Rigidbody = _rigidbody;
        Animator = _animator;
    }

    protected virtual void Start()
    {
        CurrentState = _firstState;
        CurrentState.Enter(Rigidbody, Animator);
    }

    protected void Update()
    {
        if (CurrentState == null)
            return;

        BaseState nextState = CurrentState.GetNextState();
        if (nextState != null)
        {
            Transit(nextState);
        }
    }

    protected virtual void Transit(BaseState nextState)
    {
        if (CurrentState != null)
        {
            CurrentState.Exit();
        }

        CurrentState = nextState;

        if (CurrentState != null)
        {
            CurrentState.Enter(Rigidbody, Animator);
        }
    }
}
