using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingState : EnemyState
{
    private void OnEnable()
    {
        Animator.SetTrigger("StandUp");
    }

    private void Update()
    {
        
    }
}
