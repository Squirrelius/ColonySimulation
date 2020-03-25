using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveToPos : Action
{
    private NavMeshAgent _navAgent;
    private Animator _animator;

    public SharedVector3 _targetPos;

    public override void OnAwake()
    {
        _navAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    public override void OnStart()
    {
        if (Util.isInRange(transform.position, _targetPos.Value, 1f) == false)
        {
            _navAgent.SetDestination(_targetPos.Value);
            _navAgent.isStopped = false;
            _animator.SetBool("isWalking", true);
        }
    }

    public override TaskStatus OnUpdate()
    {
        if (Util.isInRange(transform.position, _targetPos.Value, 1f))
        {
            _navAgent.isStopped = true;
            _animator.SetBool("isWalking", false);
            return TaskStatus.Success;
        }

        return TaskStatus.Running;
    }
}
