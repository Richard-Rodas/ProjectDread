using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public GameObject door;
    public bool isOpen;
    bool toggle;
    public AudioSource doorSound;

    [SerializeField] private bool inReach;
    public GameObject openText;
    public Animator anim;

    [SerializeField]private OcclusionPortal? myOcclusionPortal;


    private void Awake()
    {
        myOcclusionPortal = GetComponent<OcclusionPortal>();
    }
    private void Start()
    {
        openText.SetActive(false);
        myOcclusionPortal.open = false;
    }
    // Update is called once per frame
    void Update()
    {

        if (inReach && Input.GetKeyDown(KeyCode.E))
        {
            openClose(); 
        }

    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = true;
            openText.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = false;
            openText.SetActive(false);
        }
    }
    public void openClose()
    {
            toggle = !toggle;
            if (toggle == false)
            {
                anim.ResetTrigger("open");
                anim.SetTrigger("close");
                doorSound.Play();
                OpenDoor();
            }
            if (toggle == true)
            {
                anim.ResetTrigger("close");
                anim.SetTrigger("open");
                doorSound.Play();
                OpenDoor();
            } 
    }

    void OpenDoor()
    {
        // Toggle the Occlusion Portal's open state, so that Unity renders the GameObjects behind it
        myOcclusionPortal.open = !myOcclusionPortal.open;
    
    }



}
