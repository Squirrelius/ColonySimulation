using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[TaskCategory("Lumberjack")]
public class ChopTree : Action
{
    public float _chopDuration = 5;
    public SharedTransform _treeToChop;

    private Animator _animator;
    private LumberJack _lumberJack;
    private float _timeStartedChopping;
    public override void OnAwake()
    {
        _lumberJack = GetComponent<LumberJack>();
        _animator = GetComponent<Animator>();
    }

    public override void OnStart()
    {
        _timeStartedChopping = Time.time;
    }

    public override TaskStatus OnUpdate()
    {
        _animator.SetBool("isChopping", true);
        
        if(Time.time >= _timeStartedChopping + _chopDuration)
        {
            transform.LookAt(_treeToChop.Value, Vector3.up);
            _animator.SetBool("isChopping", false);
            GameObject.Destroy(_treeToChop.Value.gameObject);
            _lumberJack._CarriedItem = ItemTypes.Wood;
            return TaskStatus.Success;
        }

        return TaskStatus.Running;
    }
}
