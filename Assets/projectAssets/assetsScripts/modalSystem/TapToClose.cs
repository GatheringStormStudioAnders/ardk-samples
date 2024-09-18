using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapToClose : MonoBehaviour
{
    void Update()
    {
        if (gameObject.activeSelf)
        {
            if (Input.GetMouseButtonDown(0))
            {
                gameObject.SetActive(false);
            }
        }
    }
}
