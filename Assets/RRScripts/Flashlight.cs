using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public GameObject flashlight;
    public AudioSource turnOn;
    public AudioSource turnOff;
    public Animator flashlightAnim;

    private bool isOn = true;
    private bool isMoving = false;
    private bool isSprinting = false;

    void Start()
    {
        flashlight.SetActive(false);
        isOn = false;
    }

    void Update()
    {
        // Toggle flashlight on/off when pressing F
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (isOn)
            {
                TurnOffFlashlight();
            }
            else
            {
                TurnOnFlashlight();
            }
        }

        // Check for movement input
        isMoving = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);

        // Check for sprinting input
        isSprinting = Input.GetKey(KeyCode.LeftShift);

        // Update flashlight animation based on movement and sprinting
        UpdateFlashlightAnimation();
    }

    void TurnOnFlashlight()
    {
        flashlight.SetActive(true);
        turnOn.Play();
        isOn = true;
    }

    void TurnOffFlashlight()
    {
        flashlight.SetActive(false);
        turnOff.Play();
        isOn = false;
    }

    void UpdateFlashlightAnimation()
    {
        if (isMoving)
        {
            if (isSprinting)
            {
                flashlightAnim.ResetTrigger("walk");
                flashlightAnim.SetTrigger("sprint");
            }
            else
            {
                flashlightAnim.ResetTrigger("sprint");
                flashlightAnim.SetTrigger("walk");
            }
        }
        else // If no movement keys are pressed
        {
           
            flashlightAnim.ResetTrigger("sprint");
            flashlightAnim.ResetTrigger("walk"); // Reset both triggers to return flashlight to default position
            flashlightAnim.SetTrigger("walk");
        }
    }
}
