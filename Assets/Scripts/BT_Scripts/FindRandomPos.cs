using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindRandomPos : Action
{
    public SharedFloat _minX = -75;
    public SharedFloat _maxX = 75;
    public SharedFloat _minZ = -75;
    public SharedFloat _maxZ = 75;

    public SharedVector3 _rndPos;

    public override TaskStatus OnUpdate()
    {
        int rndPosCounter = 0;
        while(rndPosCounter < 50)
        {
            float rndX = Random.Range(_minX.Value, _maxX.Value);
            float rndZ = Random.Range(_minZ.Value, _maxZ.Value);

            Vector3 rndPos = new Vector3(rndX, 0, rndZ);

            Collider[] collidingObjects = Physics.OverlapSphere(rndPos, 1, ~(1 << LayerMask.NameToLayer("Ground")));
            if (collidingObjects.Length == 0)
            {
                _rndPos.Value = rndPos;
                return TaskStatus.Success;
            }
            else
                rndPosCounter++;
        }

        return TaskStatus.Failure;
    }
}
