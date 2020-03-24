using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveToObj : Action
{
    private NavMeshAgent _navAgent;
    private Animator _animator;

    public SharedTransform _targetObj;

    public override void OnAwake()
    {
        _navAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    public override void OnStart()
    {
        if (Util.isInRange(transform.position, _targetObj.Value.position, 1f) == false)
        {
            _navAgent.SetDestination(_targetObj.Value.position);
            _navAgent.isStopped = false;
            _animator.SetBool("isWalking", true);
        }
    }

    public override TaskStatus OnUpdate()
    {
        if (Util.isInRange(transform.position, _targetObj.Value.position, 1f))
        {
            _navAgent.isStopped = true;
            _animator.SetBool("isWalking", false);
            return TaskStatus.Success;
        }

        return TaskStatus.Running;
    }
}
