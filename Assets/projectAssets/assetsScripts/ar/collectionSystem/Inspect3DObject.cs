using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inspect3DObject : MonoBehaviour
{
    public float rotationSpeed = 1f;
    public Vector2 xClamp;
    public Vector2 yClamp;
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            float xAxisRot = Input.GetAxis("Mouse X") * rotationSpeed;
            float yAxisRot = Input.GetAxis("Mouse Y") * rotationSpeed;

            transform.Rotate(Vector3.down, xAxisRot);
            transform.Rotate(Vector3.right, yAxisRot);

            //if(transform.rotation.eulerAngles.x <= xClamp.x)
            //{
            //    transform.rotation = Quaternion.Euler(new Vector3(xClamp.x, transform.rotation.eulerAngles.y, 0));
            //}
            //if (transform.rotation.eulerAngles.x >= xClamp.y)
            //{
            //    transform.rotation = Quaternion.Euler(new Vector3(xClamp.y, transform.rotation.eulerAngles.y, 0));
            //}

            //if (transform.rotation.eulerAngles.y <= yClamp.x)
            //{
            //    transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, yClamp.x, 0));
            //}
            //if (transform.rotation.eulerAngles.y >= yClamp.y)
            //{
            //    transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, yClamp.y, 0));
            //}

            //float x = Mathf.Clamp(transform.rotation.eulerAngles.x, xClamp.x, xClamp.y);
            //float y = Mathf.Clamp(transform.rotation.eulerAngles.y, yClamp.x, yClamp.y);

            //transform.rotation = Quaternion.Euler(new Vector3(x, y, 0));
        }
    }
}
