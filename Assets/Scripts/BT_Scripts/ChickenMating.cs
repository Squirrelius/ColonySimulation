using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[TaskCategory("Chicken")]
public class ChickenMating : Action
{
    public SharedTransform _otherChicken;

    private Chicken _chicken;
    private float _timeToSpawnBabies;
    public override void OnAwake()
    {
        _chicken = GetComponent<Chicken>();
    }

    public override void OnStart()
    {
        _chicken.PlaySexyParticle(3);
        _timeToSpawnBabies = Time.time + 3;
    }

    public override TaskStatus OnUpdate()
    {
        _chicken.transform.LookAt(_otherChicken.Value);
        if (Time.time > _timeToSpawnBabies)
        {
            _chicken.SpawnBabies();
            return TaskStatus.Success;
        }

        if (_chicken._Fullness < 0.5f)
            return TaskStatus.Failure;
        else
            return TaskStatus.Running;
    }

}
