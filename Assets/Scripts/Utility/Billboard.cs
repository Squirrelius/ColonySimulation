using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform camTransform;

    Quaternion originalRotation;

    void Awake()
    {
        camTransform = FindObjectOfType<Camera>().transform;
    }

    void Start()
    {
        originalRotation = transform.rotation;
    }

    void Update()
    {
        transform.rotation = camTransform.rotation * originalRotation;
    }
}