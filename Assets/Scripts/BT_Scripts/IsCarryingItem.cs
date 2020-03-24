using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[TaskCategory("Lumberjack")]
public class IsCarryingItem : Conditional
{
    public ItemTypes _itemToCompare;


    public override TaskStatus OnUpdate()
    {
        ItemTypes carriedItem = GetComponent<LumberJack>()._CarriedItem;
        if (carriedItem == _itemToCompare)
            return TaskStatus.Success;
        else
            return TaskStatus.Failure;
    }

}
