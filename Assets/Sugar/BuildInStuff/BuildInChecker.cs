using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildInChecker : MonoBehaviour
{
    public Material[] materials;

    public void changeValue (float f)
    {
        foreach (Material m in materials)
        {
            m.SetFloat("_FadeShift", f);
        }
    }
}
