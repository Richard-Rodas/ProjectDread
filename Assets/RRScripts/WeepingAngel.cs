using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;


public class WeepingAngel : MonoBehaviour
{
    // The AI's Nav Mesh Agent
    public NavMeshAgent ai;

    // The player's Transform
    public Transform player;

    // The AI's Animator component
    public Animator aiAnim;

    // The AI's destination
    Vector3 dest;

    // The player's Camera and the AI's jumpscare Camera
    public Camera playerCam, jumpscareCam;

    // The AI's movement speed
    public float aiSpeed;

    // The distance in which the AI can catch the player from
    public float catchDistance;


    // The amount of seconds it takes for the AI's jumpscare to finish
    public float jumpscareTime;

    // The scene you load into after dying
    public string sceneAfterDeath;

    // Current Scene Name
    private string sceneName;

    // Timer Between Animation Poses
    public float timer = 2.0f;

    // Array of pose names
    public string[] poses = new string[] { "idle", "dancePose1", "dancePose2", "dancePose3", "dancePose4", "dancePose5", "dancePose6", "dancePose7", "dancePose8", };

    // AudioClips array for jumpscare sound effects
    public AudioClip[] jumpscareSounds;

    // Reference to the AudioSource component
    public AudioSource jumpscareAudioSource;

    private bool hasPlayerBeenSpotted = false;
    private float audioFadeOutTime = 7f;

    // Pages collected by player value
    public static int pagesCollected;

    //AI Raycast 
    private RaycastHit hit;


    private void Awake()
    {
        ai = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        pagesCollected = 0;
        ai.speed = 3.5f;
    }

    // The Update() method, stuff occurs every frame in this method
    void Update()
    {
        // Calculate the player's Camera's frustum planes
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(playerCam);

        // Get the AI's distance from the player
        float distance = Vector3.Distance(transform.position, player.position);

        // Define Raycast variables
        Vector3 eyeHeight = transform.position + Vector3.up * 1.6f; // Adjust height as needed
        Vector3 directionToPlayer = (player.position - transform.position).normalized;

        // If the AI is in the player's Camera's view
        if (GeometryUtility.TestPlanesAABB(planes, this.gameObject.GetComponent<Renderer>().bounds))
        {
            // Raycast to check if player is visible to AI
            if (Physics.Raycast(eyeHeight, directionToPlayer, out hit, Mathf.Infinity))
            {
                Debug.DrawRay(eyeHeight, directionToPlayer * hit.distance, Color.red); // Draw ray in scene view for debugging
                if (hit.transform == player)
                {
                    // Freeze AI's movement and play jumpscare sound effect
                    ai.speed = 0;
                    aiAnim.speed = 0;
                    if (!hasPlayerBeenSpotted && !jumpscareAudioSource.isPlaying && jumpscareSounds.Length > 0)
                    {
                        hasPlayerBeenSpotted = true;
                        int randomIndex = Random.Range(0, jumpscareSounds.Length);
                        jumpscareAudioSource.PlayOneShot(jumpscareSounds[randomIndex]);
                        StartCoroutine(FadeOutAudio(jumpscareAudioSource, audioFadeOutTime));
                    }
                }
            }
        }

        // Ghost Girl speed based on how many pages the player collected
        if (pagesCollected >= 1 && pagesCollected <= 5)
        {
            ai.speed += pagesCollected * 0.5f; // Increment speed for each page collected
        }

        // If the AI isn't in the player's Camera's view
        if (!GeometryUtility.TestPlanesAABB(planes, this.gameObject.GetComponent<Renderer>().bounds))
        {
            hasPlayerBeenSpotted = false;

            // Handle timer for animation poses
            timer -= Time.deltaTime;
            if (timer <= 0.0f)
            {
                aiAnim.SetBool("NextPose", true);
                // Method that chooses new pose
                // ChooseNewPose();
                timer = 2.0f;
            }

            // Reset AI's movement and animation speed
            ai.speed = aiSpeed;
            aiAnim.speed = 1;

            // Set AI's destination to player's position if within catch distance
            if (distance <= catchDistance)
            {
                player.gameObject.SetActive(false); // Disable player object
                aiAnim.Play("chokeLift");
                StartCoroutine(killPlayer()); // Start the killPlayer() coroutine
            }
            else
            {
                dest = player.position; // Set AI's destination to player's position
                ai.destination = dest;
            }
        }
    }

    // Coroutine to fade out audio over a specified time
    IEnumerator FadeOutAudio(AudioSource audioSource, float fadeTime)
    {
        float startVolume = audioSource.volume;
        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeTime;
            yield return null;
        }
        audioSource.Stop();
        audioSource.volume = startVolume; // Reset the volume for next time
    }

    // The killPlayer() coroutine
    IEnumerator killPlayer()
    {
        // Get scene name and apply to Retry Button in Main Menu script
        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
        MainMenu.lastLevelName = sceneName;

        // Wait for specified jumpscare time, then load scene after death
        yield return new WaitForSeconds(jumpscareTime);
        SceneManager.LoadScene(sceneAfterDeath);
    }
}
