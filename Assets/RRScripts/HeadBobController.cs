using StarterAssets;
using UnityEngine;

public class HeadBobController : MonoBehaviour
{
    [Range(0.001f, 0.01f)]
    public float walkAmount = 0.01f;
    [Range(1.0f, 30.0f)]
    public float walkFrequency = 15.0f;
    [Range(10.0f, 100.0f)]
    public float walkSmooth = 35.0f;

    [Range(0.001f, 0.01f)]
    public float runAmount = 0.01f;
    [Range(1.0f, 30.0f)]
    public float runFrequency = 25.0f;
    [Range(10.0f, 100.0f)]
    public float runSmooth = 75.0f;

    Vector3 startPos;
    private FirstPersonController firstPersonController;
    void Start()
    {
        startPos = transform.localPosition;
        firstPersonController = FirstPersonController.instance;
        if (firstPersonController == null)
        {
            Debug.LogError("FirstPersonController instance not found.");
        }

    }

    void Update()
    {
        CheckForHeadbobTrigger();
        StopHeadbob();
    }

    private void CheckForHeadbobTrigger()
    {
        float inputMagnitude = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).magnitude;
        bool groundCheck = firstPersonController.Grounded;

        if (inputMagnitude > 0 && groundCheck)
        {
            float amount = Input.GetKey(KeyCode.LeftShift) ? runAmount : walkAmount; //if player presses shift, set 
            float frequency = Input.GetKey(KeyCode.LeftShift) ? runFrequency : walkFrequency;
            float smooth = Input.GetKey(KeyCode.LeftShift) ? runSmooth : walkSmooth;

            StartHeadBob(amount, frequency, smooth);
        }
    }

    private void StartHeadBob(float amount, float frequency, float smooth)
    {
        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Lerp(pos.y, Mathf.Sin(Time.time * frequency) * amount * 1.4f, smooth * Time.deltaTime);
        pos.x += Mathf.Lerp(pos.x, Mathf.Cos(Time.time * frequency / 2f) * amount * 1.6f, smooth * Time.deltaTime);
        transform.localPosition += pos;
    }

    private void StopHeadbob()
    {
        if (transform.localPosition == startPos) return;
        transform.localPosition = Vector3.Lerp(transform.localPosition, startPos, 1 * Time.deltaTime);
    }
}
