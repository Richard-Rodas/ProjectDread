using StarterAssets;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    [SerializeField] private Light lightSource;
    [SerializeField] private float flickerDistance = 10f;
    [SerializeField] private float maxTime;
    [SerializeField] private float minTime;
    [SerializeField] private float timer;

    private bool flickeringEnabled = false;


    private void Start()
    {
        timer = Random.Range(minTime, maxTime);
    }

    void Update()
    {
        if (Time.timeScale == 1)
        {
            ToggleLight();
        }
    }

    // Function to toggle the light
    void ToggleLight()
    {
        // Check if the FirstPersonController instance is not null and the player is within the flicker distance
        if (FirstPersonController.instance != null && Vector3.Distance(transform.position, FirstPersonController.instance.transform.position) <= flickerDistance)
        {
            // Enable flickering
            flickeringEnabled = true;
        }
        else
        {
            // Disable flickering
            flickeringEnabled = false;
            // Optionally turn off the light to save resources when the player is far away
            lightSource.enabled = false;
        }

        if (flickeringEnabled && timer > 0)
        {
            timer -= Time.deltaTime;
        }
        if (timer <= 0)
        {
            lightSource.enabled = !lightSource.enabled;
            timer = Random.Range(minTime, maxTime);
        }
    }
}
