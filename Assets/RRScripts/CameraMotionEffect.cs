using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotionEffect : MonoBehaviour
{
   [SerializeField] private float intensity = 0.2f; 
   [SerializeField] private float speed = 1.0f;

    private Vector3 originalPos;
    
    // Start is called before the first frame update
    void Start()
    {
        originalPos = transform.position;   
    }

    // Update is called once per frame
    void Update()
    {
        //Calculate offset of Perlin noise
        float offsetX = Mathf.PerlinNoise(Time.time * speed, 0) * 2 - 1;
        float offsetZ = Mathf.PerlinNoise(0, Time.time * speed) * 2 - 1;

        //Applies motion to the camera
        transform.position = originalPos + new Vector3(offsetX, 0, offsetZ) * intensity;
    }
}
