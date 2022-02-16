using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody),typeof(Animator))]

public class EnemyStateMachine : BaseStateMachine, IDamageable
{
    [SerializeField] private BrokenState _brokenState;
    [SerializeField] private HealthContainer _healthContainer;

    private float _minDamage = 5;

    public PlayerStateMachine Player { get; private set; }

    public event UnityAction<EnemyStateMachine> Died;

    private void OnEnable()
    {
        _healthContainer.Died += OnEnemyDie;
    }

    private void OnDisable()
    {
        _healthContainer.Died -= OnEnemyDie;
    }
    private void OnEnemyDie()
    {
        enabled = false;
        Rigidbody.constraints = RigidbodyConstraints.None;
        Died?.Invoke(this);
        Debug.Log("Enemy Died");
    }

    protected override void Awake()
    {
        base.Awake();
        Player = FindObjectOfType<PlayerStateMachine>();
    }

    protected override void Start()
    {
        CurrentState = _firstState;
        CurrentState.Enter(Rigidbody, Animator, Player);
    }

    protected override void Transit(BaseState nextState)
    {
        if (CurrentState != null)
        {
            CurrentState.Exit();
        }

        CurrentState = nextState;

        if (CurrentState != null)
        {
            CurrentState.Enter(Rigidbody, Animator, Player);
        }
    }

    public bool ApplyDamage(Rigidbody rigidbody, float force)
    {
        if (force > _minDamage && CurrentState != _brokenState)
        {
            _healthContainer.TakeDamage((int)force);
            Transit(_brokenState);
            _brokenState.ApplyDamage(rigidbody, force);
            return true;
        }
        return false;
    }
}
