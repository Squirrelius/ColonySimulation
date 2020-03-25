using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;


[TaskCategory("Villager")]
public class DeliverItemToStorage : Action
{
    private Villager _lumberjack;
    private Storage _storage;
    private NavMeshAgent _navAgent;
    private Animator _animator;

    public override void OnAwake()
    {
        _lumberjack = GetComponent<Villager>();
        _navAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _storage = GameObject.FindObjectOfType<Storage>();
    }

    public override void OnStart()
    {
        _navAgent.SetDestination(_storage.transform.position);
        _navAgent.isStopped = false;
        _animator.SetBool("isWalking", true);
    }

    public override TaskStatus OnUpdate()
    {
        if (Util.isInRange(transform.position, _storage.transform.position, 6f))
        {
            _navAgent.isStopped = true;
            _animator.SetBool("isWalking", false);

            if (_lumberjack._CarriedItem == ItemTypes.Wood)
                _storage._Wood++;
            else if (_lumberjack._CarriedItem == ItemTypes.Meat)
                _storage._Meat++;

            _lumberjack._CarriedItem = ItemTypes.None;

            return TaskStatus.Success;
        }

        return TaskStatus.Running;
    }

}
