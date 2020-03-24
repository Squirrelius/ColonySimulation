using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[TaskCategory("Lumberjack")]
public class IsTreeClose : Conditional
{
    public float _range;
    public SharedTransform _closestTree;

    public override void OnAwake()
    {

    }

    public override TaskStatus OnUpdate()
    {
        Tree closestTree = Util.FindClosestObj<Tree>(transform, _range);
        if (closestTree != null)
        {
            _closestTree.Value = closestTree.transform;
            return TaskStatus.Success;
        }
        else
            return TaskStatus.Failure;
    }
}
