using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LightSwitch : MonoBehaviour
{
    [SerializeField] private GameObject _lightsTextON; // Turn on light text
    [SerializeField] private GameObject _lightsTextOFF; // Turn off light text
    [SerializeField] private GameObject _lightsOB; // Light source object
    [SerializeField] private AudioSource _switchClick; // Audio for light switch

    public bool _lightsAreOn; // Lights are on
    public bool _lightsAreOff; // Lights are off
    public bool _inReach; // Player is in reach of light switch

    // Start is called before the first frame update
    void Start()
    {
        _lightsTextOFF.SetActive(false);
        _lightsTextON.SetActive(false);
        _inReach = false;
        _lightsAreOn = false;
        _lightsAreOff = true;
        _lightsOB.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Reach"))
        {
            _inReach = true;
            UpdateTextVisibility();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Reach"))
        {
            _inReach = false;
            _lightsTextON.SetActive(false);
            _lightsTextOFF.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_inReach && Input.GetKeyDown(KeyCode.E))
        {
            ToggleLights();
        }
    }

    void ToggleLights()
    {
        _lightsAreOn = !_lightsAreOn;
        _lightsAreOff = !_lightsAreOff;
        _lightsOB.SetActive(_lightsAreOn);
        _switchClick.Play();
        UpdateTextVisibility();
    }

    void UpdateTextVisibility()
    {
        if (_inReach)
        {
            _lightsTextON.SetActive(!_lightsAreOn);
            _lightsTextOFF.SetActive(_lightsAreOn);
        }
        else
        {
            _lightsTextON.SetActive(false);
            _lightsTextOFF.SetActive(false);
        }
    }
}
