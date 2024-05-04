using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent ai;
    public List<Transform> destinations;
    public Animator aiAnim;
    public float walkSpeed, chaseSpeed, minIdleTime, maxIdleTime, idleTime, sightDistance, catchDistance, chaseTime, minChaseTime, maxChaseTime, jumpscareTime;
    public bool walking, chasing;
    public Transform player;
    Transform currentDest;
    Vector3 dest;
    public int destinationAmount;
    public Vector3 rayCastOffset;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private FirstPersonController firstPersonController;
    private GameOverScreen gameOverScreen;
    //[SerializeField]private PickupPage pickupPage;
    [SerializeField] private AudioSource chaseMusic; //Chase Music
    public GameObject hideText, stopHideText;

    public Light flashlight;

   
    //Store all PickupPage components
    private PickupPage[] pickupPages;
    private string sceneName;

    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;

        walking = true;
        currentDest = destinations[Random.Range(0, destinations.Count)];
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        if (chaseMusic != null)
        {
            chaseMusic.loop = true; // Ensure the chase music is set to loop
            chaseMusic.playOnAwake = true;

            // Preload the chase music audio clip
            chaseMusic.clip.LoadAudioData();

        }

        // Initialize pickupPages array
        pickupPages = FindObjectsOfType<PickupPage>();
        if (pickupPages.Length == 0)
        {
            Debug.LogError("No PickupPage components found.");
        }
    }
    void Update()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        RaycastHit hit;

        // Check if the player is within sight distance
        if (Physics.Raycast(transform.position + rayCastOffset, direction, out hit, sightDistance))
        {
            if (hit.collider.gameObject.tag == "Player")
            {

                walking = false;
                StopCoroutine("stayIdle");
                StopCoroutine("chaseRoutine");
                StartCoroutine("chaseRoutine");
                chasing = true;
            }
        }

        // If chasing, continue pursuing the player
        if (chasing)
        {
            chaseMusic.enabled = true;
            dest = player.position;
            ai.destination = dest;
            ai.speed = chaseSpeed;
            aiAnim.ResetTrigger("walk");
            aiAnim.ResetTrigger("sprint");
            aiAnim.SetTrigger("sprint");
            float distance = Vector3.Distance(player.position, ai.transform.position);
            if (distance <= catchDistance)
            {
                if (flashlight != null)
                {
                    flashlight.enabled = false;
                }
                hideText.SetActive(false);
                stopHideText.SetActive(false);
                player.gameObject.SetActive(false);
                aiAnim.ResetTrigger("idle");
                aiAnim.SetTrigger("jumpscare");
                StartCoroutine(deathRoutine());
                chasing = false;
                firstPersonController.GetComponent<FirstPersonController>().enabled = false;
                MainMenu.lastLevelName = sceneName;
                // Reset pagesCollected when enemy catches the player
                ResetAllPagesCollected();

            }
        }

        // If walking, move towards the current destination
        if (walking)
        {
            dest = currentDest.position;
            ai.destination = dest;
            ai.speed = walkSpeed;
            aiAnim.ResetTrigger("sprint");
            aiAnim.ResetTrigger("idle");
            aiAnim.SetTrigger("walk");

            // Check if the AI cannot find a path and retry after a delay
            if (ai.pathStatus == NavMeshPathStatus.PathInvalid || ai.pathStatus == NavMeshPathStatus.PathPartial)
            {
                StartCoroutine(FallbackRoutine());
                return;
            }

            // Check if the AI has reached the destination
            if (ai.remainingDistance <= ai.stoppingDistance && !ai.pathPending)
            {
                aiAnim.ResetTrigger("sprint");
                aiAnim.ResetTrigger("walk");
                aiAnim.SetTrigger("idle");
                ai.speed = 0;
                StopCoroutine("stayIdle");
                StartCoroutine("stayIdle");
                walking = false;
            }
        }
    }

    void ResetAllPagesCollected()
    {
        foreach (var pickupPage in pickupPages)
        {
            pickupPage.ResetPagesCollected();
        }
    }

    public void stopChase()
    {

        if (chaseMusic != null && chaseMusic.isPlaying)
        {
            chaseMusic.Stop(); // Stop the chase music if it's currently playing
        }
        chaseMusic.enabled = false; // disable audio source
        walking = true;
        chasing = false;
        StopCoroutine("chaseRoutine");
        currentDest = destinations[Random.Range(0, destinations.Count)];
        
    }
    IEnumerator stayIdle()
    {
        idleTime = Random.Range(minIdleTime, maxIdleTime);
        yield return new WaitForSeconds(idleTime);
        currentDest = destinations[Random.Range(0, destinations.Count)];
        walking = true;
    }

    IEnumerator chaseRoutine()
    {
        chaseTime = Random.Range(minChaseTime, maxChaseTime);
        yield return new WaitForSeconds(chaseTime);
        walking = true;
        chasing = false;
        chaseMusic.enabled = true;
        if (chaseMusic != null && !chaseMusic.isPlaying)
        {
            chaseMusic.Play(); // Play the chase music if it's not already playing
        }else
        {
            chaseMusic.enabled=false;
            chaseMusic.Stop();
        }
        currentDest = destinations[Random.Range(0, destinations.Count)];
    }

    IEnumerator deathRoutine()
    {
        chaseMusic.enabled = false;
        chaseMusic.Stop(); //Stop chase music
        yield return new WaitForSeconds(jumpscareTime);
        SceneManager.LoadScene("DeathScreen");
    }

    // If the AI can't find a path, it will reroute a new one
    IEnumerator FallbackRoutine()
    {
      
        yield return new WaitForSeconds(1.0f);

        // Choose a new destination and resume walking
        currentDest = destinations[Random.Range(0, destinations.Count)];
        walking = true;
    }
}
