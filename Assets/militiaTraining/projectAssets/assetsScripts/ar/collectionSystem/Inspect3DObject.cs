using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Inspect3DObject : MonoBehaviour
{
    public float rotationSpeed = 1f;
    public Vector2 xClamp;
    public Vector2 yClamp;

    public float yRotation;
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            yRotation += Input.GetAxis("Mouse X");
        }

        yRotation = Mathf.Clamp(yRotation, -25, 25);
        transform.Rotate(Vector3.down, yRotation * Time.deltaTime);
        transform.localRotation = Quaternion.Euler(0, yRotation, 0);
    }
}
