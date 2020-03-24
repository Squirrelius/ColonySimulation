using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public event Action<Tree> onDie;

    public void Die()
    {
        Destroy(gameObject);
        if (onDie != null)
            onDie(this);
    }
}
