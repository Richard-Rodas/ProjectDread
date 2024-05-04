using UnityEngine;

public class KeySpawn : MonoBehaviour
{
    // Array to hold the three destination points
    public Transform[] destinationPoints;

    // Reference to the key GameObject
    public GameObject keyObject;

    // Variables for the hovering effect
    public float hoverSpeed = 1f; // Speed of the hovering effect
    public float hoverHeight = 0.5f; // Height of the hovering effect

    private Vector3 initialPosition; // Initial position of the key

    void Start()
    {
        // Check if there are at least three destination points
        if (destinationPoints.Length < 3)
        {
            Debug.LogError("Error: Not enough destination points specified.");
            return;
        }

        // Choose a random destination point index
        int randomIndex = Random.Range(0, destinationPoints.Length);

        // Get the randomly chosen destination point
        Transform destination = destinationPoints[randomIndex];

        // Move the key object to the chosen destination point
        keyObject.transform.position = destination.position;

        // Store the initial position of the key
        initialPosition = keyObject.transform.position;
    }

    void Update()
    {
        // Check if the player camera is within the view frustum
        if (IsPlayerCameraInView())
        {
            // Calculate the vertical offset using a sine wave
            float yOffset = Mathf.Sin(Time.time * hoverSpeed) * hoverHeight;

            // Apply the offset to the key's position
            keyObject.transform.position = initialPosition + new Vector3(0f, yOffset, 0f);
        }
    }

    // Method to check if the player camera is within the view frustum
    bool IsPlayerCameraInView()
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        return GeometryUtility.TestPlanesAABB(planes, keyObject.GetComponent<Renderer>().bounds);
    }
}
