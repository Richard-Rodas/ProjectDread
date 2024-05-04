using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using TMPro.Examples;
using UnityEngine.SceneManagement;

public class PickupPage : MonoBehaviour
{
    public GameObject collectTextObj, intText, escapeTrigger, escapeText;
    public AudioSource pickupSound, ambianceLayer1, ambianceLayer2, ambianceLayer3, ambianceLayer4, ambianceLayer5, ambianceLayer6, ambianceLayer7, ambianceLayer8;
    public bool interactable;
    public static int pagesCollected;
    [SerializeField] private int totalPageNum;
    public TMP_Text collectText; //Text displaying how many pages the player has

    private void Start()
    {
        pagesCollected = 0;
        collectText.text = pagesCollected + "/" + totalPageNum +" notebooks";
        escapeText.SetActive(false);
    }
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Reach"))
        {
            intText.SetActive(true);
            interactable = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Reach"))
        {
            intText.SetActive(false);
            interactable = false;
        }
    }
    void Update()
    {
        CollectedPages();
    }

    public void CollectedPages()
    {
        if (interactable == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                pickupSound.Play();
                this.gameObject.SetActive(false);
                pagesCollected++;
                collectText.text = pagesCollected + "/" + totalPageNum + " notebooks";
                collectTextObj.SetActive(true);
                if (pagesCollected == 1)
                {
                    ambianceLayer1.Play();
                }
                if (pagesCollected == 2)
                {
                    ambianceLayer2.Play();
                }
                if (pagesCollected == 3)
                {
                    ambianceLayer3.Play();
                }
                if (pagesCollected == 4)
                {
                    ambianceLayer4.Play();
                }
                if (pagesCollected == 5)
                {
                    ambianceLayer5.Play();
                }
                if (pagesCollected == 6)
                {
                    ambianceLayer6.Play();
                }
                if (pagesCollected == 7)
                {
                    ambianceLayer7.Play();
                }
                if (pagesCollected == 8)
                {
                    ambianceLayer8.Play();
                    escapeText.SetActive(true);
                    escapeTrigger.SetActive(true);
                }
                intText.SetActive(false);
                interactable = false;
            }
        }
    }

    // Method to reset pagesCollected
    public void ResetPagesCollected()
    {
        pagesCollected = 0;
        collectText.text = pagesCollected + "/" + totalPageNum +" notebooks"; // Reset the collect text as well if needed
    }
}