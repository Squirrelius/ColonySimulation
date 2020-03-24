using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush : MonoBehaviour
{

    public event Action<Bush> onDie;


    private void OnDestroy()
    {

        if (onDie != null)
            onDie(this);
        transform.DOKill();
        Destroy(gameObject);
    }
}
