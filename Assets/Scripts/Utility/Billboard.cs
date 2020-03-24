using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform camTransform;

    Quaternion originalRotation;

    void Awake()
    {
        originalRotation = transform.rotation;
        camTransform = FindObjectOfType<Camera>().transform;
    }

    void Start()
    {
    }

    void Update()
    {
        transform.rotation = camTransform.rotation;
    }
}