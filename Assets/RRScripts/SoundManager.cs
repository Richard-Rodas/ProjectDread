using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class SoundManager : MonoBehaviour
{
    public AudioSource step1, step2;
    public FirstPersonController personController;


    private void Update()
    {
        if (Input.GetKey(KeyCode.W)|| Input.GetKey(KeyCode.S)|| Input.GetKey(KeyCode.A)|| Input.GetKey(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                step1.enabled = false;
                step2.enabled = true;
            }
            else
            {
                step1.enabled = true;
                step2.enabled = false;
            }
        }
        else
        {
            step1.enabled=false;
            step2.enabled=false;
        }
    }

}
