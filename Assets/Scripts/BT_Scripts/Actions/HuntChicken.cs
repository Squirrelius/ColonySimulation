using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[TaskCategory("Villager")]
public class HuntChicken : Action
{
    public SharedTransform _chickenTransform;

    private Villager _villager;
    private NavMeshAgent _navAgent;
    private Animator _animator;

    public override void OnAwake()
    {
        _villager = GetComponent<Villager>();
        _navAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    public override void OnStart()
    {
        _navAgent.SetDestination(_chickenTransform.Value.position);
        _navAgent.isStopped = false;
        _animator.SetBool("isWalking", true);
    }

    public override TaskStatus OnUpdate()
    {
        if (_chickenTransform.Value == null)//Chicken died before I could catch it.
            return TaskStatus.Failure;

        _navAgent.SetDestination(_chickenTransform.Value.position);

        if (Util.isInRange(transform.position, _chickenTransform.Value.position, 1f))
        {
            _navAgent.isStopped = true;
            _animator.SetBool("isWalking", false);

            _villager._CarriedItem = ItemTypes.Meat;
            GameObject.Destroy(_chickenTransform.Value.gameObject);
            return TaskStatus.Success;
        }
        else
            return TaskStatus.Running;
    }
}
