using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class Box : MonoBehaviour, IDamageable
{
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public bool ApplyDamage(Rigidbody attachedBody, float force)
    {
        Vector3 direction = (transform.position - attachedBody.position);
        direction.y = 0;
        _rigidbody.AddForce(direction.normalized * force, ForceMode.Impulse);
        return true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out IDamageable damageable))
        {
            damageable.ApplyDamage(_rigidbody, _rigidbody.velocity.magnitude);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            Destroy(gameObject);
        }
    }
}
