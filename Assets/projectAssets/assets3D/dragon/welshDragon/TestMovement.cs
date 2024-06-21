using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TestMovement : MonoBehaviour
{
    public Transform dragon;
    public Slider slider;
    public GameObject attackButton;
    public Button callDragonButton;
    public void MoveDragonPosY()
    {
        dragon.position = new Vector3(dragon.position.x, slider.value, dragon.position.z);
    }

    public Animator dragonAnim;

    public void CallDragon()
    {
        dragonAnim.SetTrigger("land");
    }

    public void DragonAttack()
    {
        if(!dragonAnim.GetBool("breatheFire"))
        {
            dragonAnim.SetBool("breatheFire", true);
        }
    }

    public void TriggerBreathFire(int state)
    {
        if(state == 0)
        {
            dragonAnim.SetBool("breatheFire", false);
        }
    }

    public void AllowAttack()
    {
        attackButton.SetActive(true);
        callDragonButton.interactable = false;
    }
}
