using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BrokenState : EnemyState
{
    [SerializeField] private float _fallDistance;

    public void ApplyDamage(Rigidbody attachedBody, float force)
    {
        Animator.SetTrigger("Death");
        Vector3 direction = (transform.position - attachedBody.position);
        direction.y = 0;
        Rigidbody.AddForce(direction.normalized * force, ForceMode.Impulse);
        StartCoroutine(DeathCorutine());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (enabled == false)
        {
            return;
        }
        if (collision.gameObject.TryGetComponent(out IDamageable damageable))
        {
            damageable.ApplyDamage(Rigidbody, Rigidbody.velocity.magnitude);
        }
    }

    private IEnumerator DeathCorutine()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
