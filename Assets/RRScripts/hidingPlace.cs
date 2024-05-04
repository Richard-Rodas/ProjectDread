using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hidingPlace : MonoBehaviour
{
    [SerializeField] public GameObject hideText, stopHideText;
    [SerializeField] public GameObject normalPlayer, hidingPlayer;
    [SerializeField] public EnemyAI monsterScript;
    [SerializeField] public Transform monsterTransform;
    [SerializeField] bool interactable, hiding;
    [SerializeField] public float loseDistance;


    void Start()
    {
        hidingPlayer.SetActive(false);
        interactable = false;
        hiding = false;
        hideText.SetActive(false);
        stopHideText.SetActive(false);
        
    }

    void OnTriggerStay(Collider other)
    {
        if (Time.timeScale == 1 && !hiding) // Check if the player is not already hiding
        {
            if (other.CompareTag("Reach"))
            {
                hideText.SetActive(true);
                interactable = true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (Time.timeScale == 1)
        {
            if (other.CompareTag("Reach"))
            {
                hideText.SetActive(false);
                interactable = false;
            }
        }
    }



    void Update()
    {
        if (interactable == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                hideText.SetActive(false);
                hidingPlayer.SetActive(true);
                float distance = Vector3.Distance(monsterTransform.position, normalPlayer.transform.position);
                if (distance > loseDistance)
                {
                    if (monsterScript.chasing == true)
                    {
                        monsterScript.stopChase();
                    }
                }
                stopHideText.SetActive(true);
                hiding = true;
                normalPlayer.SetActive(false);
                interactable = false;
            }
        }

        if (hiding == true)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                stopHideText.SetActive(false);
                normalPlayer.SetActive(true);
                hidingPlayer.SetActive(false);
                hiding = false;
            }
        }
    }
}
