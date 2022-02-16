using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseTransition : MonoBehaviour
{
    [SerializeField] private BaseState _targetState;

    public BaseState TargetState => _targetState;

    public bool NeedTransit { get; protected set; }

    protected virtual void OnEnable()
    {
        NeedTransit = false;
    }

    public abstract void Init(PlayerStateMachine player);
}
