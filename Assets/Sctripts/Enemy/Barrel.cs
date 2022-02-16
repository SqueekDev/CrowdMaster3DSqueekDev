using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Barrel : MonoBehaviour, IDamageable
{
    [SerializeField] private ParticleSystem _explosionEffect;
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;
    [SerializeField] private float _enemyExplodeForce;

    private Rigidbody _rigidbody;
    private MeshRenderer _meshRenderer;
    private CapsuleCollider _capsuleCollider;
    private float _forceForExplode = 5;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _capsuleCollider = GetComponent<CapsuleCollider>();
    }

    public bool ApplyDamage(Rigidbody attachedBody, float force)
    {
        if (force > _forceForExplode)
        {
            Explode();
        }
        return true;
    }

    private void Explode()
    {
        Scatter();
        _meshRenderer.enabled = false;
        _capsuleCollider.enabled = false;
        _explosionEffect.Play();
        Invoke("DestroyPS", 1f);
    }

    private void Scatter()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _explosionRadius);

        foreach (var nearbyObject in colliders)
        {
            EnemyStateMachine enemy = nearbyObject.GetComponent<EnemyStateMachine>();
            if (enemy != null)
            {
                enemy.ApplyDamage(_rigidbody, _enemyExplodeForce);
            }

            Rigidbody rigidbody = nearbyObject.GetComponent<Rigidbody>();
            if (rigidbody != null)
            {
                rigidbody.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
            }
        }
    }

    private void DestroyPS()
    {
        Destroy(this.gameObject);
    }
}
