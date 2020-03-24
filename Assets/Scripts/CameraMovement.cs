using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float _movSpeed = 15;
    public float _scrollSpeed = 15;

    void Update()
    {
        _Pos += new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * _movSpeed * Time.deltaTime / Time.timeScale;

        _Pos -= Vector3.up * Input.GetAxisRaw("Mouse ScrollWheel") * _scrollSpeed;
    }

    private Vector3 _Pos
    {
        get => transform.position;
        set
        {
            value.y = Mathf.Clamp(value.y, 5, 60);

            transform.position = value;
        }
    }
}
