using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody),typeof(Animator), typeof(HealthContainer))]

public class PlayerStateMachine : BaseStateMachine
{
    private HealthContainer _health;

    public event UnityAction Damaged;

    private void OnEnable()
    {
        _health.Died += OnDied;
    }

    private void OnDisable()
    {
        _health.Died -= OnDied;
    }

    private void OnDied()
    {
        Animator.SetTrigger("Death");
        Animator.SetBool("IsDead", true);
        enabled = false;
    }

    protected override void Awake()
    {
        base.Awake();
        _health = GetComponent<HealthContainer>();
    }

    public void ApplyDamage(float damage)
    {
        Damaged?.Invoke();
        _health.TakeDamage((int)damage);
        Animator.SetTrigger("GetDamaged");
    }
}
