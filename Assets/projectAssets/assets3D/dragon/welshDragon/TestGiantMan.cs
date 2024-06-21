using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGiantMan : MonoBehaviour
{
    public Animator anim;

    public void SetTrigger(string trigger)
    {
        anim.SetTrigger(trigger);
    }
}
