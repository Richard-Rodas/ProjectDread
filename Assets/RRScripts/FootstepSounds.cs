using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using static Unity.Burst.Intrinsics.X86;

public class FootstepSounds : MonoBehaviour
{
    private FirstPersonController firstPersonController;
    public AudioSource walkSound,sprintSound;
    //public AudioClip[] footstepSounds; //Array of footstep audio
    


    public void Start()
    {
        firstPersonController = FirstPersonController.instance;
        if (firstPersonController == null)
        {
            Debug.LogError("FirstPersonController instance not found.");
        }
    }

    private void Update()
    {
        if (walkSound != null || sprintSound != null)
        {
            PlayFootStep();
        }
    }

    public void PlayFootStep()
    {
        bool groundCheck = firstPersonController.Grounded; 

        if (groundCheck && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
        {
            if (groundCheck && Input.GetKey(KeyCode.LeftShift))
            {
                walkSound.enabled = false;
                sprintSound.enabled = true;
            }
            else
            {
                walkSound.enabled = true;
                sprintSound.enabled = false;
            }
        }
        else
        {
            walkSound.enabled = false;
            sprintSound.enabled = false;
        }
    }

    

 
}

